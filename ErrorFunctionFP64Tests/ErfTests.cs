using ErrorFunctionFP64;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;

namespace ErrorFunctionFP64Tests {
    [TestClass()]
    public class ErfTests {
        [TestMethod()]
        public void ErfExpectedValueTest() {
            for (double x = -27.25; x <= 27.25; x += 1d / 8192) {
                double actual = ErrorFunction.Erf(x);
                MultiPrecision<Pow2.N8> expetected = MultiPrecision<Pow2.N8>.Erf(x);

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
        public void ErfSpecialValueTest() {
            Assert.AreEqual(+1.0, ErrorFunction.Erf(double.PositiveInfinity), "x = +inf");
            Assert.AreEqual(-1.0, ErrorFunction.Erf(double.NegativeInfinity), "x = -inf");
            Assert.AreEqual(+1.0, ErrorFunction.Erf(+27.25), "x = +27.25");
            Assert.AreEqual(-1.0, ErrorFunction.Erf(-27.25), "x = -27.25");
            Assert.IsTrue(double.IsNaN(ErrorFunction.Erf(double.NaN)), "x = nan");
        }
    }
}