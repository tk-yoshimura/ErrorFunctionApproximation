using ErrorFunctionFP64;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;

namespace ErrorFunctionFP64Tests {
    [TestClass()]
    public class ErrorSummary {
        [TestMethod()]
        public void PlotErf() {
            using StreamWriter sw = new("../../../../results/erf_approx.csv");

            sw.WriteLine("x,y_expected,y_actual,error(abs),error(rate)");

            MultiPrecision<Pow2.N8> maxrateerror = 0;

            for (double x = -10; x <= 10; x += 1d / 1024) {
                double actual = ErrorFunction.Erf((double)x);
                MultiPrecision<Pow2.N8> expetected = MultiPrecision<Pow2.N8>.Erf(x);
                MultiPrecision<Pow2.N8> error = MultiPrecision<Pow2.N8>.Abs(expetected - actual);
                MultiPrecision<Pow2.N8> rateerror = (error != 0) ? error / MultiPrecision<Pow2.N8>.Abs(expetected) : 0;

                if (double.IsNormal(actual)) {
                    maxrateerror = MultiPrecision<Pow2.N8>.Max(rateerror, maxrateerror);
                }

                sw.WriteLine($"{x},{expetected:e20},{actual},{error:e8},{rateerror:e8}");
            }

            Console.WriteLine($"max error (rate): {maxrateerror:e8}");
        }

        [TestMethod()]
        public void PlotErfc() {
            using StreamWriter sw = new("../../../../results/erfc_approx.csv");

            sw.WriteLine("x,y_expected,y_actual,error(abs),error(rate)");

            MultiPrecision<Pow2.N8> maxrateerror = 0;

            for (double x = -10; x <= 30; x += 1d / 1024) {
                double actual = ErrorFunction.Erfc((double)x);
                MultiPrecision<Pow2.N8> expetected = MultiPrecision<Pow2.N8>.Erfc(x);
                MultiPrecision<Pow2.N8> error = MultiPrecision<Pow2.N8>.Abs(expetected - actual);
                MultiPrecision<Pow2.N8> rateerror = (error != 0) ? error / MultiPrecision<Pow2.N8>.Abs(expetected) : 0;

                if (double.IsNormal(actual)) {
                    maxrateerror = MultiPrecision<Pow2.N8>.Max(rateerror, maxrateerror);
                }

                sw.WriteLine($"{x},{expetected:e20},{actual},{error:e8},{rateerror:e8}");
            }

            Console.WriteLine($"max error (rate): {maxrateerror:e8}");
        }

        [TestMethod()]
        public void PlotInverseErf() {
            using StreamWriter sw = new("../../../../results/inverf_approx.csv");

            sw.WriteLine("x,y_expected,y_actual,error(abs),error(rate)");

            MultiPrecision<Pow2.N8> maxrateerror = 0;

            for (double x = -1023d / 1024; x < 1; x += 1d / 1024) {
                double actual = ErrorFunction.InverseErf((double)x);
                MultiPrecision<Pow2.N8> expetected = MultiPrecision<Pow2.N8>.InverseErf(x);
                MultiPrecision<Pow2.N8> error = MultiPrecision<Pow2.N8>.Abs(expetected - actual);
                MultiPrecision<Pow2.N8> rateerror = (error != 0) ? error / MultiPrecision<Pow2.N8>.Abs(expetected) : 0;

                if (double.IsNormal(actual)) {
                    maxrateerror = MultiPrecision<Pow2.N8>.Max(rateerror, maxrateerror);
                }

                sw.WriteLine($"{x},{expetected:e20},{actual},{error:e8},{rateerror:e8}");
            }

            Console.WriteLine($"max error (rate): {maxrateerror:e8}");
        }

        [TestMethod()]
        public void PlotInverseErfc() {
            using StreamWriter sw = new("../../../../results/inverfc_approx.csv");

            sw.WriteLine("x,y_expected,y_actual,error(abs),error(rate)");

            MultiPrecision<Pow2.N8> maxrateerror = 0;

            for (double x = Math.ScaleB(1, -1024); x < Math.ScaleB(1, -10); x *= 2) {
                double actual = ErrorFunction.InverseErfc((double)x);
                MultiPrecision<Pow2.N8> expetected = MultiPrecision<Pow2.N8>.InverseErfc(x);
                MultiPrecision<Pow2.N8> error = MultiPrecision<Pow2.N8>.Abs(expetected - actual);
                MultiPrecision<Pow2.N8> rateerror = (error != 0) ? error / MultiPrecision<Pow2.N8>.Abs(expetected) : 0;

                if (double.IsNormal(actual)) {
                    maxrateerror = MultiPrecision<Pow2.N8>.Max(rateerror, maxrateerror);
                }

                sw.WriteLine($"{x},{expetected:e20},{actual},{error:e8},{rateerror:e8}");
            }

            for (double x = 1d / 1024; x < 2; x += 1d / 1024) {
                double actual = ErrorFunction.InverseErfc((double)x);
                MultiPrecision<Pow2.N8> expetected = MultiPrecision<Pow2.N8>.InverseErfc(x);
                MultiPrecision<Pow2.N8> error = MultiPrecision<Pow2.N8>.Abs(expetected - actual);
                MultiPrecision<Pow2.N8> rateerror = (error != 0) ? error / MultiPrecision<Pow2.N8>.Abs(expetected) : 0;

                if (double.IsNormal(actual)) {
                    maxrateerror = MultiPrecision<Pow2.N8>.Max(rateerror, maxrateerror);
                }

                sw.WriteLine($"{x},{expetected:e20},{actual},{error:e8},{rateerror:e8}");
            }

            Console.WriteLine($"max error (rate): {maxrateerror:e8}");
        }
    }
}