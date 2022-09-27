using MultiPrecision;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ErrorFunctionApproximation {
    public static class ErfcExpScaled<N> where N : struct, IConstant {
        private static readonly Dictionary<int, ReadOnlyCollection<(MultiPrecision<Double<N>> c, int n, int m)>> table = new() {
            { 0, new ReadOnlyCollection<(MultiPrecision<Double<N>> c, int n, int m)>(new (MultiPrecision<Double<N>> c, int n, int m)[]{ (1, 0, 1) })},
        };

        public static MultiPrecision<N> Diff(int d, MultiPrecision<N> x) {
            MultiPrecision<Double<N>> x_ex = x.Convert<Double<N>>();
            MultiPrecision<Double<N>> y = MultiPrecision<Double<N>>.Exp(-x_ex * x_ex) / MultiPrecision<Double<N>>.Erfc(x_ex);

            MultiPrecision<Double<N>> s = 0, s0, s1;

            ReadOnlyCollection<(MultiPrecision<Double<N>> c, int n, int m)> coef = Coef(d);

            int i = 0;
            for (i = 0; i < coef.Count - 1; i += 2) {
                (MultiPrecision<Double<N>> c0, int n0, int m0) = coef[i];
                (MultiPrecision<Double<N>> c1, int n1, int m1) = coef[i + 1];

                (int n, int m) = (Math.Min(n0, n1), Math.Min(m0, m1));

                if (n0 > n) {
                    s0 = c0 * MultiPrecision<Double<N>>.Pow(x_ex, n0 - n) * MultiPrecision<Double<N>>.Pow(y, m0 - m);
                }
                else {
                    s0 = c0 * MultiPrecision<Double<N>>.Pow(y, m0 - m);
                }

                if (n1 > n) {
                    s1 = c1 * MultiPrecision<Double<N>>.Pow(x_ex, n1 - n) * MultiPrecision<Double<N>>.Pow(y, m1 - m);
                }
                else {
                    s1 = c1 * MultiPrecision<Double<N>>.Pow(y, m1 - m);
                }

                s += MultiPrecision<Double<N>>.Pow(x_ex, n) * MultiPrecision<Double<N>>.Pow(y, m) * (s0 + s1);
            }

            for (; i < coef.Count; i++) {
                (MultiPrecision<Double<N>> c, int n, int m) = coef[i];

                if (n > 0) {
                    s += c * MultiPrecision<Double<N>>.Pow(x_ex, n) * MultiPrecision<Double<N>>.Pow(y, m);
                }
                else {
                    s += c * MultiPrecision<Double<N>>.Pow(y, m);
                }
            }

            return s.Convert<N>();
        }

        private static ReadOnlyCollection<(MultiPrecision<Double<N>> c, int n, int m)> Coef(int d) {
            if (d < 0) {
                throw new ArgumentOutOfRangeException(nameof(d));
            }

            if (table.ContainsKey(d)) {
                return table[d];
            }

            MultiPrecision<Double<N>> inv_sqrtpi = 1 / MultiPrecision<Double<N>>.Sqrt(MultiPrecision<Double<N>>.PI);

            Dictionary<(int n, int m), MultiPrecision<Double<N>>> t = new();

            foreach ((MultiPrecision<Double<N>> c, int n, int m) in Coef(d - 1)) {
                if (!t.ContainsKey((n, m + 1))) {
                    t.Add((n, m + 1), 0);
                }
                t[(n, m + 1)] += 2 * m * inv_sqrtpi * c;

                if (!t.ContainsKey((n + 1, m))) {
                    t.Add((n + 1, m), 0);
                }
                t[(n + 1, m)] -= 2 * m * c;

                if (n > 0) {
                    if (!t.ContainsKey((n - 1, m))) {
                        t.Add((n - 1, m), 0);
                    }
                    t[(n - 1, m)] += n * c;
                }
            }

            ReadOnlyCollection<(MultiPrecision<Double<N>> c, int n, int m)> coef = new(
                t.
                OrderByDescending((item) => item.Key.m).ThenByDescending((item) => item.Key.n).
                Select((item) => (item.Value, item.Key.n, item.Key.m)).ToArray()
            );

            table.Add(d, coef);

            return coef;
        }
    }
}
