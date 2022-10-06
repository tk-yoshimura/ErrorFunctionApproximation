// Copyright (c) T.Yoshimura 2022
// https://github.com/tk-yoshimura

using System.Runtime.CompilerServices;

namespace ErrorFunctionFP64 {
    public static class ErrorFunction {
        public static double Erf(double x) {
            if (double.IsNaN(x)) {
                return double.NaN;
            }
            if (x < 0.0) {
                return -Erf(Math.Abs(x));
            }

            if (x < 0.5) {
                return ErfNearZero(x);
            }
            if (x < 1.0) {
                return 1.0 - ErfcGtP5(x);
            }
            if (x < 2.0) {
                return 1.0 - ErfcGt1(x);
            }
            if (x < 4.0) {
                return 1.0 - ErfcGt2(x);
            }
            if (x < 8.0) {
                return 1.0 - ErfcGt4(x);
            }

            return 1.0;
        }

        public static double Erfc(double x) {
            if (double.IsNaN(x)) {
                return double.NaN;
            }

            if (x < 0.5) {
                return 1.0 - Erf(x);
            }
            if (x < 1.0) {
                return ErfcGtP5(x);
            }
            if (x < 2.0) {
                return ErfcGt1(x);
            }
            if (x < 4.0) {
                return ErfcGt2(x);
            }
            if (x < 8.0) {
                return ErfcGt4(x);
            }
            if (x < 16.0) {
                return ErfcGt8(x);
            }
            if (x < 27.25) {
                return ErfcGt16(x);
            }

            return 0.0;
        }

        public static double InverseErf(double x) {
            if (x < 0.0) {
                return -InverseErf(Math.Abs(x));
            }

            if (x < 0.5) {
                return InverseErfNearZero(x);
            }
            if (x < 1.0) {
                return InverseErfc(1.0 - x);
            }
            if (x == 1.0) {
                return double.PositiveInfinity;
            }

            return double.NaN;
        }

