using System.IO;
using MultiPrecision;

namespace ErrorFunctionApproximation {
    class Program {
        static void Main(string[] args) {
            using (StreamWriter sw = new StreamWriter("../../../../results/erf_approx.csv")) {
                sw.WriteLine("z,y_true,y_approx,mp_error,double_error");

                for (int z1000 = -5 * 1000; z1000 <= 5 * 1000; z1000++) {
                    double z = z1000 / 1000.0;

                    MultiPrecision<Pow2.N4> y_true = MultiPrecision<Pow2.N4>.Erf(z);
                    double y_approx = ErrorFunction.Erf(z);
                    MultiPrecision<Pow2.N4> mp_err = y_true - y_approx;
                    double double_err = (double)y_true - y_approx;

                    sw.WriteLine($"{z},{y_true},{y_approx},{mp_err},{double_err}");
                }
            }

            using (StreamWriter sw = new StreamWriter("../../../../results/erfc_approx.csv")) {
                sw.WriteLine("z,y_true,y_approx,mp_error,double_error");

                for (int z1000 = -2 * 1000; z1000 <= 8 * 1000; z1000++) {
                    double z = z1000 / 1000.0;

                    MultiPrecision<Pow2.N4> y_true = MultiPrecision<Pow2.N4>.Erfc(z);
                    double y_approx = ErrorFunction.Erfc(z);
                    MultiPrecision<Pow2.N4> mp_err = y_true - y_approx;
                    double double_err = (double)y_true - y_approx;

                    sw.WriteLine($"{z},{y_true},{y_approx},{mp_err},{double_err}");
                }
            }

            using (StreamWriter sw = new StreamWriter("../../../../results/inverf_approx.csv")) {
                sw.WriteLine("z,y_true,y_approx,mp_error,double_error");

                for (int z10000 = -1 * 10000; z10000 <= 1 * 10000; z10000++) {
                    double z = z10000 / 10000.0;

                    MultiPrecision<Pow2.N4> y_true = MultiPrecision<Pow2.N4>.InverseErf(z);
                    double y_approx = ErrorFunction.InverseErf(z);
                    MultiPrecision<Pow2.N4> mp_err = y_true - y_approx;
                    double double_err = (double)y_true - y_approx;

                    sw.WriteLine($"{z},{y_true},{y_approx},{mp_err},{double_err}");
                }
            }

            using (StreamWriter sw = new StreamWriter("../../../../results/inverfc_approx.csv")) {
                sw.WriteLine("z,y_true,y_approx,mp_error,double_error");

                for (int z10000 = 0 * 10000; z10000 <= 2 * 10000; z10000++) {
                    double z = z10000 / 10000.0;

                    MultiPrecision<Pow2.N4> y_true = MultiPrecision<Pow2.N4>.InverseErfc(z);
                    double y_approx = ErrorFunction.InverseErfc(z);
                    MultiPrecision<Pow2.N4> mp_err = y_true - y_approx;
                    double double_err = (double)y_true - y_approx;

                    sw.WriteLine($"{z},{y_true},{y_approx},{mp_err},{double_err}");
                }
            }
        }
    }
}