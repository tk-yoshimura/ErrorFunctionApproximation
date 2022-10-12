using ErrorFunctionFP64;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;

namespace ErrorFunctionFP64Tests {
    [TestClass()]
    public class InverseErfcTests {
        [TestMethod()]
        public void InverseErfcExpectedValueTest() {
            for (double x = Math.ScaleB(1, -1024); x <= 1; x *= 2) {
                double actual = ErrorFunction.InverseErfc(x);
                MultiPrecision<Pow2.N8> expetected = MultiPrecision<Pow2.N8>.InverseErfc(x);

                if (Math.ILogB(x) % 4 == 0) {
                    Console.WriteLine(x);
                    Console.WriteLine(actual);
                    Console.WriteLine(expetected);
                }

                if (MultiPrecision<Pow2.N8>.Abs(actual - expetected) > MultiPrecision<Pow2.N8>.Abs(expetected) * 6e-16 || !double.IsFinite(actual)) {
                    if (!double.IsNormal(actual) && !double.IsNormal((double)expetected)) {
                        Console.WriteLine("skip checking abnormal value");
                        continue;
                    }
                    Assert.Fail($"{x}\n{actual}\n{expetected}");
                }
            }

            for (double x = 1d / 65536; x <= 131071d / 65536; x += 1d / 65536) {
                double actual = ErrorFunction.InverseErfc(x);
                MultiPrecision<Pow2.N8> expetected = MultiPrecision<Pow2.N8>.InverseErfc(x);

                if (x % 0.25 == 0) {
                    Console.WriteLine(x);
                    Console.WriteLine(actual);
                    Console.WriteLine(expetected);
                }

                if (MultiPrecision<Pow2.N8>.Abs(actual - expetected) > MultiPrecision<Pow2.N8>.Abs(expetected) * 6e-16 || !double.IsFinite(actual)) {
                    if (!double.IsNormal(actual) && !double.IsNormal((double)expetected)) {
                        Console.WriteLine("skip checking abnormal value");
                        continue;
                    }
                    Assert.Fail($"{x}\n{actual}\n{expetected}");
                }
            }
        }

        [TestMethod()]
        public void InverseErfcSpecialValueTest() {
            Assert.IsTrue(double.IsPositiveInfinity(ErrorFunction.InverseErfc(0)), "x = 0");
            Assert.IsTrue(double.IsNegativeInfinity(ErrorFunction.InverseErfc(2)), "x = 2");

            Assert.IsTrue(double.IsNaN(ErrorFunction.InverseErfc(-1)), "x = -1");
            Assert.IsTrue(double.IsNaN(ErrorFunction.InverseErfc(3)), "x = 3");

            Assert.IsTrue(double.IsNaN(ErrorFunction.InverseErfc(Math.BitDecrement(0))), "x = 0-eps");
            Assert.IsTrue(double.IsNaN(ErrorFunction.InverseErfc(Math.BitIncrement(2))), "x = 2+eps");

            Assert.IsTrue(double.IsFinite(ErrorFunction.InverseErfc(Math.BitIncrement(0))), "x = 0+eps");
            Assert.IsTrue(double.IsFinite(ErrorFunction.InverseErfc(Math.BitDecrement(2))), "x = 2-eps");

            Assert.IsTrue(double.IsNaN(ErrorFunction.InverseErf(double.PositiveInfinity)), "x = +inf");
            Assert.IsTrue(double.IsNaN(ErrorFunction.InverseErf(double.NegativeInfinity)), "x = -inf");
            Assert.IsTrue(double.IsNaN(ErrorFunction.InverseErf(double.NaN)), "x = nan");
        }

        [TestMethod()]
        public void InverseErfcValuePlotTest() {
            Console.WriteLine(ErrorFunction.InverseErfc(1d / 10));
            Console.WriteLine(ErrorFunction.InverseErfc(1d / 100));
            Console.WriteLine(ErrorFunction.InverseErfc(1d / 1000));
            Console.WriteLine(ErrorFunction.InverseErfc(1d / 10000));
        }
    }
}