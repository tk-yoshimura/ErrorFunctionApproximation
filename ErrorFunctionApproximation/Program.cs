using System;
using System.IO;
using MultiPrecision;
using MultiPrecision.ParameterSearchUtil;

namespace ErrorFunctionApproximation {
    class Program {
        static void Main(string[] args) {
            using StreamWriter sw_convergence = new StreamWriter("../../../../results/convergence.csv");
            using StreamWriter sw_neighbors = new StreamWriter("../../../../results/neighbors.txt");
            using StreamWriter sw_values = new StreamWriter("../../../../results/values.txt");

            sw_convergence.WriteLine("z,bits,convergence_n");

            for (int z4 = 8; z4 <= 400; z4++) {
                decimal z = ((decimal)z4) / 4;

                Console.WriteLine($"z:{z}");

                long n = 4;

                n = ErfcConvergence<Pow2.N4>(z4, n);
                Plot<Pow2.N4>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Expand25<Pow2.N4>>(z4, n);
                Plot<Expand25<Pow2.N4>>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Expand50<Pow2.N4>>(z4, n);
                Plot<Expand50<Pow2.N4>>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Expand75<Pow2.N4>>(z4, n);
                Plot<Expand75<Pow2.N4>>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Pow2.N8>(z4, n);
                Plot<Pow2.N8>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Expand25<Pow2.N8>>(z4, n);
                Plot<Expand25<Pow2.N8>>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Expand50<Pow2.N8>>(z4, n);
                Plot<Expand50<Pow2.N8>>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Expand75<Pow2.N8>>(z4, n);
                Plot<Expand75<Pow2.N8>>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Pow2.N16>(z4, n);
                Plot<Pow2.N16>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Expand25<Pow2.N16>>(z4, n);
                Plot<Expand25<Pow2.N16>>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Expand50<Pow2.N16>>(z4, n);
                Plot<Expand50<Pow2.N16>>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Expand75<Pow2.N16>>(z4, n);
                Plot<Expand75<Pow2.N16>>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Pow2.N32>(z4, n);
                Plot<Pow2.N32>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Expand25<Pow2.N32>>(z4, n);
                Plot<Expand25<Pow2.N32>>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Expand50<Pow2.N32>>(z4, n);
                Plot<Expand50<Pow2.N32>>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Expand75<Pow2.N32>>(z4, n);
                Plot<Expand75<Pow2.N32>>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Pow2.N64>(z4, n);
                Plot<Pow2.N64>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Expand25<Pow2.N64>>(z4, n);
                Plot<Expand25<Pow2.N64>>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Expand50<Pow2.N64>>(z4, n);
                Plot<Expand50<Pow2.N64>>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Expand75<Pow2.N64>>(z4, n);
                Plot<Expand75<Pow2.N64>>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Pow2.N128>(z4, n);
                Plot<Pow2.N128>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Expand25<Pow2.N128>>(z4, n);
                Plot<Expand25<Pow2.N128>>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Expand50<Pow2.N128>>(z4, n);
                Plot<Expand50<Pow2.N128>>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Expand75<Pow2.N128>>(z4, n);
                Plot<Expand75<Pow2.N128>>(sw_convergence, sw_neighbors, sw_values, z4, n);

                n = ErfcConvergence<Pow2.N256>(z4, n);
                Plot<Pow2.N256>(sw_convergence, sw_neighbors, sw_values, z4, n);
            }

            Console.WriteLine("END");
            Console.Read();
        }

        private static void Plot<N>(StreamWriter sw_convergence, StreamWriter sw_neighbors, StreamWriter sw_values, long z4, long n) where N : struct, IConstant {
            MultiPrecision<N> z = ((MultiPrecision<N>)z4) / 4;

            sw_convergence.WriteLine($"{z},{MultiPrecision<N>.Bits},{n}");

            for (long i = Math.Max(-n + 1, -4); i <= 3; i++) {
                sw_neighbors.WriteLine($"z:{z} bits:{MultiPrecision<N>.Bits} n:{n + i}");
                sw_neighbors.WriteLine($"{ErfcContinuedFraction<N>.Erfc(z, n + i)}");
                sw_neighbors.WriteLine($"{ErfcContinuedFraction<N>.Erfc(z, n + i).ToHexcode()}");
            }

            sw_values.WriteLine($"z:{z} bits:{MultiPrecision<N>.Bits} n:{n}");
            sw_values.WriteLine($"{ErfcContinuedFraction<N>.Erfc(z, n)}");
            sw_values.WriteLine($"{ErfcContinuedFraction<N>.Erfc(z, n).ToHexcode()}");

            sw_convergence.Flush();
            sw_neighbors.Flush();
            sw_values.Flush();
        }

        static long ErfcConvergence<N>(int z4, long n_init) where N : struct, IConstant {
            Console.WriteLine($"bits:{MultiPrecision<N>.Bits}");

            MultiPrecision<N> z = ((MultiPrecision<N>)z4) / 4;

            ConvergenceSearch<N> searcher = new ConvergenceSearch<N>((n_init, n_init * 2), (n_init, long.MaxValue));

            while (!searcher.IsSearched) {
                foreach (long n in searcher.SampleRequests) {
                    MultiPrecision<N> y = ErfcContinuedFraction<N>.Frac(z, n);

                    searcher.PushSampleResult(n, y);
                }

                Console.WriteLine($"step {searcher.Step}, convpoint {searcher.ConvergencePoint}");
                searcher.ReflashSampleRequests();
            }

            return searcher.ConvergencePoint;
        } 
    }
}