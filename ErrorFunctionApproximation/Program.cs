using MultiPrecision;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ErrorFunctionApproximation {
    class Program {
        static void Main(string[] args) {
            MultiPrecision<Pow2.N32> dx = 1d / 1024;

            using StreamWriter sw = new("../../../../results_disused/series_inverf_e31_hex.txt");
            sw.WriteLine("pade approximant f(x^2) f(x):=inverf(x)/x");

            MultiPrecision<Pow2.N32> x0 = 0, x1 = 0.5;

            sw.WriteLine($"\nrange x in [{x0}, {x1}]");

            MultiPrecision<Pow2.N32> c = MultiPrecision<Pow2.N32>.Sqrt(MultiPrecision<Pow2.N32>.PI) / 2;

            sw.WriteLine("expected");
            List<MultiPrecision<Pow2.N32>> expecteds = new();
            for (MultiPrecision<Pow2.N32> x = x0; x <= x1; x += dx) {
                MultiPrecision<Pow2.N32> y = (x > 0) ? MultiPrecision<Pow2.N32>.InverseErf(x) / x : c;
                
                expecteds.Add(y);
                
                //sw.WriteLine($"{x},{y:e40}");
            }
                
            sw.WriteLine($"series x = {x0}");
            List<MultiPrecision<Pow2.N32>> diffs = new();
            for (int d = 0; d <= 64; d++) {
                MultiPrecision<Pow2.N32> dy = InvErfTaylorSeries.Coef<Pow2.N32>(d);
                diffs.Add(dy);
                
                sw.WriteLine($"{ToFP128(dy)}, ");

                if (MultiPrecision<Pow2.N32>.Ldexp(dy, -4 * d) < 3e-32) {
                    break;
                }
            }
            sw.Flush();

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