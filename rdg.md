### Module Description ###
The rdg module implements pseudo random number generators with a
distribution. The module requires a pseudo random generator with an
uniform distribution which returns floating point random numbers with
a range of [0,1`>`. Due to the extensive use of the floating point stack,
this module has an environmental dependency.

### Module Words ###
#### Distributed Random generator structure ####
**rdg%** ( -- n )
> Get the required space for a rdg variable
#### Distributed random generator creation, initialisation and destruction ####
**rdg-init** ( x xt rdg -- )
> Initialise the generator with the random generator xt and its data x
**rdg-create** ( x xt "`<`spaces`>`name" -- ; -- rdg )
> Create a named random generator in the dictionary with the random generator xt and its data x
**rdg-new** ( x xt -- rdg )
> Create a new random generator on the heap with the random generator xt and its data x
**rdg-free** ( rdg -- )
> Free the random generator from the heap
#### Random generator words ####
**rdg-uniform** ( F: [r1](https://code.google.com/p/ffl/source/detail?r=1) [r2](https://code.google.com/p/ffl/source/detail?r=2) -- [r3](https://code.google.com/p/ffl/source/detail?r=3) ; rdg -- )
> Generate a random number with a uniform distribution in the range of [[r1](https://code.google.com/p/ffl/source/detail?r=1),[r2](https://code.google.com/p/ffl/source/detail?r=2)`>`
**rdg-normal** ( F: [r1](https://code.google.com/p/ffl/source/detail?r=1) [r2](https://code.google.com/p/ffl/source/detail?r=2) -- [r3](https://code.google.com/p/ffl/source/detail?r=3) ; rdg -- )
> Generate a random number with a normal or Gaussian distribution with mu or mean [r1](https://code.google.com/p/ffl/source/detail?r=1) and sigma or standard deviation [r2](https://code.google.com/p/ffl/source/detail?r=2)
**rdg-exponential** ( F: [r1](https://code.google.com/p/ffl/source/detail?r=1) -- [r2](https://code.google.com/p/ffl/source/detail?r=2) ; rdg -- )
> Generate a random number with an exponential distribution with mu or mean [r1](https://code.google.com/p/ffl/source/detail?r=1) [`>`0]
**rdg-gamma** ( F: [r1](https://code.google.com/p/ffl/source/detail?r=1) [r2](https://code.google.com/p/ffl/source/detail?r=2) -- [r3](https://code.google.com/p/ffl/source/detail?r=3) ; rdg -- )
> Generate a random number with a gamma distribution with alpha [r1](https://code.google.com/p/ffl/source/detail?r=1) [`>`0] and beta [r2](https://code.google.com/p/ffl/source/detail?r=2) [`>`0], alpha\*beta = mean, alpha\*beta^2 = variance
**rdg-beta** ( F: [r1](https://code.google.com/p/ffl/source/detail?r=1) [r2](https://code.google.com/p/ffl/source/detail?r=2) -- [r3](https://code.google.com/p/ffl/source/detail?r=3) ; rdg -- )
> Generate a random number with a beta distribution with alpha [r1](https://code.google.com/p/ffl/source/detail?r=1) [`>`0] and beta [r2](https://code.google.com/p/ffl/source/detail?r=2) [`>`0], alpha\*beta = mean, alpha\*beta^2 = variance
**rdg-binomial** ( F: r -- ; u1 rdg -- u2 )
> Generate a random number with a binomial distribution with probability r [0,1] and trails u1 [`>`=0]
**rdg-poisson** ( F: r -- ; rdg -- u )
> Generate a random number with a Poisson distribution with mean r [`>`=0]
**rdg-pareto** ( F: [r1](https://code.google.com/p/ffl/source/detail?r=1) [r2](https://code.google.com/p/ffl/source/detail?r=2) -- [r3](https://code.google.com/p/ffl/source/detail?r=3) ; rdg -- )
> Generate a random number with a Pareto distribution with alpha [r1](https://code.google.com/p/ffl/source/detail?r=1) [`>`0] the scale parameter and [r2](https://code.google.com/p/ffl/source/detail?r=2) [`>`0] the shape parameter
**rdg-weibull** ( F: [r1](https://code.google.com/p/ffl/source/detail?r=1) [r2](https://code.google.com/p/ffl/source/detail?r=2) -- [r3](https://code.google.com/p/ffl/source/detail?r=3) ; rdg -- )
> Generate a random number with a Weibull distribution with alpha [r1](https://code.google.com/p/ffl/source/detail?r=1) [`>`0] the scale parameter and beta [r2](https://code.google.com/p/ffl/source/detail?r=2) [`>`0] the shape parameter
### Examples ###
```
include ffl/rng.fs
include ffl/rdg.fs


\ Create a random generator variable in the dictionary with seed 5489

5489 rng-create rng1

\ Create a distributed random generator in the dictionary with the rng1 random generator
\ The distributed random generator expects the state and the word that generates random
\ numbers in the range [0..1>.

rng1 ' rng-next-float rdg-create rdg1

\ Generate a normal or gaussian random number with mean 1.0 and stddev 0.5

1E+0 0.5E+0 rdg1 rdg-normal f. cr

\ Generate an exponential random number with mean 2.0

2E+0 rdg1 rdg-exponential f. cr

\ Generate a gamma random number with alpha 2.0 and beta 0.5

2E+0 0.5E+0 rdg1 rdg-gamma f. cr

\ Generate a beta random number with alpha 2.0 and beta 0.5

2E+0 0.5E+0 rdg1 rdg-beta f. cr

\ Generate a binomial random number with probability 0.4 and trails 15

0.4E+0 15 rdg1 rdg-binomial u. cr

\ Generate a poisson random number with mean 17.0

17E+0 rdg1 rdg-poisson u. cr

\ Generate a pareto random number with alpha 3.5 and beta 2.0

3.5E+0 2E+0 rdg1 rdg-pareto f. cr

\ Generate a weibull random number with alpha 3.5 and beta 2.0

3.5E+0 2E+0 rdg1 rdg-weibull f. cr



\ Create a distributed random generator variable on the heap

rng1 ' rng-next-float rdg-new value rdg2

\ Generate an uniform random number in the range of [34.5,34.6>

34.5E+0 34.6E+0 rdg2 rdg-uniform f. cr

\ Free the variable from the heap

rdg2 rdg-free

```

---

Generated by **ofcfrth-0.10.0**