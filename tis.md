### Module Description ###
The tis module implements a text input stream. It extends the str module,
so all words from the str module, can be used on the tis data structure.
There are seven basic methods: fetch = fetch the data, the stream pointer
is not updated; next = after a fetch, the stream pointer is updated; seek
= move the stream pointer; match = try to match data, if there is a match,
the stream pointer is updated, read = read data, if data is returned then
the stream pointer is updated; scan = scan for data, if the data is found
then the leading text is returned and the stream pointer is moved after
the scanned data; skip = move the stream pointer after the skipped data.
```
  Stack usage reader word: x -- c-addr u | 0 = Return read data c-addr u or 0 for no more
```

### Module Words ###
#### Input stream structure ####
**tis%** ( -- n )
> Get the required space for a tis variable
#### Input stream creation, initialisation and destruction ####
**tis-init** ( tis -- )
> Initialise the empty input stream
**tis-create** ( "`<`spaces`>`name" -- ; -- tis )
> Create a named input stream in the dictionary
**tis-new** ( -- tis )
> Create a new input stream on the heap
**tis-(free)** ( tis -- )
> Free the internal, private variables from the heap
**tis-free** ( tis -- )
> Free the input stream from the heap
#### Seek and tell words: position in the stream ####
**tis-pntr@** ( tis -- u )
> Get the stream pointer
**tis-pntr!** ( n tis -- flag )
> Set the stream pointer from start `{>`=0} or from end `{<`0}
**tis-pntr+!** ( n tis -- flag )
> Add the offset u to the stream pointer
#### Reader words ####
**tis-set-reader** ( x xt tis -- )
> Initialise the stream for reading using the reader callback xt and its data x
**tis-read-more** ( tis -- flag )
> Read more data from the reader
#### String words ####
**tis-reset** ( tis -- )
> Reset the input stream for reading from string
**tis-set** ( c-addr u tis -- )
> Initialise the stream for reading from a string
**tis-get** ( tis -- 0 | addr u )
> Get the remaining characters from the stream, stream pointer is not changed
#### Stream words ####
**tis-eof?** ( tis -- flag )
> Check if the end of the stream is reached
**tis-reduce** ( tis -- )
> Reduce the stream size
#### Fetch and next words ####
**tis-fetch-char** ( tis -- false | char true )
> Fetch the next character from the stream
**tis-next-char** ( tis -- )
> Move the stream pointer one character after fetch-char
**tis-fetch-chars** ( n tis -- 0 | addr u )
> Fetch maximum of n next characters from the stream
**tis-next-chars** ( n tis -- )
> Move the stream pointer n characters after fetch-chars
#### Match words: check for starting data ####
**tis-imatch-char** ( char tis -- flag )
> Match case-insensitive a character
**tis-cmatch-char** ( char tis -- flag )
> Match case-sensitive a character
**tis-cmatch-chars** ( c-addr u tis -- false | char true )
> Match one of the characters in the string case-sensitive
**tis-cmatch-string** ( c-addr u tis -- flag )
> Match case-sensitive a string
**tis-imatch-string** ( c-addr u tis -- flag )
> Match case-insensitive a string
**tis-match-set** ( chs tis - false | char true )
> Match one of the characters in the set
#### Read data words ####
**tis-read-char** ( tis -- false | char true )
> Read character from the stream
**tis-read-all** ( tis -- 0 | c-addr u )
> Read all remaining characters from the stream
**tis-read-string** ( n tis -- 0 | c-addr u )
> Read n characters from the stream
**tis-read-line** ( tis -- 0 | c-addr u )
> Read characters till cr and/or lf
**tis-read-number** ( tis -- false | n true )
> Read a cell number in the current base from the stream
**tis-read-double** ( tis -- false | d true )
> Read a double value in the current base from the stream
**tis-read-float** ( jis -- false | r true )
> Read a float from the stream
#### Scan words: look for data in the stream ####
**tis-scan-char** ( char tis -- false | c-addr u true )
> Read characters till the char
**tis-scan-chars** ( c-addr1 n1 tis -- false | c-addr2 u2 char true )
> Read characters till one of characters in c-addr1 u1
**tis-scan-string** ( c-addr1 n2 tis -- false | c-addr1 u2 true )
> Read characters till the string c-addr1 n1
**tis-iscan-string** ( c-addr1 n1 tis -- false | c-addr2 u2 true )
> Read characters till the string c-addr1 n1 [insensitive](case.md)
**tis-scan-set** ( chs tis - false | c-addr u char true )
> Read characters till one of the characters in the set chs
#### Skip words: skip data in the stream ####
**tis-skip-spaces** ( tis -- n )
> Skip whitespace in the stream, return the number of skipped whitespace characters
#### Inspection ####
**tis-dump** ( tis -- )
> Dump the text input stream
### Examples ###
```
include ffl/tis.fs


\ Example 1: Use the text input stream with a string of text


\ Create an text input stream in the dictionary

tis-create tis1


\ Fill the stream with a string

s" This is a special test string" tis1 tis-set


\ Match the start of the string

char t tis1 tis-imatch-char [IF]            \ Match the start of the string with a t (case insensitive)
  .( The string starts with a t or T.) cr
[THEN]
  
s" his i" tis1 tis-cmatch-string [IF]
  .( After that the string starts with his.)  cr \ After matching the t, the string 'his i' is matched
[THEN]


\ Read in the string

tis1 tis-read-char [IF]
  .( Next character: ) emit cr                \ The next character is read (e.g. s)
[THEN]

.( Next five characters: )
5 tis1 tis-read-string type cr                \ The next five characters are read (e.g. ' a sp')


\ Scan for string

s" test" tis1 tis-scan-string [IF]
  .( String till 'test': ) type cr            \ Return the skipped text (e.g. 'ecial ')
[THEN]


\ Skip spaces

.( Skipped spaces: )
tis1 tis-skip-spaces . cr                     \ Skip spaces and return the number of skipped spaces (e.g. 1)
  


\ Example 2: Use the text input stream with a reader

\ Create a text input stream on the heap

tis-new value tis2


\ Setup the reader callback word

: tis-reader ( fileid -- c-addr u | 0 )
  pad 64 rot read-file throw
  dup IF
    pad swap
  THEN
;

s" index.html" r/o open-file throw dup  ' tis-reader   tis2 tis-set-reader   \ Setup a reader with a file


\ Scan with the reader 
: show-links   ( -- = example: Type all links in html file )
  ." Links:" cr
  BEGIN
    true
    s" <a HREF=" tis2 tis-iscan-string IF        \ Look for '<a HREF=', case insensitive, save lookup result
      2drop                                      \ No interest in leading string
      0=
      [char] > tis2 tis-scan-char IF             \ Look for '>'
        type cr                                  \ Type leading string = link
      THEN
    THEN
  UNTIL
;

show-links

\ Done, close the file

close-file throw

\ Free the stream from the heap

tis2 tis-free

```

---

Generated by **ofcfrth-0.10.0**