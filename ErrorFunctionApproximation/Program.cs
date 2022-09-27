using MultiPrecision;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ErrorFunctionApproximation {
    class Program {
        static void Main(string[] args) {
            MultiPrecision<Pow2.N32>[] xs = new MultiPrecision<Pow2.N32>[]{
                0.5, 1, 2, 4, 8, 16, 27.25
            };

            MultiPrecision<Pow2.N32> dx = 1d / 1024;

            using StreamWriter sw = new("../../../../results/pade_erfc_e32.txt");
            sw.WriteLine("pade approximant exp(-x^2)/erfc(x)");

            for (int j = 0; j < xs.Length - 1; j++) {
                MultiPrecision<Pow2.N32> x0 = xs[j], x1 = xs[j + 1];

                sw.WriteLine($"\nrange x in [{x0}, {x1}]");

                sw.WriteLine("expected");
                List<MultiPrecision<Pow2.N32>> expecteds = new();
                for (MultiPrecision<Pow2.N32> x = x0; x <= x1; x += dx) {
                    MultiPrecision<Pow2.N32> y = MultiPrecision<Pow2.N32>.Exp(-x * x) / MultiPrecision<Pow2.N32>.Erfc(x);
                
                    expecteds.Add(y);
                
                    //sw.WriteLine($"{x},{y:e40}");
                }
                
                sw.WriteLine($"diffs x = {x0}");
                List<MultiPrecision<Pow2.N32>> diffs = new();
                for (int d = 0; d <= 128; d++) {
                    MultiPrecision<Pow2.N32> dy = ErfcExpScaled<Pow2.N32>.Diff(d, x0) * MultiPrecision<Pow2.N32>.TaylorSequence[d];
                    diffs.Add(dy);
                
                    //sw.WriteLine($"{d},{dy:e40}");
                }
                sw.Flush();

                sw.WriteLine("pade results");
                bool is_finished = false;

                for (int m = 4; m <= 64 && !is_finished; m++) {
                    for (int n = m - 1; n <= m + 1 && m + n < 128 && !is_finished; n++) {
                        (MultiPrecision<Pow2.N32>[] ms, MultiPrecision<Pow2.N32>[] ns) =
                            PadeSolver<Pow2.N32>.Solve(diffs.Take(m + n + 1).ToArray(), m, n);

                        MultiPrecision<Pow2.N32> err = 0;
                        for ((int i, MultiPrecision<Pow2.N32> x) = (0, x0); i < expecteds.Count; i++, x += dx) {
                            MultiPrecision<Pow2.N32> expected = expecteds[i];
                            MultiPrecision<Pow2.N32> actual = PadeSolver<Pow2.N32>.Approx(x - x0, ms, ns);

                            err = MultiPrecision<Pow2.N32>.Max(err, MultiPrecision<Pow2.N32>.Abs(expected / actual - 1));
                        }

                        Console.WriteLine($"m={m},n={n}");
                        Console.WriteLine($"{err:e20}");

                        if (err < 1e-32 && ms.All((v) => v > 0) && ns.All((v) => v > 0)) {
                            sw.WriteLine($"m={m},n={n}");
                            sw.WriteLine("ms");
                            for (int i = 0; i < ms.Length; i++) {
                                sw.WriteLine($"{ms[i]:e40}");
                            }
                            sw.WriteLine("ns");
                            for (int i = 0; i < ns.Length; i++) {
                                sw.WriteLine($"{ns[i]:e40}");
                            }
                            sw.WriteLine("relative err");
                            sw.WriteLine($"{err:e20}");
                            sw.Flush();

                            is_finished = true;
                            break;
                        }
                    }
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}