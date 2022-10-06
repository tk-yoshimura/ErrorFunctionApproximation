using ErrorFunctionFP64;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;

namespace ErrorFunctionFP64Tests {
    [TestClass()]
    public class InverseErfTests {
        [TestMethod()]
        public void InverseErfExpectedValueTest() {
            for (double x = Math.ScaleB(1, -1000); x < 1; x *= 2) {
                double actual = ErrorFunction.InverseErf(x);
                MultiPrecision<Pow2.N8> expetected = MultiPrecision<Pow2.N8>.InverseErf(x);

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

            for (double x = -65535d / 65536; x <= 65535d / 65536; x += 1d / 65536) {
                double actual = ErrorFunction.InverseErf(x);
                MultiPrecision<Pow2.N8> expetected = MultiPrecision<Pow2.N8>.InverseErf(x);

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
        public void InverseErfSpecialValueTest() {
            Assert.IsTrue(double.IsNegativeInfinity(ErrorFunction.InverseErf(-1)), "x = -1");
            Assert.IsTrue(double.IsPositiveInfinity(ErrorFunction.InverseErf(+1)), "x = +1");

            Assert.IsTrue(double.IsNaN(ErrorFunction.InverseErf(-2)), "x = -2");
            Assert.IsTrue(double.IsNaN(ErrorFunction.InverseErf(+2)), "x = +2");

            Assert.IsTrue(double.IsNaN(ErrorFunction.InverseErf(Math.BitDecrement(-1))), "x = -1-eps");
            Assert.IsTrue(double.IsNaN(ErrorFunction.InverseErf(Math.BitIncrement(+1))), "x = +1+eps");

            Assert.IsTrue(double.IsFinite(ErrorFunction.InverseErf(Math.BitIncrement(-1))), "x = -1+eps");
            Assert.IsTrue(double.IsFinite(ErrorFunction.InverseErf(Math.BitDecrement(+1))), "x = +1-eps");

            Assert.IsTrue(double.IsNaN(ErrorFunction.InverseErf(double.PositiveInfinity)), "x = +inf");
            Assert.IsTrue(double.IsNaN(ErrorFunction.InverseErf(double.NegativeInfinity)), "x = -inf");
            Assert.IsTrue(double.IsNaN(ErrorFunction.InverseErf(double.NaN)), "x = nan");
        }
    }
}