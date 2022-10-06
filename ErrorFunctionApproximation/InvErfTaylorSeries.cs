using MultiPrecision;
using System;
using System.Collections.Generic;

namespace ErrorFunctionApproximation {
    public static class InvErfTaylorSeries {
        private static readonly List<Fraction> coefs = new() {
            1, 1
        };

        public static MultiPrecision<N> Coef<N>(int n) where N : struct, IConstant {
            if (n < 0) {
                throw new ArgumentOutOfRangeException(nameof(n));
            }

            for (int k = coefs.Count; k <= n; k++) {
                Fraction coef = 0;

                for (int m = 0; m < k; m++) {
                    coef += coefs[m] * coefs[k - m - 1] / ((m + 1) * (2 * m + 1));
                }
                
                coefs.Add(coef);
            }

            return MultiPrecision<N>.Div(coefs[n].Numer, coefs[n].Denom) / (2 * n + 1)
                * MultiPrecision<N>.Pow(MultiPrecision<N>.Sqrt(MultiPrecision<N>.PI) / 2, 2 * n + 1);
        }
    }
}
