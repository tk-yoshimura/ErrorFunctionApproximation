# ErrorFunctionApproximation

## Definition

The error function is defined by the following equation:

![erf](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/erf.svg)

The complementary error function is defined by the following equation:

![erfc](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/erfc.svg)

## Convergence Equation

To obtain these numerical solutions, use the following equations:

Taylor series (|z| &le; 2.5):

![taylor series](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/taylor_series.svg)

Continued fraction expansion: (|z| &geq; 2.5):

![continued fraction expansion](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/fracexpand.svg)

## Taylor series

First, Taylor series convergence becomes slow as the absolute value of z increases.

The absolute value of each term is as follows:

![Taylor series term](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/taylor_terms.svg)

The behavior for each z:

![Taylor series convergence](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/taylor_convergence.svg)

The ratio to the next term is less than 1:

![Taylor scale](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/taylor_scale.svg)

![Taylor scale value](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/taylor_scale_value.svg)

## Continued fraction expansion

Next, in Continued fraction expansion, when the absolute value of z is small, the convergence becomes slow.

Replace continued fractions:

![frac f1](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/fracexpand_f1.svg)

The recursive part can be written below:

![frac e2](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/fracexpand_e2.svg)

![frac e4](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/fracexpand_e4.svg)

The fixed points are as follows:

![frac x2](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/fracexpand_x2.svg)

![frac x4](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/fracexpand_x4.svg)

These approximate the true values:

![frac x](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/fracexpand_x.svg)

![frac x value](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/fracexpand_x_value.svg)![frac x error](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/fracexpand_error.svg)