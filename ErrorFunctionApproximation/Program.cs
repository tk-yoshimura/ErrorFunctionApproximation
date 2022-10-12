using MultiPrecision;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ErrorFunctionApproximation {
    class Program {
        static void Main(string[] args) {
             MultiPrecision<Pow2.N32> dx = 1d / 8192;

            using StreamWriter sw = new("../../../../results_disused/pade_erf_e32_hex.txt");
            sw.WriteLine("pade approximant f(x^2) f(x):=erf(x)/x");

            MultiPrecision<Pow2.N32> x0 = 0, x1 = 0.5;

            sw.WriteLine($"\nrange x in [{x0}, {x1}]");

            MultiPrecision<Pow2.N32> c = 2 / MultiPrecision<Pow2.N32>.Sqrt(MultiPrecision<Pow2.N32>.PI);

            sw.WriteLine("expected");
            List<MultiPrecision<Pow2.N32>> expecteds = new();
            for (MultiPrecision<Pow2.N32> x = x0; x <= x1; x += dx) {
                MultiPrecision<Pow2.N32> y = (x > 0) ? MultiPrecision<Pow2.N32>.Erf(x) / (x * c) : 1;
                
                expecteds.Add(y);
                
                //sw.WriteLine($"{x},{y:e40}");
            }
                
            sw.WriteLine($"diffs x = {x0}");
            List<MultiPrecision<Pow2.N32>> diffs = new();
            for (int d = 0; d <= 128; d++) {
                MultiPrecision<Pow2.N32> dy = 1 / ErfTaylorSeries.Coef<Pow2.N32>(d);
                diffs.Add(dy);
                
                //sw.WriteLine($"{d},{dy:e40}");
            }
            sw.Flush();

            sw.WriteLine("pade results");

            for (int m = 4; m <= 64; m++) {
                (MultiPrecision<Pow2.N32>[] ms, MultiPrecision<Pow2.N32>[] ns) =
                    PadeSolver<Pow2.N32>.Solve(diffs.Take(m + m + 1).ToArray(), m, m);

                MultiPrecision<Pow2.N32> err = 0;
                for ((int i, MultiPrecision<Pow2.N32> x) = (0, x0); i < expecteds.Count; i++, x += dx) {
                    MultiPrecision<Pow2.N32> expected = expecteds[i];
                    MultiPrecision<Pow2.N32> actual = PadeSolver<Pow2.N32>.Approx(MultiPrecision<Pow2.N32>.Square(x - x0), ms, ns);

                    err = MultiPrecision<Pow2.N32>.Max(err, MultiPrecision<Pow2.N32>.Abs(expected / actual - 1));
                }

                Console.WriteLine($"m={m},n={m}");
                Console.WriteLine($"{err:e20}");

                if (err < 4e-32) {
                    sw.WriteLine($"m={m},n={m}");
                    sw.WriteLine("ms");
                    for (int i = 0; i <= m; i++) {
                        sw.WriteLine($"({ToFP128(ms[i] * c)}, {ToFP128(ns[i])}), ");
                    }
                    sw.WriteLine("relative err");
                    sw.WriteLine($"{err:e20}");
                    sw.Flush();
                    break;
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