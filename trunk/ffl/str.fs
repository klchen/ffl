\ ==============================================================================
\
\             str - the character string module in the ffl
\
\               Copyright (C) 2005  Dick van Oudheusden
\  
\ This library is free software; you can redistribute it and/or
\ modify it under the terms of the GNU General Public
\ License as published by the Free Software Foundation; either
\ version 2 of the License, or (at your option) any later version.
\
\ This library is distributed in the hope that it will be useful,
\ but WITHOUT ANY WARRANTY; without even the implied warranty of
\ MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
\ General Public License for more details.
\
\ You should have received a copy of the GNU General Public
\ License along with this library; if not, write to the Free
\ Software Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.
\
\ ==============================================================================
\ 
\  $Date: 2005-12-25 19:53:12 $ $Revision: 1.3 $
\
\ ==============================================================================

include ffl/config.fs


[UNDEFINED] str.version [IF]


include ffl/stc.fs
include ffl/chr.fs


( str = Character string )
( The str module implements a character string.)


1 constant str.version


( Public structure )

struct: str%       ( - n = Get the required space for the str data structure )
  cell: str>data
  cell: str>length
  cell: str>size    
  cell: str>extra
;struct


( Private database )

8 value str.extra   ( - w = the initial extra space )


( Private words )

: str-offset       ( n w:scl - n = Determine offset from index, incl. validation )
  str>length @
  
  over 0< IF                 \ if index < 0 then
    tuck + swap              \   index = index + length
  THEN
  
  over <= over 0< OR IF      \ if index < 0 or index >= length
    exp-index-out-of-range throw 
  THEN
;


( Public words )

: str-init         ( w:str - = Initialise the empty string )
  dup str>data  nil!
  dup str>length  0!
  dup str>size    0!
  str.extra swap str>extra !
;


: str-create       ( C: "name" - R: - w:str = Create a named string in the dictionary )
  create   here   str% allot   str-init
;


: str-new          ( - w:str = Create a new string on the heap )
  str% allocate  throw  dup str-init
;


: str-free         ( w:str - = Free the string from the heap )
  free  throw
;


: str-empty?       ( w:str - f = Check for empty string )
  str>length @ 0=  
;


: str-length@      ( w:str - u = Get the length of the string )
  str>length @
;


: str-size!        ( u w:str - = Insure the size of the string )
  dup str>data @ nil= IF     \ if data = nil then
    tuck str>extra @ +       \   size = requested + extra
    2dup swap str>size !
    1+ chars allocate throw  \ 
    swap str>data !          \   data = allocated size + 1 for zero terminated string
  ELSE
    2dup str>size @ > IF     \ else if requested > current size then
      tuck str>extra @ +     \   size = requested + extra
      2dup swap str>size !
      1+ chars
      over str>data @ swap   \   reserve extra character for zero terminated string
      resize throw        
      swap str>data !    
    ELSE
      2drop
    THEN
  THEN
;


: str-extra@       ( w:str - u = Get the extra space allocated during resizing of the string )
  str>extra @
;

: str-extra!       ( u w:str - = Set the extra space allocated during resizing of the string )
  str>extra !
;


: str+extra@       ( - u = Get the initial extra space allocated during resizing of the string )
  str.extra
;


: str+extra!       ( u - = Set the initial extra space allocated during resizing of the string )
  to str.extra
;


( String manipulation )


: str-clear        ( w:str - = Clear the string )
  str>length 0!
;


: str-set          ( c-addr u w:str - = Set a string in the string )
  2dup str-size!             \ check the space
  2dup str>length !          \ set the length
  str>data @ swap chars cmove \ move the string
;


( Private words )

: str-length+      ( u w:str - u = Increase the length, return the previous length )
  tuck str>length @ +        \ len' = str>length + u
  swap
  2dup str-size!             \ check space
  str>length @!              \ fetch and store the new length
;


( Public words )

: str-append       ( c-addr u w:str - = Append a string to the string )
  2dup str-length+           \ increase the length
  
  chars swap str>data @ +    \ move the string at the end
  swap cmove
;


: str-prepend      ( c-addr u w:str - = Prepend a string to the string )
  2dup str-length+           \ increase the length
  
  >r 2dup str>data @
  swap chars over + r> cmove> \ move away the current string
  
  str>data @ swap cmove      \ move the new string at the begin
;


: str-append-chars   ( c u w:str - = Append a number of characters )
  2dup str-length+           \ increase the length
  
  chars swap str>data @ +    \ fill the characters at the end
  -rot swap fill
;


: str-prepend-chars  ( c u w:str - = Prepend a number of characters )
  2dup str-length+           \ increase the length
  
  >r 2dup str>data @
  swap chars over + r> cmove>  \ move away the current string
  
  str>data @ -rot swap fill  \ fill the characters at the begin
;


: str-insert       ( c-addr u w:start w:str - = Insert a string in the string )
;


: str-substring    ( w:start w:end w:str - w:str = Get a substring as a new string )
;


: str-get          ( w:str - c-addr u = Get the string )
  dup  str>data @
  swap str>length @
;


: str-bounds       ( w:str - c-addr+u c-addr = Get the bounds of the string )
  str-get bounds
