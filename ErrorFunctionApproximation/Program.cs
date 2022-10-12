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

            using StreamWriter sw = new("../../../../results_disused/pade_erfc_e32_hex.txt");
            sw.WriteLine("pade approximant erfc(x)/exp(-x^2)");

            for (int j = 0; j < xs.Length - 1; j++) {
                MultiPrecision<Pow2.N32> x0 = xs[j], x1 = xs[j + 1];
                MultiPrecision<Pow2.N32> dx = (x1 - x0) / 8192;

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
                
                for (int m = 10; m <= 32; m++) {
                    (MultiPrecision<Pow2.N32>[] ms, MultiPrecision<Pow2.N32>[] ns) =
                        PadeSolver<Pow2.N32>.Solve(diffs.Take(m + m + 1).ToArray(), m, m);

                    MultiPrecision<Pow2.N32> err = 0;
                    for ((int i, MultiPrecision<Pow2.N32> x) = (0, x0); i < expecteds.Count; i++, x += dx) {
                        MultiPrecision<Pow2.N32> expected = expecteds[i];
                        MultiPrecision<Pow2.N32> actual = PadeSolver<Pow2.N32>.Approx(x - x0, ms, ns);

                        err = MultiPrecision<Pow2.N32>.Max(err, MultiPrecision<Pow2.N32>.Abs(expected / actual - 1));
                    }

                    Console.WriteLine($"m={m},n={m}");
                    Console.WriteLine($"{err:e20}");

                    if (err < 4e-32) {
                        sw.WriteLine($"m={m},n={m}");
                        for (int i = 0; i <= m; i++) {
                            sw.WriteLine($"({ToFP128(ns[i])}, {ToFP128(ms[i])}), ");
                        }
                        sw.WriteLine("relative err");
                        sw.WriteLine($"{err:e20}");
                        sw.Flush();

                        break;
                    }
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }

        public static string ToFP128(MultiPrecision<Pow2.N32> x) { 
            Sign sign = x.Sign;
            long exponent = x.Exponent;
            uint[] mantissa = x.Mantissa.Reverse().ToArray();
            
            string code = $"({(sign == Sign.Plus ? "+1" : "-1")}, {exponent}, 0x{mantissa[0]:X8}{mantissa[1]:X8}uL, 0x{mantissa[2]:X8}{mantissa[3]:X8}uL)";

            return code;
        }
    }
}