### Module Description ###
The scl module implements a single linked list that can store cell wide data.

### Module Words ###
#### List structure ####
**scl%** ( -- n )
> Get the required space for a scl variable
#### List creation, initialisation and destruction ####
**scl-init** ( scl -- )
> Initialise the scl list
**scl-(free)** ( scl -- )
> Free the nodes from the heap
**scl-create** ( "`<`spaces`>`name" -- ; -- scl )
> Create a named scl list in the dictionary
**scl-new** ( -- scl )
> Create a new scl list on the heap
**scl-free** ( scl -- )
> Free the list from the heap
#### Member words ####
**scl-empty?** ( scl -- flag )
> Check for an empty list
**scl-length@** ( scl -- u )
> Get the number of nodes in the list
**scl-compare!** ( xt scl -- )
> Set the compare execution token for sorting the list
**scl-compare@** ( scl -- xt )
> Get the compare execution token for sorting the list
#### List words ####
**scl-clear** ( scl -- )
> Delete all nodes from the list
**scl-append** ( x scl -- )
> Append the cell data x in the list
**scl-prepend** ( x scl -- )
> Prepend the cell data x in the list
#### Index words ####
**scl-index?** ( n scl -- flag )
> Check if the index n is valid for the list
**scl-set** ( x n scl -- )
> Set the cell data x in the nth node in the list
**scl-get** ( n scl -- x )
> Get the cell data x from the nth node in the list
**scl-insert** ( x n scl -- )
> Insert cell data x at the nth node in the list
**scl-delete** ( n scl -- x )
> Delete the nth node from the list, return the cell data from the deleted node
#### Special words ####
**scl-count** ( x scl -- u )
> Count the number of occurrences of the cell data x in the list
**scl-execute** ( i\*x xt scl -- j\*x )
> Execute xt for every cell data in list
**scl-execute?** ( i\*x xt scl -- j\*x flag )
> Execute xt for every cell data in the list or until xt returns true, flag is true if xt returned true
**scl-reverse** ( scl -- )
> Reverse or mirror the list
#### Search words ####
**scl-find** ( x scl -- n )
> Find the first index for cell data x in the list, -1 for not found
**scl-has?** ( x scl -- flag )
> Check if the cell data x is present in the list
**scl-remove** ( x scl -- flag )
> Remove the first occurrence of the cell data x from the list, return success
#### Sort words ####
**scl-insert-sorted** ( x scl -- )
> Insert the cell data x sorted in the list
**scl-sort** ( xt scl -- )
> Sort the list scl using mergesort, xt compares the nodes
#### Inspection ####
**scl-dump** ( scl -- )
> Dump the list
### Examples ###
```
include ffl/scl.fs
include ffl/rng.fs


\ Example: sort a cell based single linked list with 1001 random numbers


\ Create the single linked list on the heap

scl-new value nlist

\ Create the pseudo random generator in the dictionary with seed 5498

5498 rng-create nrng

\ Insert 1001 numbers in the nlist

: nlist-insert     ( n -- = Insert n random numbers in nlist )
  0 DO
    nrng rng-next-number          \ Generate random number and ..
    nlist scl-append              \ .. append to the list
  LOOP
;

1001 nlist-insert

\ Check the number of numbers out of sequence

: nnode-out-sequence ( n1 n2 flag n3 -- n4 n5 true = Count the number of out of sequence number, n1: count n2:previous number n3: number )
  swap IF
    tuck > IF >r 1+ r> THEN       \ Compare current number with previous number increment counter if out of sequence
  ELSE
    nip                           \ First call, no check
  THEN
  true
;

.( Before sorting there are ) 0 0 false ' nnode-out-sequence nlist scl-execute 2drop . .( numbers out of sequence. ) cr

\ Sort the list using the <=> word

' <=> nlist scl-sort

\ Check the number of numbers out of sequence again

.( After sorting there are ) 0 0 false ' nnode-out-sequence nlist scl-execute 2drop . .( numbers out of sequence. ) cr

\ Cleanup the list

nlist scl-free

```

---

Generated by **ofcfrth-0.10.0**