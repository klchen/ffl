## Stack ##

Since version 0.6.0 FFL uses the ANS stack notation.

Stack parameters input to and output from a word are described by:
( _stack_: _before_ -- _after_) where _stack_ specifies which stack is used. The
code for the control-flow-stack is C:, for the return-stack it is R: and for the
float stack F:. If no stack identifier is present in the notation, the data stack is used.

_Before_ represents the stack parameter data types before execution of the word
and _after_ represents them after execution. The top of the stack is to the right.
The following symbols are used in _before_ and _after_:

| **Symbol** | **Data type** |
|:-----------|:--------------|
| flag | flag |
| true | true flag |
| false | false flag |
| char | character |
| n | signed number |
| +n | non-negative number |
| u | unsigned number |
| n|u | number |
| x | unspecified cell |
| xt | execution token |
| addr | address |
| a-addr | aligned address |
| c-addr | character-aligned address |
| d | double-cell signed number |
| +d | double-cell non-negative number |
| ud | double-cell unsigned number |
| d|ud | double-cell number |
| xd | unspecified cell pair |
| i\*x, j\*x, k\*x | any data type |
| r | float number |

As an addition to this table the FFL variables on the stack are represented by the
three alphabetic characters of the defining module. For example the stack
notation of the sh1-finish word, used in the Starting chapter is: ( sh1 -- u1 u2 u3 u4 u5 ).
This word expects a sh1 variable on stack and returns five unsigned numbers.
The FFL variables are actually aligned addresses (a-addr).

It is possible that a word returns different stack parameters after execution. This is
described by _after1_ | _after2_.

If a word has both compile time as run time behaviour, this is described by
( _before1_ -- _after1_ ; _before2_ -- _after2_ ). The stack parameters _before1_
and _after1_ specifies stack behaviour during compilation time and _before2_
and _after2_ during run time.

A few words in the FFL parse text during compilation. Those words use the following
syntax: ( _before_ "_parsed-text-abbreviation_" -- _after_ ). The
parsed-text-abbreviation uses the following specifications:

| **Abbreviation** | **Description** |
|:-----------------|:----------------|
| `<char>` | the char marking the end of the parsed string |
| `<chars>` | zero or more chars |
| `<space>` | a delimiting space character |
| `<spaces>` | zero or more spaces |
| `<quote>` | a delimiting double quote |
| `<paren>` | a delimiting right parenthesis |
| `<eol>` | a delimiting end of line |
| ccc | arbitrary characters, excluding the delimiter character |
| name | a token delimited by space |


## Word naming ##

There are two different type of words in the FFL: general use words and library words.

### General words ###

The general words perform small, general actions. They use normal, descriptive names. Exampes are nil<>, #bits/byte, begin-stringtable and so on.

### Library words ###

The name of the library words start with a prefix, followed by a special character and
ends with a descriptive name. The prefix is equal to the module name. The special character is one of the following:

| % | this word returns the size of the module variable, for example: sh1% |
|:--|:---------------------------------------------------------------------|
| . | this word is an enumeration, variable, value, constant of defer in the module, for example: sh1.version |
| + | this word expects **no** module variables on the stack, for example: frc+multiply |
| - | this word expects **one** module variable on the stack, for example: sh1-update |
| ^ | this word expects **two** module variables on the stack, for example: str^move |

The descriptive name specifies the action that the word performs on the optional stack
parameters. Some special characters are used for abbreviations:

| ? | the word performs a check and will return a boolean flag, for example: str-index? |
|:--|:----------------------------------------------------------------------------------|
| @ | the word fetches the state of an internal variable, for example: dtm-day@ |
| ! | the word stores the state of an internal variable, for example: dtm-day! |
| + | the word performs an addition or incrementation, for example: dti-seconds+ |
| - | the word performs an subtraction or decrementation, for example: dti-seconds- |
| ( ) | the word performs an action on the internal state, for example: str-(free) |

### Inconsistencies ###

Due to historical reasons there are two groups of words that do not follow this naming convention.
Those are _module-name_`-create` and _module-name_`-new`. Based on the name you
should expect that those words expect a module variable on the stack, but actually they don't. So they
should be named _module-name_`+create` and _module-name_`+new`, but again for historical
reasons they are not.

![http://c14.statcounter.com/1434121/0/407ca2a5/0/?img=stats.png](http://c14.statcounter.com/1434121/0/407ca2a5/0/?img=stats.png)