        public static double InverseErfc(double x) {
            if (x > 1.0) {
                return -InverseErfc(2.0 - x);
            }

            if (!(x >= 0)) {
                return double.NaN;
            }

            if (x > 0.5) {
                return InverseErfNearZero(1.0 - x);
            }

            int exp = Math.ILogB(x);

            if (exp >= -4) {
                return InverseErfcLtRcpBinpow1(x);
            }
            if (exp >= -16) {
                return InverseErfcLtRcpBinpow4(x);
            }
            if (exp >= -64) {
                return InverseErfcLtRcpBinpow16(x);
            }
            if (exp >= -256) {
                return InverseErfcLtRcpBinpow64(x);
            }
            if (x > 0) {
                return InverseErfcLtRcpBinpow256(x);
            }

            return double.PositiveInfinity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double Fma(double z, double x, double y) {
            return Math.FusedMultiplyAdd(x, y, z);
        }

        private static double ErfNearZero(double x) {
            double w = x * x;

#if DEBUG
            if (!(w >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(x));
            }
#endif

            double u = Fma(
                1.12837916709551257390e0, w, Fma(
                1.41992244117798429234e-1, w, Fma(
                4.52195919383661078468e-2, w, Fma(
                1.81571926199746178752e-3, w,
                1.92899332746423909221e-4))))
                / Fma(
                1.00000000000000000000e0, w, Fma(
                4.59170683275987299444e-1, w, Fma(
                9.31317143590956177199e-2, w, Fma(
                1.05454995673356468502e-2, w, Fma(
                6.75953355582711700711e-4, w,
                1.99751561392946360834e-5)))));

            double y = x * u;

            return y;
        }

        private static double ErfcGtP5(double x) {
            double w = x - 0.5;

#if DEBUG
            if (!(w >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(x));
            }
#endif

            double u = Fma(
                1.00000000000000000000e0, w, Fma(
                1.34099976999121329590e0, w, Fma(
                8.56762978144288861443e-1, w, Fma(
                3.16570587181473309350e-1, w, Fma(
                7.05798099110973092777e-2, w, Fma(
                8.92998004186820217446e-3, w,
                5.00239328430236539100e-4))))))
                / Fma(
                1.62419308574807066971e0, w, Fma(
                3.53051729947024079596e0, w, Fma(
                3.38347446398070396153e0, w, Fma(
                1.85669816101497046144e0, w, Fma(
                6.31463529762465612180e-1, w, Fma(
                1.33441854070762465425e-1, w, Fma(
                1.62726723432911469379e-2, w,
                8.86583249851894459219e-4)))))));

            double y = Math.Exp(-x * x) * u;

            return y;
        }

        private static double ErfcGt1(double x) {
            double w = x - 1.0;

#if DEBUG
            if (!(w >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(x));
            }
#endif

            double u = Fma(
                1.00000000000000000000e0, w, Fma(
                1.50314224721654127642e0, w, Fma(
                1.04476336774122843193e0, w, Fma(
                4.26889879050711362051e-1, w, Fma(
                1.09935724575582226688e-1, w, Fma(
                1.77735261175907078409e-2, w, Fma(
                1.66761213238888643314e-3, w,
                7.00293677081427709050e-5)))))))
                / Fma(
                2.33872406651000647661e0, w, Fma(
                5.00980365221224034376e0, w, Fma(
                4.80015965515186399462e0, w, Fma(
                2.69016407758612841845e0, w, Fma(
                9.65783251517281689174e-1, w, Fma(
                2.27773183728755448353e-1, w, Fma(
                3.45207187382309254048e-2, w, Fma(
                3.07987987255160486906e-3, w,
                1.24124178972110154556e-4))))))));

            double y = Math.Exp(-x * x) * u;

            return y;
        }

        private static double ErfcGt2(double x) {
            double w = x - 2.0;

#if DEBUG
            if (!(w >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(x));
            }
#endif

            double u = Fma(
                1.00000000000000000000e0, w, Fma(
                1.54789172892709681253e0, w, Fma(
                1.08732691359437197452e0, w, Fma(
                4.50760649484611848536e-1, w, Fma(
                1.20340580111242787211e-1, w, Fma(
                2.11598919778364788400e-2, w, Fma(
                2.39169506541366076969e-3, w, Fma(
                1.58871052785301256315e-4, w,
                4.75003546471038792860e-6))))))))
                / Fma(
                3.91549306725230897235e0, w, Fma(
                7.69806507033900444546e0, w, Fma(
                6.83556849290924291721e0, w, Fma(
                3.60033851351004657099e0, w, Fma(
                1.24047612045121162177e0, w, Fma(
                2.90158593990291689591e-1, w, Fma(
                4.61156483147089573007e-2, w, Fma(
                4.80656185549192240090e-3, w, Fma(
                2.98430051654960595459e-4, w,
                8.41921851559063070821e-6)))))))));

            double y = Math.Exp(-x * x) * u;

            return y;
        }

        private static double ErfcGt4(double x) {
            double w = x - 4.0;

#if DEBUG
            if (!(w >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(x));
            }
#endif

            double u = Fma(
                1.00000000000000000000e0, w, Fma(
                1.25746170311257204438e0, w, Fma(
                7.03808332270199051823e-1, w, Fma(
                2.28892390212618636873e-1, w, Fma(
                4.72951835938663487710e-2, w, Fma(
                6.35712058181220292220e-3, w, Fma(
                5.42839661005351321710e-4, w, Fma(
                2.69272815311508576646e-5, w,
                5.94216970374386786069e-7))))))))
                / Fma(
                7.29929897048781762900e0, w, Fma(
                1.09039745244909594419e1, w, Fma(
                7.31699851898786526319e0, w, Fma(
                2.89571228902039492291e0, w, Fma(
                7.45055426257682045346e-1, w, Fma(
                1.29292865258882582078e-1, w, Fma(
                1.51380930848769294948e-2, w, Fma(
                1.15359431397539239090e-3, w, Fma(
                5.19402524758985255038e-5, w,
                1.05322215737521862732e-6)))))))));

            double y = Math.Exp(-x * x) * u;

            return y;
        }

        private static double ErfcGt8(double x) {
            double w = x - 8.0;

#if DEBUG
            if (!(w >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(x));
            }
#endif

            double u = Fma(
                1.00000000000000000000e0, w, Fma(
                6.48274799208145354401e-1, w, Fma(
                1.76503869655751721640e-1, w, Fma(
                2.58334208322351492685e-2, w, Fma(
                2.14371252130922470978e-3, w, Fma(
                9.56306077897485124122e-5, w,
                1.79178792352027294607e-6))))))
                / Fma(
                1.42887422333136168339e1, w, Fma(
                1.10222480155706011853e1, w, Fma(
                3.66405698556123833673e0, w, Fma(
                6.80475185385687115344e-1, w, Fma(
                7.62576453195300656087e-2, w, Fma(
                5.15722615664563702995e-3, w, Fma(
                1.94907730282247604012e-4, w,
                3.17586140504182968574e-6)))))));

            double y = Math.Exp(-x * x) * u;

            return y;
        }

        private static double ErfcGt16(double x) {
            double w = x - 16.0;

#if DEBUG
            if (!(w >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(x));
            }
#endif

            double u = Fma(
                1.00000000000000000000e0, w, Fma(
                3.01131564001861798828e-1, w, Fma(
                3.63647817146354655538e-2, w, Fma(
                2.20132258260920223731e-3, w, Fma(
                6.67982674643708596687e-5, w,
                8.12863261006534035097e-7)))))
                / Fma(
                2.84144365162813186597e1, w, Fma(
                1.03255156563650534155e1, w, Fma(
                1.56620755225060452355e0, w, Fma(
                1.26930452727791373837e-1, w, Fma(
                5.79681261235647022363e-3, w, Fma(
                1.41449048276726826659e-4, w,
                1.44076261723069040035e-6))))));

            double y = Math.Exp(-x * x) * u;

            return y;
        }

        private static double InverseErfNearZero(double x) {
            double w = x * x;

#if DEBUG
            if (!(w >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(x));
            }
#endif

            double u = Fma(
                8.86226925452758013649e-1, w, Fma(
                2.32013666534654493554e-1, w, Fma(
                1.27556175305597958254e-1, w, Fma(
                8.65521292415475337296e-2, w, Fma(
                6.49596177453854133820e-2, w, Fma(
                5.17312819846163741126e-2, w, Fma(
                4.28367206517973498447e-2, w, Fma(
                3.64659293085316263256e-2, w, Fma(
                3.16890050216054468096e-2, w, Fma(
                2.79806329649952247334e-2, w, Fma(
                2.50222758411983494572e-2, w, Fma(
                2.26098633188975744328e-2, w, Fma(
                2.06067803790590017188e-2, w, Fma(
                1.89182172507788544635e-2, w, Fma(
                1.74763705628565461904e-2, w, Fma(
                1.62315009876852512753e-2, w, Fma(
                1.51463150632478055204e-2, w, Fma(
                1.41923160025099641512e-2, w, Fma(
                1.33473641974212971503e-2, w, Fma(
                1.25940048713320698416e-2, w, Fma(
                1.19182959363920398739e-2, w, Fma(
                1.13089701059225367722e-2, w, Fma(
                1.07568253033179575778e-2, w,
                1.02542740818534682141e-2)))))))))))))))))))))));

            double y = x * u;

            return y;
        }

        private static double InverseErfcLtRcpBinpow1(double x) {
            double w = Math.Sqrt(-Math.Log2(x)) - 1.0;

#if DEBUG
            if (!(w >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(x));
            }
#endif

            double y = Fma(
                4.76936276204469873381e-1, w, Fma(
                2.84616760158405525682e0, w, Fma(
                7.83497820480967952200e0, w, Fma(
                1.31854418507247069612e1, w, Fma(
                1.50991314347931481408e1, w, Fma(
                1.23622587209910180023e1, w, Fma(
                7.38914611882483323616e0, w, Fma(
                3.22887927743532400543e0, w, Fma(
                1.01469022581043355024e0, w, Fma(
                2.20662675054627813944e-1, w, Fma(
                3.08544968974593494673e-2, w, Fma(
                2.39971790811111452922e-3, w,
                7.30863745742286909984e-5))))))))))))
                / Fma(
                1.00000000000000000000e0, w, Fma(
                4.35064949911730838463e0, w, Fma(
                9.11050028775966939545e0, w, Fma(
                1.19065638260552095590e1, w, Fma(
                1.06774569435004386796e1, w, Fma(
                6.83563640086112993386e0, w, Fma(
                3.15858388923513427185e0, w, Fma(
                1.04190005305890918793e0, w, Fma(
                2.36890807337000398359e-1, w, Fma(
                3.45535255003216715674e-2, w, Fma(
                2.79593756760869554701e-3, w,
                8.77716242076543178347e-5)))))))))));

            return y;
        }

        private static double InverseErfcLtRcpBinpow4(double x) {
            double w = Math.Sqrt(-Math.Log2(x)) - 2.0;

#if DEBUG
            if (!(w >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(x));
            }
#endif

            double y = Fma(
                1.31715033498613074888e0, w, Fma(
                5.24458754775368177301e0, w, Fma(
                9.40160525891638988553e0, w, Fma(
                1.00202034129966156769e1, w, Fma(
                7.05691179492779932518e0, w, Fma(
                3.44983677152287023345e0, w, Fma(
                1.19599008643785375871e0, w, Fma(
                2.94950265045921950933e-1, w, Fma(
                5.10782854503079662255e-2, w, Fma(
                6.01441083880282118134e-3, w, Fma(
                4.51658698995963805678e-4, w, Fma(
                1.89285431743474708101e-5, w,
                3.15388752170395768576e-7))))))))))))
                / Fma(
                1.00000000000000000000e0, w, Fma(
                3.32088314086107720588e0, w, Fma(
                4.93633069582100752978e0, w, Fma(
                4.33068270770402851695e0, w, Fma(
                2.48425031004238464589e0, w, Fma(
                9.74405384109905296811e-1, w, Fma(
                2.65317103733908307675e-1, w, Fma(
                4.98465716049986284626e-2, w, Fma(
                6.28359357767144839905e-3, w, Fma(
                4.99944887548374135709e-4, w, Fma(
                2.19811733960022333360e-5, w,
                3.78801125171414199195e-7)))))))))));

            return y;
        }

        private static double InverseErfcLtRcpBinpow16(double x) {
            double w = Math.Sqrt(-Math.Log2(x)) - 4.0;

#if DEBUG
            if (!(w >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(x));
            }
#endif

            double y = Fma(
                3.05817958185091609017e0, w, Fma(
                5.46058005151837044625e0, w, Fma(
                4.35044043825330455239e0, w, Fma(
                2.03457880733478547851e0, w, Fma(
                6.17858183289348088999e-1, w, Fma(
                1.27154967916683733738e-1, w, Fma(
                1.79441318823134651626e-2, w, Fma(
                1.71580775626225814677e-3, w, Fma(
                1.07077990129698360247e-4, w, Fma(
                4.04893243746801509290e-6, w, Fma(
                8.02885982996411575279e-8, w,
                5.93446257127705509834e-10)))))))))))
                / Fma(
                1.00000000000000000000e0, w, Fma(
                1.50292120903908680231e0, w, Fma(
                9.98944861452710271053e-1, w, Fma(
                3.84644928772479066794e-1, w, Fma(
                9.43673024883845966012e-2, w, Fma(
                1.52697801446623291466e-2, w, Fma(
                1.62876063962271824041e-3, w, Fma(
                1.10951078743598768503e-4, w, Fma(
                4.49200199007734187298e-6, w, Fma(
                9.35878439223674540264e-8, w,
                7.12795267975690158827e-10))))))))));

            return y;
        }

        private static double InverseErfcLtRcpBinpow64(double x) {
            double w = Math.Sqrt(-Math.Log2(x)) - 8.0;

#if DEBUG
            if (!(w >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(x));
            }
#endif

            double y = Fma(
                6.47377031042129186999e0, w, Fma(
                5.34838994550346293756e0, w, Fma(
                1.93053468898855702476e0, w, Fma(
                3.98757267856051660596e-1, w, Fma(
                5.18342899619842162950e-2, w, Fma(
                4.39089367057022013391e-3, w, Fma(
                2.42536565664157596353e-4, w, Fma(
                8.48414085297905990337e-6, w, Fma(
                1.75671488734063438897e-7, w, Fma(
                1.87601473521508687333e-9, w,
                7.39959302337396811314e-12))))))))))
                / Fma(
                1.00000000000000000000e0, w, Fma(
                6.95375728953307536842e-1, w, Fma(
                2.07447955889175560264e-1, w, Fma(
                3.45772906527347126853e-2, w, Fma(
                3.51323935344692382562e-3, w, Fma(
                2.22693855216122866386e-4, w, Fma(
                8.64983969934989776423e-6, w, Fma(
                1.93587604401181576517e-7, w, Fma(
                2.18223823643588797025e-9, w,
                8.88779432554651766675e-12)))))))));

            return y;
        }

        private static double InverseErfcLtRcpBinpow256(double x) {
            double w = Math.Sqrt(-Math.Log2(x)) - 16.0;

#if DEBUG
            if (!(w >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(x));
            }
#endif

            double y = Fma(
                1.32018977499982715476e1, w, Fma(
                4.95914307095240821650e0, w, Fma(
                8.04204788972377532769e-1, w, Fma(
                7.35471062100918840625e-2, w, Fma(
                4.15519031019905868403e-3, w, Fma(
                1.49223223006883557180e-4, w, Fma(
                3.37090975197484553464e-6, w, Fma(
                4.55211778144805056604e-8, w, Fma(
                3.26501597461293127931e-10, w,
                9.17443940594623928761e-13)))))))))
                / Fma(
                1.00000000000000000000e0, w, Fma(
                3.12188066754441996038e-1, w, Fma(
                4.11261021505214135376e-2, w, Fma(
                2.96638537473187160901e-3, w, Fma(
                1.27047628192346500146e-4, w, Fma(
                3.27129851735830803895e-6, w, Fma(
                4.86893741017571186056e-8, w, Fma(
                3.74538785452651599674e-10, w, Fma(
                1.10195988093709126449e-12, w,
                3.65919249071928830347e-21)))))))));

            return y;
        }
    }
}