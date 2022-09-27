using MultiPrecision;

namespace ErrorFunctionApproximation {
    public static class ErfConvergenseTester<N> where N : struct, IConstant {

        public static (MultiPrecision<N> y, int convergense_n) Erf(MultiPrecision<N> z) {
            MultiPrecision<Plus4<N>> z_ex = z.Convert<Plus4<N>>();
            MultiPrecision<Plus4<N>> w = z_ex * z_ex, v = w;
            MultiPrecision<Plus4<N>> c = MultiPrecision<Plus4<N>>.Ldexp(z_ex, 1) / MultiPrecision<Plus4<N>>.Sqrt(MultiPrecision<Plus4<N>>.PI);

            MultiPrecision<Plus4<N>> s = MultiPrecision<Plus4<N>>.One;

            MultiPrecision<N> s_prev = null;

            int n = 1;
            while (s_prev != s.Convert<N>()) {
                s_prev = s.Convert<N>();

                s += v / ErfTaylorSeries.Coef<Plus4<N>>(n);

                v *= w;
                n++;
            }

            return ((s * c).Convert<N>(), n - 1);
        }
    }
}
