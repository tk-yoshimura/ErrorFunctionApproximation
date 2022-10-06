# ErrorFunctionApproximation

## Definition

The error function (erf) is defined by the following equation:

![erf](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/erf.svg)

The complementary error function (erfc) is defined by the following equation:

![erfc](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/erfc.svg)

## Convergence Equation

To obtain these numerical solutions, use the following equations:

Taylor series:

![taylor series](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/taylor_series.svg)

Continued fraction expansion:

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

The number of terms required to obtain arbitrary precision is as follows:  
In addition, in order to obtain the expected value, 128-bit expansion was performed to absorb cancellation.

![erf convergence zwise](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/erf_convergence_zwise.svg)

![erf convergence bitwise](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/erf_convergence_bitswise.svg)

## Continued fraction expansion

Next, in Continued fraction expansion, when the absolute value of z is small, the convergence becomes slow.

Replace continued fractions:

![frac f1](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/fracexpand_f1.svg)

The recursive part can be written below:

![frac e2](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/fracexpand_e2.svg)

![frac e4](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/fracexpand_e4.svg)

The fixed points at n = 1 are:

![frac x2](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/fracexpand_x2.svg)

![frac x4](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/fracexpand_x4.svg)

These approximate the true values:

![frac x](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/fracexpand_x.svg)

![frac x value](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/fracexpand_x_value.svg)![frac x error](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/fracexpand_error.svg)

Solve the continued fraction F1 (z) backward with X4 (z) as the initial value.  
Let N be the recursion times (Fn+4(z) to Fn(z)), and N until convergence is as follows:

![erfc convergence zwise](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/erfc_convergence_zwise.svg)

![erfc convergence bitwise](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/erfc_convergence_bitswise.svg)

## Double Precision (IEEE 754) Approx
The calculation error is at most 2 epsilons.  
[C# code](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/ErrorFunctionFP64/ErrorFunction.cs)  

[erf result](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/results/erf_approx.csv)  
![erf result](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/erf_approx.svg)  
[erfc result](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/results/erfc_approx.csv)  
![erfc result](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/erfc_approx.svg)  
[inverse erf result](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/results/inverf_approx.csv)  
![inverse erf result](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/inverf_approx.svg)  
[inverse erfc result](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/results/inverfc_approx.csv)  
![inverse erfc result](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/inverfc_approx.svg)  

The following is used to approximate the error of machine epsilon in the entire domain.

near zero:  
![erf approx nearzero](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/erf_approx_nz.svg)

1 to 4:  
![erfc approx az](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/erfc_approx_az.svg)

![erfc az](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/erfc_az.svg)

![erfc az diff](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/erfc_diff.svg)

![pade definition](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/pade_definition.svg)  

[pade erf precision16](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/results/pade_erf_e16.txt)  
[pade erf precision32](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/results/pade_erf_e32.txt)  
[pade erfc precision16](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/results/pade_erfc_e16.txt)  
[pade erfc precision32](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/results/pade_erfc_e32.txt)  

## Inverse Function

### InverseErfc NearZero
![inverse erfc nearzero](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/figures/inverse_erfc_nz.svg)  

[pade inverfc precision16](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/results/pade_inverfc_e16.txt)  
[pade inverfc precision32](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/results/pade_inverfc_e32.txt)  

### InverseErf NearZero

[series inverf e20](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/results/series_inverf_e20.txt)  
[series inverf e40](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/results/series_inverf_e40.txt)  
[series inverf e80](https://github.com/tk-yoshimura/ErrorFunctionApproximation/blob/main/results/series_inverf_e80.txt)  