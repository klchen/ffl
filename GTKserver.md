## Introduction ##

The GTK-server interface module (gsv) in the FFL library implements an ANS
compatible way to use the GTK library. This module is added in version 0.8.0.

To actually display the GUI, the [GTK-server](http://www.gtk-server.org/) tool
is used. This tool can receive GTK calls through a pipe or TCP-IP connection from
an external process (the forth engine).

The gsv module supports this by connecting to the GTK-server, translating the
forth calls to GTK-server calls, sending the calls, receiving the response and
translating the response back.

This way the module makes it possible for a forth engine to display a GTK GUI.

Currently the module supports UNIX/LINUX. Support for Windows will be added at
a later time.


## Installation ##

You will need the GTK library and the [GTK-server](http://www.gtk-server.org/)
application. See the relevant docs for installing.


## Getting Started ##

For using the GTK-server it is important to read the relevant documentation.
In particular the documentation of the gtk-server.cfg file is very
important. This file describes what requests can be made to the GTK-server.
By adding or deleting calls in this file, you can optimize the
gtk-server.cfg file for your project. For example the gtk-server.cfg
file used in the examples directory of the ffl library, is expanded
with a number of requests to make the examples possible.

For a project using GTK and Forth, the best setup is a dedicated
directory. In this directory you can put the project specific
gtk-server.cfg file. This file can then be used by the GTK-server
and the gsv-module. As a result both use the same configuration file.

After a gtk-server.cfg is copied to the project directory, you can
start the GTK-server (on linux) in this project directory:

> `gtk-server -fifo=prj-fifo &`

The prj-fifo is the fifo filename that is used to communicate with the
GTK-server. You can use any file name. All the examples in the ffl
library use the 'ffl-fifo' filename.

The next step is to start the forth engine (gforth):

> `gforth`

In gforth you should include the gsv-module:

> `include ffl/gsv.fs`

Then read the gtk-server.cfg file and open a connection to the GTK-server:

> `s" gtk-server.cfg" s" prj-fifo" gsv+open throw`

The gsv+open call returns an ior. If it is 0, there is a connection to
the GTK-server and the gtk-server.cfg is processed.

From now on you can send GTK calls to the GTK-server. First
initialise the toolkit:

> `gtk_init`

Then create a toplevel window and save the handle in the window value:

> `GTK_WINDOW_TOPLEVEL gtk_window_new VALUE window`

Give the window a title:

> `s" Title: hello world" window gtk_window_set_title`

Show the window:

> `window gtk_widget_show`

Wait for events:

> `s" WAIT" gtk_server_callback 2drop`

The window is shown. After pressing the close button of the window ('X'),
the call will return.

Then enter:

> `0 gtk_exit`

This will close all windows and stop the GTK-server process.


## Examples ##

There are a number of examples in the library available. These are
inspired by the
[GTK-tutorial](http://library.gnome.org/devel/gtk-tutorial/stable/).

The examples use the fifo name: ffl-fifo. To run an example
start the GTK-server in the example directory:

> `gtk-server -fifo=ffl-fifo &`

Then start one of the following forth engines with the example file:

> `gforth gsv_expl.fs`

> `pfe gsv_expl.fs`

> `spf4 ffl/config.fs gsv_expl.fs`

> `lxf include gsv_expl.fs`

> `vfxlin include gsv_expl.fs`

The following examples are shipped with the library (in the examples directory):

| **Example file** | **Description** |
|:-----------------|:----------------|
| gsv\_expl.fs    | [Hello World](http://ffl.googlecode.com/svn/trunk/examples/gsv_expl.fs) |
| gsv2\_expl.fs   | [Button Widget](http://ffl.googlecode.com/svn/trunk/examples/gsv2_expl.fs) |
| gsv3\_expl.fs   | [Box Packing](http://ffl.googlecode.com/svn/trunk/examples/gsv3_expl.fs) |
| gsv4\_expl.fs   | [Table Packing](http://ffl.googlecode.com/svn/trunk/examples/gsv4_expl.fs) |
| gsv5\_expl.fs   | [Pixmap Button](http://ffl.googlecode.com/svn/trunk/examples/gsv5_expl.fs) |
| gsv6\_expl.fs   | [Toggle Button Widget](http://ffl.googlecode.com/svn/trunk/examples/gsv6_expl.fs) |
| gsv7\_expl.fs   | [Adjustment Widget](http://ffl.googlecode.com/svn/trunk/examples/gsv7_expl.fs) |
| gsv8\_expl.fs   | [Label Widget](http://ffl.googlecode.com/svn/trunk/examples/gsv8_expl.fs) |
| gsv9\_expl.fs   | [Arrow & Tooltip Widget](http://ffl.googlecode.com/svn/trunk/examples/gsv9_expl.fs) |
| gsv10\_expl.fs  | [Timer & Progress bar Widget](http://ffl.googlecode.com/svn/trunk/examples/gsv10_expl.fs) |
| gsv12\_expl.fs  | [Statusbar Widget](http://ffl.googlecode.com/svn/trunk/examples/gsv12_expl.fs) |
| gsv13\_expl.fs  | [Entry Widget](http://ffl.googlecode.com/svn/trunk/examples/gsv13_expl.fs) |
| gsv14\_expl.fs  | [Spinbutton Widget](http://ffl.googlecode.com/svn/trunk/examples/gsv14_expl.fs) |
| gsv17\_expl.fs  | [File Chooser Dialog](http://ffl.googlecode.com/svn/trunk/examples/gsv17_expl.fs) |
| gsv18\_expl.fs  | [Event Box](http://ffl.googlecode.com/svn/trunk/examples/gsv18_expl.fs) |
| gsv19\_expl.fs  | [Fixed Container](http://ffl.googlecode.com/svn/trunk/examples/gsv19_expl.fs) |
| gsv20\_expl.fs  | [Frame](http://ffl.googlecode.com/svn/trunk/examples/gsv20_expl.fs) |
| gsv21\_expl.fs  | [Aspect Frame](http://ffl.googlecode.com/svn/trunk/examples/gsv21_expl.fs) |
| gsv23\_expl.fs  | [Scrolled Window](http://ffl.googlecode.com/svn/trunk/examples/gsv23_expl.fs) |
| gsv24\_expl.fs  | [Button Box](http://ffl.googlecode.com/svn/trunk/examples/gsv24_expl.fs) |
| gsv26\_expl.fs  | [Notebook Widget](http://ffl.googlecode.com/svn/trunk/examples/gsv26_expl.fs) |
| gsv27\_expl.fs  | [Menu](http://ffl.googlecode.com/svn/trunk/examples/gsv27_expl.fs) |
| gsv99\_expl.fs  | [Mandelbrot](http://ffl.googlecode.com/svn/trunk/examples/gsv99_expl.fs) |

[![](http://ffl.googlecode.com/svn/wiki/mandelbrot.png)](http://ffl.googlecode.com/svn/trunk/examples/gsv99_expl.fs)


## Trouble shooting ##

My experience is that the GTK-server will not always clean up the fifo
file after an error. You can manually remove it (on linux):

> `rm prj-fifo`

The GTK-server is running as a process. You can see the process ID
by entering:

> `ps -e | grep gtk-server`

This ID can then be used to stop the GTK-server after problems:

> `kill ID`


## How it works ##

The GTK-server uses the gtk-server.cfg file to determine which requests can be
made and what the arguments are for that requests.

To keep the gsv-module in sync with the GTK-server, the module reads the
gtk-server.cfg file. It parses the file and translates the contents into forth
words and forth constants in the dictionary.


## Own gtk-server.cfg ##

If you use your own gtk-server.cfg, then make sure that the definition of
**gtk\_server\_enable\_c\_string\_escaping** is present in your configuration file.
The gsv module uses this call to check if the gtk calls are loaded, if the
connection to the gtk-server is working and to support quoted strings.

If this call is missing in your gtk-server.cfg file, the gsv module will
not work.


## Type conversion ##

The gsv-module translates the arguments of the requests in the gtk-server.cfg file
to the following forth stack data types :

| **gtk-server.cfg argument datatype** | **forth stack data type** |
|:-------------------------------------|:--------------------------|
| NULL       |   |
| WIDGET     | n |
| POINTER    | n |
| BOOL       | flag |
| STRING     | c-addr u |
| INT        | n |
| LONG       | d |
| DOUBLE     | r |
| FLOAT      | r |
| ENUM       | n |
| MACRO      | c-addr u |
| BASE64     | c-addr u |

Note: The argument STRING is quoted by the gsv-module.

The return value of the requests in the gtk-server.cfg file is translated
to the following forth data type:

| **gtk-server.cfg returnvalue datatype** | **forth stack data type** |
|:----------------------------------------|:--------------------------|
| NONE       |   |
| VOID       |   |
| WIDGET     | n |
| POINTER    | n |
| ADDRESS    | n |
| BOOL       | flag |
| STRING     | c-addr u |
| INT        | n |
| LONG       | d |
| DOUBLE     | r |
| FLOAT      | r |

Note: Keep in mind that returned addresses and pointers refer to memory
in the GTK-server and not in the forth engine.


## Events ##

Special attention should be given to the event handling by the GTK-server.
The server links a default event to most widget types. If this default
event is triggered, the widget ID is returned by gtk\_server\_callback. See
the gtk-server.cfg file for the default event per widget type.

If another event triggering is necessary for a widget, the
`gtk_server_connect` request must be used to connect an event to a widget.
According to the gtk-server.cfg file, the type of the ID for this event is string,
but this is not always convenient. So the gsv-module also defines:
`gsv+server-connect` and `gsv+server-connect-after`. Those two words use
the same parameters as the `gtk_server_connect` call, except that the event
ID is numerically.

Events that are triggered, are returned to your program via the `gtk_server_callback`
request. According to the gtk-server.cfg file this request returns the event in a
string. If you make sure that all events are numerically, you can easily convert the
string event ID to a number and use the CASE - OF - ENDOF - ENDCASE construct to check
which event is triggered. Most examples in the library use this method.

Another method is the use of the [AVL binary tree](act.md). The key in this tree is
the numerical event ID and the linked data is the execution token (XT) of the
forth word that must be executed when the event is triggered. When an event
is created, the event ID and the XT of the callback word are stored in the tree.
In the mainloop the event ID, that is returned by `gtk_server_callback`, is
fed to the tree. The tree will return the callback XT which is then executed.
See [Menu](http://ffl.googlecode.com/svn/trunk/examples/gsv27_expl.fs) for an example
using this method.

![http://c14.statcounter.com/1434121/0/407ca2a5/0/?img=stats.png](http://c14.statcounter.com/1434121/0/407ca2a5/0/?img=stats.png)