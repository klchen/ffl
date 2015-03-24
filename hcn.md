### Module Description ###
The hcn module implements a node that stores cell wide data in a hash table.

### Module Words ###
#### Hash table node structure ####
**hcn%** ( - n )
> Get the required space for a hcn node
#### Node creation, initialisation and destruction ####
**hcn-init** ( x c-addr u u2 hcn -- )
> Initialise the node with the hash u2, the key c-addr u and cell data x
**hcn-(free)** ( hcn -- )
> Free the key from the heap
**hcn-new** ( x c-addr u u2 -- hcn )
> Create a new node on the heap with the hash u2, the key c-addr u and cell data x
**hcn-free** ( hcn -- )
> Free the node from the heap
#### Inspection ####
**hcn-dump** ( hcn -- )
> Dump the node
### Examples ###
```
include ffl/hct.fs
include ffl/hci.fs

\ Example: store mountain heights in a hash table


\ Create the hash table in the dictionary with an initial size of 10

10 hct-create height-table


\ Add the mountains (in meters)

8300 s" mount everest" height-table hct-insert
4819 s" mont blanc"    height-table hct-insert
5642 s" mount elbrus"  height-table hct-insert

\ Get a mountain height

s" mount everest" height-table hct-get [IF]
  .( Mount everest: ) . cr
[ELSE]
  .( Unknown mountain) cr
[THEN]

s" vaalserberg" height-table hct-get [IF]
  .( Vaalserberg: ) . cr
[ELSE]
  .( Unknown mountain) cr
[THEN]


\ Word for printing the mountain height

: height-emit ( n c-addr u -- = height key )
  type ."  -> " . cr
;


\ Print all mountain heights

' height-emit height-table hct-execute            \ Execute the word height-emit for all entries in the hash table



\ Example hash table iterator

\ Create the hash table iterator in the dictionary

height-table hci-create height-iter               \ Create an iterator named height-iter on the height-table hash table

\ Moving the iterator

height-iter hci-first                         \ Move the iterator to the first record
[IF]                                          \ If record exists Then ..
  height-iter hci-key type                    \   Type the key ..
  .(  => )
  .                                           \   .. and the value
  cr
[THEN]

8300 height-iter hci-move                     \ Move the iterator to the mountain with a height of 8300
[IF]                                          \ If mountain exists Then ..
  height-iter hci-key type                    \   Type the name ..
  .(  => )
  height-iter hci-get drop .                  \   .. and the height
  cr
[ELSE]
  .( No mountain found with a height of 8300) cr  
[THEN]
```

---

Generated by **ofcfrth-0.10.0**