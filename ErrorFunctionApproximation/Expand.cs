using MultiPrecision;

namespace ErrorFunctionApproximation {
    internal struct Plus4<N> : IConstant where N : struct, IConstant {
        public int Value => checked(default(N).Value + 4);
    }

    internal struct Expand25<N> : IConstant where N : struct, IConstant {
        public int Value => checked(default(N).Value * 5 / 4);
    }

    internal struct Expand50<N> : IConstant where N : struct, IConstant {
        public int Value => checked(default(N).Value * 3 / 2);
    }

    internal struct Expand75<N> : IConstant where N : struct, IConstant {
        public int Value => checked(default(N).Value * 7 / 4);
    }

    internal struct Double<N> : IConstant where N : struct, IConstant {
        public int Value => checked(default(N).Value * 2);
    }
}
