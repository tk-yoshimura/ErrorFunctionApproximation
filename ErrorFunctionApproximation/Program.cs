using MultiPrecision;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ErrorFunctionApproximation {
    class Program {
        static void Main(string[] args) {
            MultiPrecision<Pow2.N32> dx = 1d / 1024;

            using StreamWriter sw = new("../../../../results/series_inverf_e20.txt");
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
                
                sw.WriteLine($"{d},{dy:e20}");
            }
            sw.Flush();

            //sw.WriteLine("pade results");
            //bool is_finished = false;
            //
            //for (int m = 4; m <= 64 && !is_finished; m++) {
            //    for (int n = m - 1; n <= m + 1 && m + n < 128; n++) {
            //        (MultiPrecision<Pow2.N32>[] ms, MultiPrecision<Pow2.N32>[] ns) =
            //            PadeSolver<Pow2.N32>.Solve(diffs.Take(m + n + 1).ToArray(), m, n);
            //
            //        MultiPrecision<Pow2.N32> err = 0;
            //        for ((int i, MultiPrecision<Pow2.N32> x) = (0, x0); i < expecteds.Count; i++, x += dx) {
            //            MultiPrecision<Pow2.N32> expected = expecteds[i];
            //            MultiPrecision<Pow2.N32> actual = PadeSolver<Pow2.N32>.Approx(MultiPrecision<Pow2.N32>.Square(x - x0), ms, ns);
            //
            //            err = MultiPrecision<Pow2.N32>.Max(err, MultiPrecision<Pow2.N32>.Abs(expected / actual - 1));
            //        }
            //
            //        Console.WriteLine($"m={m},n={n}");
            //        Console.WriteLine($"{err:e20}");
            //
            //        if (err < 1e-32) {
            //            sw.WriteLine($"m={m},n={n}");
            //            sw.WriteLine("ms");
            //            for (int i = 0; i < ms.Length; i++) {
            //                sw.WriteLine($"{(ms[i] * c):e40}");
            //            }
            //            sw.WriteLine("ns");
            //            for (int i = 0; i < ns.Length; i++) {
            //                sw.WriteLine($"{ns[i]:e40}");
            //            }
            //            sw.WriteLine("relative err");
            //            sw.WriteLine($"{err:e20}");
            //            sw.Flush();
            //            
            //            is_finished = true;
            //        }
            //    }
            //}

            Console.WriteLine("END");
            Console.Read();
        }
    }
}