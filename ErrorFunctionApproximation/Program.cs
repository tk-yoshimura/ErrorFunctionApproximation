using MultiPrecision;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ErrorFunctionApproximation {
    class Program {
        static void Main(string[] args) {
            MultiPrecision<Pow2.N32>[] xs = new MultiPrecision<Pow2.N32>[]{
                1, 2, 4, 8, 16, 32
            };

            static MultiPrecision<Pow2.N32> f32(MultiPrecision<Pow2.N32> x) {
                return MultiPrecision<Pow2.N32>.InverseErfc(MultiPrecision<Pow2.N32>.Pow2(-x * x));
            };

            static MultiPrecision<Pow2.N64> f64(MultiPrecision<Pow2.N64> x) {
                return MultiPrecision<Pow2.N64>.InverseErfc(MultiPrecision<Pow2.N64>.Pow2(-x * x));
            };

            using StreamWriter sw = new("../../../../results/pade_inverfc_e16.txt");
            sw.WriteLine("pade approximant inverse_erfc(2^(-x^2))");

            for (int j = 0; j < xs.Length - 1; j++) {
                MultiPrecision<Pow2.N32> x0 = xs[j], x1 = xs[j + 1];
                MultiPrecision<Pow2.N32> dx = (x1 - x0) / 2048;

                sw.WriteLine($"\nrange x in [{x0}, {x1}]");

                //sw.WriteLine("expected");
                List<MultiPrecision<Pow2.N32>> expecteds = new();
                for (MultiPrecision<Pow2.N32> x = x0; x <= x1; x += dx) {
                    MultiPrecision<Pow2.N32> y = f32(x);
                
                    expecteds.Add(y);

                    if ((x % 0.125) == 0) {
                        Console.Write(".");
                    }
                
                    //sw.WriteLine($"{x},{y:e40}");
                }

                Console.Write("\n");
                
                sw.WriteLine($"diffs x = {x0}");
                MultiPrecision<Pow2.N64>[] diffs = FiniteDifference<Pow2.N64>.Diff(x0.Convert<Pow2.N64>(), f64, Math.ScaleB(1, -20));
                for (int i = 0; i < diffs.Length; i++) {
                    sw.WriteLine($"{i},{diffs[i]:e40}");
                }
                sw.Flush();

                MultiPrecision<Pow2.N32>[] cs = new MultiPrecision<Pow2.N32>[diffs.Length + 1];
                cs[0] = f32(x0);
                for (int i = 0; i < diffs.Length; i++) {
                    cs[i + 1] = diffs[i].Convert<Pow2.N32>() * MultiPrecision<Pow2.N32>.TaylorSequence[i + 1];
                }

                sw.WriteLine("pade results");
                bool is_finished = false;

                for (int m = 4; m < diffs.Length && !is_finished; m++) {
                    for (int n = m - 1; n <= m + 1 && m + n < diffs.Length; n++) {
                        (MultiPrecision<Pow2.N32>[] ms, MultiPrecision<Pow2.N32>[] ns) =
                            PadeSolver<Pow2.N32>.Solve(cs.Take(m + n + 1).ToArray(), m, n);

                        MultiPrecision<Pow2.N32> err = 0;
                        for ((int i, MultiPrecision<Pow2.N32> x) = (0, x0); i < expecteds.Count; i++, x += dx) {
                            MultiPrecision<Pow2.N32> expected = expecteds[i];
                            MultiPrecision<Pow2.N32> actual = PadeSolver<Pow2.N32>.Approx(x - x0, ms, ns);

                            err = MultiPrecision<Pow2.N32>.Max(err, MultiPrecision<Pow2.N32>.Abs(expected / actual - 1));
                        }

                        Console.WriteLine($"m={m},n={n}");
                        Console.WriteLine($"{err:e20}");

                        if (err < 1e-16) {
                            sw.WriteLine($"m={m},n={n}");
                            sw.WriteLine("ms");
                            for (int i = 0; i < ms.Length; i++) {
                                sw.WriteLine($"{ms[i]:e20}");
                            }
                            sw.WriteLine("ns");
                            for (int i = 0; i < ns.Length; i++) {
                                sw.WriteLine($"{ns[i]:e20}");
                            }
                            sw.WriteLine("relative err");
                            sw.WriteLine($"{err:e20}");
                            sw.Flush();

                            if (ms.All((v) => v > 0) && ns.All((v) => v > 0)) {
                                is_finished = true;
                            }
                        }
                    }
                }

                if (!is_finished) {
                    Console.WriteLine("not convergence");
                }
            }


            Console.WriteLine("END");
            Console.Read();
        }
    }
}