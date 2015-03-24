### Module Description ###
The trm module implements an outputter for terminal escape sequences.
It supports a selection of ANSI, VT100, VT102, ECMA-48 and linux console
specific escape sequences in order to perform special terminal actions like
colors, cursor movements, inserting, erasing lines, etc.
Note: the module uses the pictured output buffer.

### Module Words ###
#### Attributes ####
**trm.reset** ( -- u )
> Reset attributes to defaults
**trm.bold** ( -- u )
> Set bold
**trm.half-bright** ( -- u )
> Set half bright
**trm.underscore-on** ( -- u )
> Set underscore
**trm.blink-on** ( -- u )
> Set blink
**trm.reverse-on** ( -- u )
> Set reverse video
**trm.normal-intensity** ( -- u )
> Set normal intensity
**trm.underline-off** ( -- u )
> Reset underline
**trm.blink-off** ( -- u )
> Reset blink
**trm.reverse-off** ( -- u )
> Reset reverse
**trm.foreground-black** ( -- u )
> Set black foreground
**trm.foreground-red** ( -- u )
> Set red foreground
**trm.foreground-green** ( -- u )
> Set green foreground
**trm.foreground-brown** ( -- u )
> Set brown foreground
**trm.foreground-blue** ( -- u )
> Set blue foreground
**trm.foreground-magenta** ( -- u )
> Set magenta foreground
**trm.foreground-cyan** ( -- u )
> Set cyan foreground
**trm.foreground-white** ( -- u )
> Set white foreground
**trm.foreground-def-underline** ( -- u )
> Set default foreground with underscore on
**trm.foreground-default** ( -- u )
> Set default foreground
**trm.background-black** ( -- u )
> Set black background
**trm.background-red** ( -- u )
> Set red background
**trm.background-green** ( -- u )
> Set green background
**trm.background-brown** ( -- u )
> Set brown background
**trm.background-blue** ( -- u )
> Set blue background
**trm.background-magenta** ( -- u )
> Set magenta background
**trm.background-cyan** ( -- u )
> Set cyan background
**trm.background-white** ( -- u )
> Set white background
**trm.background-default** ( -- u )
> Set default background
#### Terminal words ####
**trm+reset** ( -- )
> Reset the terminal
**trm+save-current-state** ( -- )
> Save the current state: cursor, attributes and character sets
**trm+restore-current-state** ( -- )
> Restore the current state: cursor, attributes and character sets
#### Tab words ####
**trm+set-tab-stop** ( -- )
> Set tab stop at current column
**trm+clear-tab-stop** ( -- )
> Clear tab stop at current column
**trm+clear-all-tab-stops** ( -- )
> Clear all tab stops
#### Scroll words ####
**trm+set-scroll-region** ( u1 u2 -- )
> Set the scroll region rows with top u2 and bottom u1, both ranging from 1 till numbers of rows
**trm+scroll-up** ( -- )
> Scroll the display up
**trm+scroll-down** ( -- )
> Scroll the display down
**trm+reset-scroll-region** ( -- )
> Reset the scroll region rows
#### Cursor words ####
**trm+move-cursor-up** ( u -- )
> Move cursor up u rows, always at least one row
**trm+move-cursor-down** ( u -- )
> Move cursor down u rows, always at least one row
**trm+move-cursor-right** ( u -- )
> Move cursor right u columns, always at least one column
**trm+move-cursor-left** ( u -- )
> Move cursor left u columns, always at least one column
**trm+move-cursor** ( u1 u2 -- )
> Move cursor to column and row with x u1 and y u2, both starting from 1
**trm+get-cursor** ( -- u1 u2 )
> Get the current cursor position with x u1 and y u2, both starting from 1
**trm+save-cursor** ( -- )
> Save cursor location
**trm+restore-cursor** ( -- )
> Restore cursor location
#### Erase display words ####
**trm+erase-display-down** ( -- )
> Erase display from cursor to end
**trm+erase-display-up** ( -- )
> Erase from start display to cursor
**trm+erase-display** ( -- )
> Erase the whole display
#### Erase line words ####
**trm+erase-end-of-line** ( -- )
> Erase the line from cursor to end of line
**trm+erase-start-of-line** ( -- )
> Erase the line from start line to cursor
**trm+erase-line** ( -- )
> Erase the whole line
#### Insert and delete lines words ####
**trm+insert-lines** ( u -- )
> Insert u blank lines
**trm+delete-lines** ( u -- )
> Delete u lines
#### Character words ####
**trm+insert-spaces** ( u -- )
> Insert u spaces
**trm+delete-chars** ( u -- )
> Delete n characters on the current line
**trm+erase-chars** ( u -- )
> Erase u characters on the current line
#### Attribute words ####
**trm+set-attributes** ( u1 .. un n -- )
> Set n attributes
#### LED words ####
**trm+clear-all-leds** ( -- )
> Clear all LEDs
**trm+set-scroll-led** ( -- )
> Set the scroll lock LED
**trm+set-num-led** ( -- )
> Set the num lock LED
**trm+set-caps-led** ( -- )
> Set the caps lock LED
#### Character set words ####
**trm+select-default-font** ( -- )
> Select the default character set
**trm+select-alternate-font** ( -- )
> Select the alternate character set
#### Linux console words ####
**trm+set-default-attributes** ( -- )
> Set the current attributes the default attributes
**trm+set-screen-blank-timeout** ( u -- )
> Set the screen blank timeout in minutes
**trm+activate-console** ( u -- )
> Bring the console to the front
**trm+unblank-screen** ( -- )
> Unblank the screen
**trm+select-default** ( -- )
> Select the default character set ISO8859-1
**trm+select-UTF-8** ( -- )
> Select the UTF-8 character set
**trm+select-graphics-font2** ( -- )
> Select the vt100 graphics font for the alternate font


---

Generated by **ofcfrth-0.10.0**