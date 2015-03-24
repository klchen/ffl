### Module Description ###
The str module implements a dynamic text string.

### Module Words ###
#### String structure ####
**str%** ( -- n )
> Get the required space for a str variable
#### String creation, initialisation and destruction ####
**str-init** ( str -- )
> Initialise to an empty string
**str-(free)** ( str -- )
> Free the string data from the heap
**str-create** ( "`<`spaces`>`name" -- ; -- str )
> Create a named empty string in the dictionary
**str-new** ( -- str )
> Create a new empty string on the heap
**str-free** ( str -- )
> Free the string from the heap
#### Member words ####
**str-empty?** ( str -- flag )
> Check for an empty string
**str-length@** ( str -- u )
> Get the length of the string
**str-length!** ( u str -- )
> Set the length of the string
**str-index?** ( n str -- flag )
> Check if the index n is valid in the string
**str-data@** ( str -- c-addr )
> Get the start of the string
**str-size!** ( u str -- )
> Insure the size u of the string
**str-extra@** ( str -- u )
> Get the extra space allocated during resizing of the string
**str-extra!** ( u str -- )
> Set the extra space allocated during resizing of the string
**str+extra@** ( -- u )
> Get the initial extra space allocated during resizing of the string
**str+extra!** ( u -- )
> Set the initial extra space allocated during resizing of the string
#### Set words ####
**str-clear** ( str -- )
> Clear the string
**str-set** ( c-addr u str -- )
> Set the string c-addr u in the string
**str-append-string** ( c-addr u str -- )
> Append the string c-addr u to the string
**str-prepend-string** ( c-addr u str -- )
> Prepend the string c-addr u to the string
**str-append-chars** ( char u str -- )
> Append u chars in the string
**str-prepend-chars** ( char u str -- )
> Prepend u chars in the string
**str-insert-string** ( c-addr u n str -- )
> Insert the string c-addr in the string at index n
**str-insert-chars** ( char u n str -- )
> Insert u chars in the string at index n
#### Get words ####
**str-get-substring** ( u n str -- c-addr u )
> Get a substring starting from index n,  u characters long
**str-get** ( str -- c-addr u )
> Get the string in the string
**str-bounds** ( str -- c-addr+u c-addr )
> Get the bounds of the string
#### Delete word ####
**str-delete** ( u n str -- )
> Delete a substring starting at index n with length u from the string
#### Zero terminated string words ####
**str-set-zstring** ( c-addr str -- )
> Set a zero terminated string in the string
**str-get-zstring** ( str -- c-addr )
> Get the string as zero terminated string
#### Strings word ####
#### Character words ####
**str-append-char** ( char str -- )
> Append a character at the end of the string
**str-prepend-char** ( char str -- )
> Prepend a character at the start of the string
**str-push-char** ( char str -- )
> Push a character at the end of the string
**str-pop-char** ( str -- char )
> Pop a character from the end of the string
**str-enqueue-char** ( char str -- )
> Place a character at the start of the string
**str-dequeue-char** ( char str -- )
> Get the character at the end of the string
**str-set-char** ( char n str -- )
> Set the character on the nth position in the string
**str-get-char** ( n str -- char )
> Get the character from the nth position in the string
**str-insert-char** ( char n str -- )
> Insert the character on the nth position in the string
**str-delete-char** ( n str -- )
> Delete the character on the nth position in the string
#### Special words ####
**str-count** ( c-addr u str -- u )
> Count the number of occurrences of the string c-addr u in the string
**str-execute** ( i\*x xt str -- j\*x )
> Execute the xt token for every character in the string
**str-execute?** ( i\*x xt str -- j\*x flag )
> Execute the xt token for every character in the string or until xt returns true, flag is true if xt returned true
#### Special manipulation words ####
**str-capatilize** ( str -- )
> Capitalize the first word in the string
**str-cap-words** ( str -- )
> Capitalize all words in the string
**str-center** ( u str -- )
> Center the string in width u
**str-align-left** ( u str -- )
> Align left the string in width u
**str-align-right** ( u str -- )
> Align right the string in width u
**str-strip-leading** ( str -- )
> Strip leading spaces in the string
**str+strip-leading** ( c-addr1 u1 -- c-addr2 u2 )
> Strip leading whitespace in the string c-addr1 u1
**str-strip-trailing** ( str -- )
> Strip trailing spaces in the string
**str+strip-trailing** ( c-addr u1 -- c-addr u2 )
> Strip trailing whitespace in the string c-addr u1
**str-strip** ( str -- )
> Strip leading and trailing spaces in the string
**str+strip** ( c-addr1 u1 -- c-addr2 u2 )
> Strip leading and trailing spaces from string c-addr1 u1
**str-lower** ( str -- )
> Convert the string to lower case
**str-upper** ( str -- )
> Convert the string to upper case
**str-expand-tabs** ( u str -- )
> Expand the tabs to u spaces in the string
#### Comparison words ####
**str-icompare** ( c-addr u str -- n )
> Compare case-insensitive the string c-addr u with the string, return the result [-1,0,1]
**str-ccompare** ( c-addr u str -- n )
> Compare case-sensitive the string c-addr u with the string, return the result [-1,0,1]
**str^icompare** ( str1 str2 -- n )
> Compare case-insensitive the strings str1 and str, return the result [-1,0,1]
**str^ccompare** ( str1 str2 -- n )
> Compare case-sensitive the string str1 and str2, return the result [-1,0,1]
#### Search and replace words ####
**str-find** ( c-addr u n1 str -- n2 )
> Find the first occurrence of the string c-addr u starting from index n1 in the string, return -1 if not found
**str-replace** ( c-addr1 u1 c-addr2 u2 str -- )
> Replace all occurences of the string c-addr2 u2 with the string c-addr1 u1 in the string
#### Split words ####
**str+columns** ( c-addr u n -- c-addrn un ... c-addr1 u1 n )
> Split the string c-addr u in n substrings, u width wide, skipping leading spaces [recursive](recursive.md)
#### Inspection ####
**str-dump** ( str -- )
> Dump the string


---

Generated by **ofcfrth-0.10.0**