### Module Description ###
The snl module implements a single linked list that can store variable size
nodes. It is the base module for more specialized modules, for example the
single linked cell list [scl](scl.md) module.

### Module Words ###
#### List structure ####
**snl%** ( -- n )
> Get the required space for a snl variable
#### List creation, initialisation and destruction ####
**snl-init** ( snl -- )
> Initialise the snl list
**snl-(free)** ( xt scl -- )
> Free the nodes from the heap using xt
**snl-create** ( "`<`spaces`>`name" -- ; -- snl )
> Create a named snl list in the dictionary
**snl-new** ( -- snl )
> Create a new snl list on the heap
**snl-free** ( snl -- )
> Free the list from the heap
#### Member words ####
**snl-length@** ( snl -- u )
> Get the number of nodes in the list
**snl-empty?** ( snl -- flag )
> Check for an empty list
**snl-first@** ( snl -- snn | nil )
> Get the first node from the list
**snl-last@** ( snl -- snn | nil )
> Get the last node from the list
#### List words ####
**snl-append** ( snn snl -- )
> Append the node snn to the list
**snl-prepend** ( snn snl -- )
> Prepend the node snn in the list
**snl-insert-after** ( snn1 snn2 snl -- )
> Insert the node snn1 after the reference node snn2 in the list
**snl-remove-first** ( snl -- snn | nil )
> Remove the first node from the list, return the removed node
**snl-remove-after** ( snn1 snl -- snn2 | nil )
> Remove the node after the reference node snn1 from the list, return the removed node
#### Index words ####
**snl-index?** ( n snl -- flag )
> Check if the index n is valid in the list
**snl-get** ( n snl -- snn )
> Get the nth node from the list
**snl-insert** ( snn n snl -- )
> Insert a node before the nth node in the list
**snl-delete** ( n snl -- snn )
> Delete the nth node from the list, return the deleted node
#### LIFO words ####
**snl-push** ( snn snl -- )
> Push the node snn at the top of the stack [= start of the list]
**snl-pop** ( snl -- snn | nil )
> Pop the node at the top of the stack [= start of the list], return the popped node
**snl-tos** ( snl -- snn | nil )
> Get the node at the top of the stack [= start of the list], return this node
#### FIFO words ####
**snl-enqueue** ( snn snl -- )
> Enqueue the node snn at the start of the queue [=end of the list]
**snl-dequeue** ( snl -- snn | nil )
> Dequeue the node at the end of the queue [= start of the list], return this node
#### Special words ####
**snl-execute** ( i\*x xt snl -- j\*x )
> Execute xt for every node in list
**snl-execute?** ( i\*x xt snl -- j\*x flag )
> Execute xt for every node in the list or until xt returns true, flag is true if xt returned true
**snl-reverse** ( snl -- )
> Reverse or mirror the list
#### Sort word ####
**snl-sort** ( xt snl -- )
> Sort the list snl using mergesort, xt compares the nodes
#### Inspection ####
**snl-dump** ( snl -- )
> Dump the list
### Examples ###
```
include ffl/snl.fs
include ffl/rng.fs


\ Example: sort a single linked list with 1001 random floats


\ Create the single linked list on the heap

snl-new value flist

\ Extend the generic single linked list node with a float field

begin-structure fnode%
  snn%
  +field   fnode>node
  ffield:  fnode>float
end-structure

\ Create the pseudo random generator in the dictionary with seed 5489

5489 rng-create frng

\ Insert 1001 float nodes in the flist

: flist-insert     ( n -- = Insert n random floats in flist )
  0 DO
    frng rng-next-float           \ Generate random float
    fnode% allocate throw         \ Allocate fnode
    dup fnode>node  snn-init      \ Initialise generic node
    dup fnode>float f!            \ Store random float
        flist snl-append          \ Append to flist
  LOOP
;

1001 flist-insert

\ Check the number of floats out of sequence

: fnode-out-sequence ( n1 r1 fnode -- n2 r2 = Count the number of out of sequence floats, n: count r:previous float )
  fnode>float f@
  fswap fover
  f> IF 1+ THEN                   \ Compare current float with previous float, increment counter if out of sequence
;

.( Before sorting there are ) 0 -1.0E+0 ' fnode-out-sequence flist snl-execute fdrop . .( floats out of sequence. ) cr

\ Sort the list using the fnode-compare word

: fnode-compare    ( fnode1 fnode2 -- n = Compare fnode1 with fnode2 )
  swap fnode>float f@ fnode>float f@ f-
  fdup f0< IF
    fdrop -1
  ELSE f0= IF
    0
  ELSE
    1
  THEN THEN
;

' fnode-compare flist snl-sort

\ Check the number of floats out of sequence again

.( After sorting there are ) 0 -1.0E+0 ' fnode-out-sequence flist snl-execute fdrop . .( floats out of sequence. ) cr

\ Cleanup the list

flist snl-free                    \ No dynamic memory stored in node, so default free word can be used

```

---

Generated by **ofcfrth-0.10.0**