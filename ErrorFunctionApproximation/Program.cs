using MultiPrecision;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ErrorFunctionApproximation {
    class Program {
        static void Main(string[] args) {
            using StreamWriter sw = new("../../../../results_disused/pade_erfc_n32.txt");
            sw.WriteLine("pade approximant exp(-x^2)/erfc(x)");

            sw.WriteLine("expected");
            List<MultiPrecision<Pow2.N32>> expecteds = new();
            for (MultiPrecision<Pow2.N32> x = 0.5; x <= 27.25; x += 1d / 64) {
                MultiPrecision<Pow2.N32> y = MultiPrecision<Pow2.N32>.Exp(-x * x) / MultiPrecision<Pow2.N32>.Erfc(x);

                expecteds.Add(y);

                sw.WriteLine($"{x},{y}");
            }

            sw.WriteLine("diffs x = 0.5");
            List<MultiPrecision<Pow2.N32>> diffs = new();
            for (int d = 0; d <= 128; d++) {
                MultiPrecision<Pow2.N32> dy = ErfcExpScaled<Pow2.N32>.Diff(d, 0.5) * MultiPrecision<Pow2.N32>.TaylorSequence[d];
                diffs.Add(dy);

                sw.WriteLine($"{d},{dy}");
            }
            sw.Flush();

            sw.WriteLine("pade results");
            for (int m = 4; m <= 64; m++) {
                for (int n = m / 2; n <= m * 2 && m + n < 128; n++) {
                    (MultiPrecision<Pow2.N32>[] ms, MultiPrecision<Pow2.N32>[] ns) =
                        PadeSolver<Pow2.N32>.Solve(diffs.Take(m + n + 1).ToArray(), m, n);

                    MultiPrecision<Pow2.N32> err = 0;
                    for ((int i, MultiPrecision<Pow2.N32> x) = (0, 0.5); x <= 27.25; i++, x += 1d / 64) {
                        MultiPrecision<Pow2.N32> expected = expecteds[i];
                        MultiPrecision<Pow2.N32> actual = PadeSolver<Pow2.N32>.Approx(x - 0.5, ms, ns);

                        err = MultiPrecision<Pow2.N32>.Max(err, MultiPrecision<Pow2.N32>.Abs(expected / actual - 1));
                    }
                    if (err < 1e-15) {
                        sw.WriteLine($"\nm={m},n={n}");
                        sw.WriteLine("ms");
                        for (int i = 0; i < ms.Length; i++) {
                            sw.WriteLine(ms[i]);
                        }
                        sw.WriteLine("ns");
                        for (int i = 0; i < ns.Length; i++) {
                            sw.WriteLine(ns[i]);
                        }
                        sw.WriteLine("relative err");
                        sw.WriteLine($"{err:e20}");
                        sw.Flush();
                    }

                    Console.WriteLine($"m={m},n={n}");
                    Console.WriteLine($"{err:e20}");
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}