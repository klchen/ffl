\ ==============================================================================
\
\                spf - the sprintf formatter in the ffl
\
\               Copyright (C) 2008  Dick van Oudheusden
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
\  $Date: 2008-09-22 18:46:53 $ $Revision: 1.4 $
\
\ ==============================================================================


include ffl/config.fs


[UNDEFINED] spf.version [IF]

include ffl/str.fs


( spf = Sprintf string formatter )
( The spf module implements a simplified version of C's sprintf function.    )
( The words in this module expect a format string with specifiers [see below )
( for the format]. For every specifier [except %% and %n] a stack item is    )
( converted  to the character representation and added to the destination.   )
( All other characters is simply copied to the destination.                  )
( <pre>                                                                      )
( Format: %[flags][width][length]specifier                                   )
(     Flags: 0      = Left-pads the number with zeros instead of spaces      )
(            -      = Left justify the number                                )
(            +      = A positive number is preceded with a '+'               )
(            space  = A positive number is preceded with a space             )
(     Width: number = the minimum number of characters written               )
(    Double: l      = the argument is interpreted as a double                )
( Specifier: c      = format a character [char]                              )
(            d      = format a signed number [n or d]                        )
(            i      = format a signed number [n or d]                        )
(            o      = format a signed octal [n or d]                         )
(            s      = format a string [c-addr u]                             )
(            u      = format a unsigned number [u or ud]                     )
(            x      = format a unsigned hexadecimal number [u or ud]         )
(            X      = format a unsigned hexadecimal number, capital letters  )
(            p      = format a unsigned hexadecimal number [u or ud]         )
(            n      = store the length of the string in [addr]               )
(            %      = write a '%' []                                         )
( </pre>                                                                     )


1 constant spf.version


( Private flags )

1  constant spf.zero-padding   \ 0
2  constant spf.left-justify   \ -
4  constant spf.space-sign     \ ' '
8  constant spf.plus-sign      \ +
16 constant spf.minus-sign     \ -
32 constant spf.double         \ l

spf.space-sign spf.plus-sign OR spf.minus-sign OR
   constant spf.signs          \ All sign bits


( Private format words )

: spf-leading-spaces  ( n1 n2 str -- n1 n2 = Add n2 leading spaces, if indicated by n1 )
  >r
  over spf.left-justify spf.zero-padding OR AND 0= IF
    bl over r@ str-append-chars
  THEN
  rdrop
;


: spf-sign         ( n1 n2 str -- n1 n2 = Add the sign to string, if indicated by n1 )
  >r
  over dup spf.minus-sign AND IF
    drop [char] - r@ str-append-char
  ELSE dup spf.plus-sign AND IF
    drop [char] + r@ str-append-char
  ELSE spf.space-sign AND IF
    bl r@ str-append-char
  THEN THEN THEN
  rdrop
;


: spf-leading-zeros  ( n1 n2 str -- n1 n2 = Add n2 leading zeros, if indicated by n1 )
  >r
  over spf.left-justify spf.zero-padding OR AND spf.zero-padding = IF
    [char] 0 over r@ str-append-chars
  THEN
  rdrop
;


: spf-trailing-spaces  ( n1 n2 str -- = Add n2 trailing spaces, if indicated by n1 )
  >r
  swap spf.left-justify AND IF
    bl swap r@ str-append-chars
  ELSE
    drop
  THEN
  rdrop
;


: spf-signed       ( i*x n1 n2 n3 str -- j*x = Convert i*x using flags n1, width n2, base n3 to a string )
  >r
  base @ >r                       \ Set the base
  base !

  >r >r
  r@ spf.double AND 0= IF
    s>d                           \ If single than convert to double
  THEN

  dup 0< IF 
    r> spf.minus-sign OR >r       \ Save minus sign in flags
  THEN 

  dabs <# #s #>                   \ Convert double to string
  r>                              \ Flags

  over r> swap -
  over spf.signs AND IF
    1-
  THEN
  0 max                           \ Pad = width - length - 1 (if sign), min. 0

  r> base !                       \ Restore base

  r@ spf-leading-spaces           \ Add leading spaces, if requested
  r@ spf-sign                     \ Add sign, if requested
  r@ spf-leading-zeros            \ Add leading zeros, if requested
  
  2swap r@ str-append-string      \ Add converted string

  r> spf-trailing-spaces          \ Add trailing spaces, if requested
;


: spf+convert-unsigned  ( i*x n1 n2 n3 -- j*x c-addr u n1 n4 = Convert unsigned i*x using flags n1, width n2 and base n3 )
  base @ >r
  base !

  >r >r
  r@ spf.double AND 0= IF
    0
  THEN

  <# #s #>

  r> over r> swap - 0 max         \ Pad = width - length, min. 0

  r> base !
;


: spf-lower-unsigned  ( i*x n1 n2 n3 str -- j*x = Convert unsigned i*x using flags n1, width n2 and base n3 to string )
  >r
  spf+convert-unsigned            \ Convert number to string using the base

  r@ spf-leading-spaces           \ Add leading spaces, if requested
  r@ spf-leading-zeros            \ Add leading zeros, if requested

  2swap r@ -rot
  bounds ?DO
    I c@ chr-lower over str-append-char \ Put string lowercase in str
  LOOP
  drop

  r> spf-trailing-spaces          \ Add trailing spaces, if requested
;


: spf-upper-unsigned  ( i*x n1 n2 n3 str -- j*x = Convert unsigned i*x using flags n1, width n2 and base n3 to string )
  >r
  spf+convert-unsigned            \ Convert number to string using the base

  r@ spf-leading-spaces           \ Add leading spaces, if requested
  r@ spf-leading-zeros            \ Add leading zeros, if requested

  2swap r@ -rot
  bounds ?DO
    I c@ chr-upper over str-append-char \ Put string uppercase in str
  LOOP
  drop

  r> spf-trailing-spaces          \ Add trailing spaces
;


: spf+convert-char ( char n1 n2 -- char n1 n3 = Convert a char by determining the pad width n3 )
  1- 0 max
;


: spf+convert-string ( c-addr u n1 n2 -- c-addr u n1 n3 = Convert a string by determining the pad width n3 )
  >r over r> swap - 0 max
;


( Private state words )

0 value spf.check-format
0 value spf.check-flags
0 value spf.check-width
0 value spf.check-double
0 value spf.check-specifier


: spf-check-specifier ( i*x n1 n2 char str -- j*x str xt = Check for specifier, next state = check-format )
  >r
  CASE
    [char] d OF 10 r@ spf-signed ENDOF

    [char] i OF 10 r@ spf-signed ENDOF

    [char] u OF 10 r@ spf-lower-unsigned ENDOF

    [char] x OF 16 r@ spf-lower-unsigned ENDOF

    [char] X OF 16 r@ spf-upper-unsigned ENDOF

    [char] c OF spf+convert-char   r@ spf-leading-spaces rot   r@ str-append-char   r@ spf-trailing-spaces ENDOF

    [char] s OF spf+convert-string r@ spf-leading-spaces 2swap r@ str-append-string r@ spf-trailing-spaces ENDOF

    [char] n OF 2drop  r@ str-length@ swap ! ENDOF

    [char] o OF 8 r@ spf-signed ENDOF

    [char] p OF 16 r@ spf-lower-unsigned ENDOF

    [char] % OF 2drop [char] % r@ str-append-char ENDOF

    [char] ? r@ str-append-char >r 2drop r>
  ENDCASE
  r>
  spf.check-format
;
' spf-check-specifier to spf.check-specifier


: spf-check-double ( n1 n2 char str -- n3 n2 str xt1 | str xt2 = Check for double, next states xt1 = check-specifier, xt2 = check-format )
  over [char] l = IF
    nip 2>r spf.double OR 2r>
    spf.check-specifier
  ELSE
    spf-check-specifier
  THEN
;
' spf-check-double to spf.check-double


: spf-check-width  ( n1 n2 char str -- n1 n3 str xt1 | .. | str xt3 = Check for width, next states xt1 = check-width, xt2 = ..., xt3 = check-format )
  over chr-digit? IF
    >r swap 10 * swap [char] 0 - + r>
    spf.check-width
  ELSE
    spf-check-double
  THEN
;
' spf-check-width to spf.check-width


: spf-check-flags  ( n1 char str -- n2 str xt1 | n1 n2 str xt2 | str xt3 = Check for flags, next states xt1 = check-flags, xt2 = .., xt3 = check-format )
  over [char] 0 = IF
    nip >r spf.zero-padding OR r>
    spf.check-flags
  ELSE over [char] - = IF
    nip >r spf.left-justify OR r>
    spf.check-flags
  ELSE over bl = IF
    nip >r spf.space-sign OR r>
    spf.check-flags
  ELSE over [char] + = IF
    nip >r spf.plus-sign OR r>
    spf.check-flags
  ELSE
    0 -rot spf-check-width
  THEN THEN THEN THEN
;
' spf-check-flags to spf.check-flags


: spf-check-format  ( char str -- str xt1 | 0 str xt2 = Check for format character, next states xt1 = check-format, xt2 = check-flags )
  over [char] % = IF
    nip 0 swap spf.check-flags
  ELSE
    tuck str-append-char
    spf.check-format
  THEN
;
' spf-check-format to spf.check-format


( Sprintf words )


: spf-append       ( i*x c-addr u str -- = Convert the arguments i*x with the format string c-addr u and append the result to str )
  spf.check-format 2swap
  bounds ?DO
    I c@ -rot execute
  LOOP
  2drop
;


: spf-set          ( i*x c-addr u str -- = Convert the arguments i*x with the format string c-addr u and set the result in str)
  dup str-clear spf-append
;


: spf"             ( "ccc<quote>" i*x str -- = Convert the arguments i*x with the format string and set the result in str )
  [char] " parse
  state @ IF
    postpone    sliteral
    ['] rot     compile,
    ['] spf-set compile,
  ELSE
    rot spf-set
  THEN
; immediate

[THEN]

\ ==============================================================================