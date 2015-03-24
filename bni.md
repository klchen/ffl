### Module Description ###
The bni module implements an iterator on the generic binary tree [bnt](bnt.md).

### Module Words ###
#### Iterator Structure ####
**bni%** ( -- n )
> Get the required space for a bni variable
#### Iterator creation, initialisation and destruction ####
**bni-init** ( bnt bni -- )
> Initialise the iterator with a binary tree
**bni-create** ( bnt "`<`spaces`>`name" -- ; -- bni )
> Create a named iterator in the dictionary with a binary tree
**bni-new** ( bnt -- bni )
> Create an iterator on the heap with a binary tree
**bni-free** ( bni -- )
> Free the iterator from the heap
#### Iterator words ####
**bni-get** ( bni -- bnn | nil )
> Get the current node from the iterator
**bni-key** ( bni -- false | x true )
> Get the key x from the current node
**bni-first** ( bni -- bnn | nil )
> Move the iterator to the first node, return this node
**bni-next** ( bni -- bnn | nil )
> Move the iterator to the next node, return this node
**bni-prev** ( bni -- bnn | nil )
> Move the iterator to the previous node, return this node
**bni-last** ( bni -- bnn | nil )
> Move the iterator to the last node, return this node
**bni-first?** ( bni -- flag )
> Check if the iterator is on the first node
**bni-last?** ( bni -- flag )
> Check if the iterator is on the last node
#### Inspection ####
**bni-dump** ( bni -- )
> Dump the iterator variable
### Examples ###
```
include ffl/bnt.fs
include ffl/bni.fs
include ffl/str.fs


\ Example: store mountain positions in a binary tree


\ Create the generic binary tree in the dictionary

bnt-create mountains


\ Setup the compare word for comparing the mountain names

: mount-compare  ( str str - n = Compare the two mountain names )
  str^ccompare
;

' mount-compare mountains bnt-compare!


\ Extend the generic binary tree node with mountain positions fields

begin-structure mount%
  bnn%
  +field   mount>node        \ Mountain structure extends the bnn structure
  ffield:  mount>lng         \ Longitude position
  ffield:  mount>lat         \ Latitude position
end-structure


\ Create the allocation word for the extended structure

: mount-new    ( F: r1 r2 -- ; x bnn1 -- bnn2 = Create a new mountain position variable with position r1 r2, name c-addr u and parent bnn1 )
  mount% allocate throw             \ Allocate the mountain variable
  >r
  r@ mount>node bnn-init            \ Initialise the binary tree node
  r@ mount>lng f!                   \ Save the longitude position
  r@ mount>lat f!                   \ Save the latitude position
  r>
;

 
  
\ Add the mountain positions to the binary tree; the key is the mountain name in a (unique) dynamic string

27.98E0 86.92E0  ' mount-new  str-new dup s" mount everest" rot str-set  mountains bnt-insert [IF]
  .( Mountain:) bnn-key@ str-get type .(  added to the tree.) cr
[ELSE]
  .( Mountain was not unique in tree) cr drop fdrop fdrop 
[THEN]

45.92E0  6.92E0  ' mount-new  str-new dup s" mont blanc" rot str-set   mountains bnt-insert [IF]
  .( Mountain:) bnn-key@ str-get type .(  added to the tree.) cr
[ELSE]
  .( Mountain was not unique in tree) cr drop fdrop fdrop
[THEN]

43.35E0 42.43E0 ' mount-new   str-new dup s" mount elbrus" rot str-set  mountains bnt-insert [IF]
  .( Mountain:) bnn-key@ str-get type .(  added to the tree.) cr
[ELSE]
  .( Mountain was not unique in tree) cr drop fdrop fdrop
[THEN]


\ Find a mountain in the binary tree

str-new value mount-name

s" mont blanc" mount-name str-set

mount-name mountains bnt-get [IF]
  .( Mount:)      dup bnn-key@ str-get type 
  .(  latitude:)  dup mount>lat f@ f. 
  .(  longitude:)     mount>lng f@ f. cr
[ELSE]
  .( Mount:) mount-name str-get type .(  not in tree.) cr
[THEN]


s" vaalserberg" mount-name str-set

mount-name mountains bnt-get [IF]
  .( Mount:)      dup bnn-key@ str-get type 
  .(  latitude:)  dup mount>lat f@ f. 
  .(  longitude:)     mount>lng f@ f. cr
[ELSE]
  .( Mount:) mount-name str-get type .(  not in tree.) cr
[THEN] 


\ Word for printing the mountain positions

: mount-emit ( mount -- = Print mountain )
  dup bnn-key@ str-get type ."  --> "
  dup mount>lat f@ f. ." - "
      mount>lng f@ f. cr
;


\ Print all mountain positions

' mount-emit mountains bnt-execute       \ Execute the word mount-emit for all entries in the tree


\ Example mountains iterator

\ Create the tree iterator in the dictionary

mountains bni-create mount-iter          \ Create an iterator named mount-iter on the mountains tree

\ Moving the iterator

mount-iter bni-first nil<>? [IF]
  .( First mount:) dup bnn-key@ str-get type 
  .(  latitude:)   dup mount>lat f@ f. 
  .(  longitude:)      mount>lng f@ f. cr
[ELSE]
  .( No first mountain.) cr
[THEN]

mount-iter bni-last nil<>? [IF]
  .( Last mount:) dup bnn-key@ str-get type 
  .(  latitude:)  dup mount>lat f@ f. 
  .(  longitude:)     mount>lng f@ f. cr
[ELSE]
  .( No last mountain.) cr
[THEN]


\ Word for freeing the tree node 

: mount-free     ( mount -- = Free the node in the tree )
  dup bnn-key@ str-free
  
  free throw
;

\ Cleanup the tree

' mount-free mountains bnt-clear
```

---

Generated by **ofcfrth-0.10.0**