;  


: str-delete       ( w:start w:end w:str - = Delete a range from the string )
;


: str-set-cstring  ( c-addr w:str - = Set a zero terminated string in the string )
  over 0 swap                \ length = 0
  BEGIN
    dup c@ 0<>               \ while [str] <> 0 do
  WHILE
    swap 1+ swap             \  increase length
    1 chars +                \  increase str
  REPEAT
  drop
  swap str-set               \ set in string
;


: str-get-cstring  ( w:str - c-addr = Get the string as zero terminated string )
  dup str>length @ chars
  swap str>data @
  tuck + 0 swap c!           \ store null at end of string
;


: str^move         ( w:str2 w:str1 - Move str2 in str1 )
  >r str-get r> str-set
;


( Character words )

: str-push-char    ( c w:str - = Push a character at the end of the string )
  1 over str-length+
  
  chars swap str>data @ + c! \ store char at end of string
;


: str-pop-char     ( w:str - c = Pop a character from the end of the string )
  dup str>length dup @ dup 0> IF
    1- tuck swap !
    chars swap str>data @ + c@
  ELSE
    exp-no-data throw
  THEN
;


: str-enqueue-char ( c w:str - = Place a character at the start of the string )
;


: str-dequeue-char ( c w:str - = Get the character at the end of the string )
  postpone str-pop-char
; immediate


: str-set-char     ( c n w:str - = Set the character on the nth position in the string )
  tuck str-offset 
  chars swap str>data @ + c!
;


: str-get-char     ( n w:str - c = Get the character from the nth position in the string )
  tuck str-offset
  chars swap str>data @ + c@
;


: str-insert-char  ( c n w:str - = Insert the character on the nth position in the string )
;


: str-delete-char  ( c n w:str - = Delete the character on the nth position in the string )
;


: str-execute      ( ... xt w:str - ... = Execute the xt token for every character in the string )
  str-bounds ?DO             \ Do for string
    I c@
    swap dup >r
    execute                  \  Execute token for character with stack cleared
    r>
    1 chars +LOOP
  drop
;


( Special manipulation )

: str-capatilize   ( w:str - = Capatilize the first word in the string )
  str-bounds ?DO             \ Do for the string
    I c@
    chr-alpha? IF            \   If alpha character then
      I c@ chr-upper I c!    \     Convert to upper
      LEAVE                  \     Done
    THEN
    1 chars 
  +LOOP
;

  
: str-cap-words    ( w:str - = Capatilize all words in the string )
  false swap str-bounds ?DO  \ Do for the string
    I c@ 
    chr-alpha? tuck IF       \   If alpha character then
      0= IF                  \     If previous was not then
        I c@ chr-upper I c!  \       Convert to upper
      THEN
    ELSE
      drop
    THEN
    1 chars
  +LOOP
  drop
;


: str-center       ( u w:str - = Center the string in u width )
  dup >r str>length @ - dup 0> IF
    dup 2/ swap over -
    32 swap r@ str-append-chars  \ ToDo: space
    32 swap r@ str-prepend-chars
  ELSE
    drop
  THEN
  rdrop
;


: str-ljust        ( u w:str - = Left justify the string )
  tuck str>length @ - dup 0> IF
    32 -rot swap str-append-chars \ ToDo: space
  ELSE
    2drop
  THEN
;


: str-rjust        ( u w:str - = Right justify the string )
  tuck str>length @ - dup 0> IF
    32 -rot swap str-prepend-chars \ ToDo: space
  ELSE
    2drop
  THEN
;


: str-zfill        ( u w:str - = Right justify the string with leading zero's )
  tuck str>length @ - dup 0> IF
    [char] 0 -rot swap str-prepend-chars
  ELSE
    2drop
  THEN
;


: str-strip        ( w:str - = Strip leading and trailing spaces in the string )
;


: str-lstrip       ( w:str - = Strip leading spaces in the string )
;


: str-rstrip       ( w:str - = Strip trailing spaces in the string )
;


: str-lower        ( w:str - = Convert the string to lower case )
  str-bounds ?DO
    I c@ chr-lower I c!
    1 chars
  +LOOP
;


: str-upper        ( w:str - = Convert the string to upper case )
  str-bounds ?DO
    I c@ chr-upper I c!
    1 chars
  +LOOP
;


: str-expand-tabs  ( u w:str - = Expand the tabs in the string )
;


( Comparison )


: str^icompare     ( w:str w:str - n = Compare case-insensitive two strings )
;


: str^ccompare     ( w:str w:str - n = Compare case-sensitive two strings )
;


: str-icompare     ( c-addr u w:str - n = Compare case-insensitive a string with the string )
;


: str-ccompare     ( c-addr u w:str - n = Compare case-sensitive a string with the string )
;


: str-count        ( c-addr u w:str - u = Count the number of occurences of a string in the string )
;


: str-find         ( c-addr u n w:str - n = Find the first occurence of a string from nth position in the string )
;


: str-replace      ( c-addr u c-addr u w:str - n = Replace the occurences of the first string with the second string in the string )
;


[THEN]

\ ==============================================================================
