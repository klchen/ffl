### Module Description ###
The bar module implements a bit array.

### Module Words ###
#### Bit array structure ####
**bar%** ( -- n )
> Get the required space for a bar variable
#### Array creation, initialisation and destruction ####
**bar-init** ( +n bar -- )
> Initialise the array with length n
**bar-(free)** ( bar -- )
> Free the internal data from the heap
**bar-create** ( +n "`<`spaces`>`name" -- ; -- bar )
> Create a bit array in the dictionary with length n
**bar-new** ( n -- bar )
> Create a bit array on the heap with length n
**bar-free** ( bar -- )
> Free the array from the heap
#### Member words ####
**bar-length@** ( bar -- +n )
> Get the number of bits in the array
**bar-index?** ( n bar -- flag )
> Check if the index n is valid in the array
#### Bit array words ####
**bar^move** ( bar1 bar2 -- )
> Move bar1 into bar2
**bar^or** ( bar1 bar2 -- )
> OR the bit arrays bar1 and bar2 and store the result in bar2
**bar^and** ( bar1 bar2 -- )
> AND the bit arrays bar1 and bar2 and store the result in bar2
**bar^xor** ( bar1 bar2 -- )
> XOR the bit arrays bar1 and bar2 and store the result in bar2
#### Bit set words ####
**bar-set-bit** ( n bar -- )
> Set the nth bit in the array
**bar-set-bits** ( u n bar -- )
> Set a range of bits in the array, starting from the nth bit, u bits long
**bar-set-list** ( nu .. n1 u bar -- )
> Set n1 till nuth bits in the array
**bar-set** ( bar -- )
> Set all bits in the array
#### Bit reset words ####
**bar-reset-bit** ( n bar -- )
> Reset the nth bit
**bar-reset-bits** ( u n bar -- )
> Reset a range of bits in the array, starting from the nth bit, u bits long
**bar-reset-list** ( nu .. n1 u bar -- )
> Reset n1 till nuth bits in the array
**bar-reset** ( bar -- )
> Reset all bits in the array
#### Bit invert words ####
**bar-invert-bit** ( n bar -- )
> Invert the nth bit
**bar-invert-bits** ( u n bar -- )
> Invert a range of bits in the array, starting from the nth bit, u bits long
**bar-invert** ( bar -- )
> Invert all bits in the array
#### Bit check words ####
**bar-get-bit** ( n bar -- flag )
> Check if the nth bit is set
**bar-count-bits** ( +n1 n2 bar -- u )
> Count the number of bits set in a range in the array, starting from the n2th bit, n1 bits long
**bar-count** ( bar -- u )
> Count the number of bits set in the array
**bar-execute** ( i\*x xt bar -- j\*x )
> Execute xt for every bit in the array
**bar-execute?** ( i\*x xt bar -- j\*x flag )
> Execute xt for every bit in the array or until xt returns true, flag is true if xt returned true
#### Inspection ####
**bar-dump** ( bar -- )
> Dump the bit array
### Examples ###
```
include ffl/bar.fs


\ Create a bit array with 15 bits [0..14] in the dictionary

15 bar-create bar1


\ Set bit 1, 4..8, 12 and 14 in the array

1       bar1 bar-set-bit
5 4     bar1 bar-set-bits

12 14 2 bar1 bar-set-list   \ new in version 3

\ Count the number of bits set 

.( There are ) bar1 bar-count . .( bits set in the array.) cr

\ Check for bits

6 bar1 bar-get-bit [IF]
  .( Bit 6 is set in the array.) cr
[ELSE]
  .( Bit 6 is not set in the array.) cr
[THEN]

13 bar1 bar-get-bit [IF]
  .( Bit 13 is set in the array.) cr
[ELSE]
  .( Bit 13 is not set in the array.) cr
[THEN]


\ Create a bit array with 8 bits on the heap

8 bar-new value bar2

\ Set all bits in the array

bar2 bar-set

\ Reset bits 5..7 in the array

3 5 bar2 bar-reset-bits

\ Print the bit array by executing bar-emit for every bit in the array

: bar-emit ( flag -- )
  1 AND [char] 0 + emit
;

.( Bit array: ) ' bar-emit bar2 bar-execute cr

\ Hamming distance

.( Hamming distance: ) 
bar2 bar1 bar^xor          \ new in version 3
bar1 bar-count . cr

\ Free the array from the heap

bar2 bar-free

```

---

Generated by **ofcfrth-0.10.0**