using ErrorFunctionFP64;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;

namespace ErrorFunctionFP64Tests {
    [TestClass()]
    public class ErfcTests {
        [TestMethod()]
        public void ErfcExpectedValueTest() {
            for (double x = -27.25; x <= 27.25; x += 1d / 8192) {
                double actual = ErrorFunction.Erfc(x);
                MultiPrecision<Pow2.N8> expetected = MultiPrecision<Pow2.N8>.Erfc(x);

                if (x % 0.25 == 0) {
                    Console.WriteLine(x);
                    Console.WriteLine(actual);
                    Console.WriteLine(expetected);
                }

                if (MultiPrecision<Pow2.N8>.Abs(actual - expetected) > MultiPrecision<Pow2.N8>.Abs(expetected) * 8e-16 || !double.IsFinite(actual)) {
                    if (!double.IsNormal(actual) && !double.IsNormal((double)expetected)) {
                        Console.WriteLine("skip checking abnormal value");
                        continue;
                    }
                    Assert.Fail($"{x}\n{actual}\n{expetected}");
                }
            }
        }

        [TestMethod()]
        public void ErfcSpecialValueTest() {
            Assert.AreEqual(0.0, ErrorFunction.Erfc(double.PositiveInfinity), "x = +inf");
            Assert.AreEqual(2.0, ErrorFunction.Erfc(double.NegativeInfinity), "x = -inf");
            Assert.AreEqual(0.0, ErrorFunction.Erfc(+27.25), "x = +27.25");
            Assert.AreEqual(2.0, ErrorFunction.Erfc(-27.25), "x = -27.25");
            Assert.IsTrue(double.IsNaN(ErrorFunction.Erfc(double.NaN)), "x = nan");
        }
    }
}