## Getting started ##

First you need to include the module you want to use. For example, to include the fraction module (and all modules which are needed by the fraction module), you would do:

> `include ffl/frc.fs`

You can also include all modules at once with:

> `include ffl/ffl.fs`

Many modules store state in a variable. These modules provide three ways to allocate and initialize the variable. For example, after including the dynamic string module, you could:

1. Create a variable named var1 in the dictionary with:

> `str-create var1`

2. Dynamically allocate and free an str variable with:

> `str-new`<br>
<blockquote><code>str-free </code></blockquote>

3. If you prefer to allocate the variable yourself, you can get the necessary size with:<br>
<br>
<blockquote><code>str%</code></blockquote>

After allocating the variable, you must initialize it with:<br>
<br>
<blockquote><code>str-init</code></blockquote>

and when you are finished with the variable, free the internal, private variables with:<br>
<br>
<blockquote><code>str-(free)</code></blockquote>

The documentation for all modules is listed on the <a href='Modules.md'>Modules</a> page. There is an alphabetical list of all the words on the <a href='Words.md'>Words</a> page. For an increasing number of modules there are small code examples.<br>
<br>
<h2>Hands on</h2>

<h3>Modules</h3>

If you want to use a module, you start with including the module in your forth engine.<br>
For example if you want to do fraction calculations, you start with:<br>
<br>
<blockquote><code>include ffl/frc.fs</code></blockquote>

This will include the fraction module and also all modules that are needed by the fraction<br>
module. After including you can use the module. For example, if you want to multiply two<br>
fractions, you can enter:<br>
<br>
<blockquote><code>1 4 2 3 frc+multiply</code></blockquote>

This will multiply 1/4 times 2/3. By entering .s you will see the result: 2 12, which is 2/12.<br>
You can then normalize, convert the fraction to a string and type it by entering:<br>
<br>
<blockquote><code>frc+to-string type</code></blockquote>

This will show the result: 1/6<br>
<br>
<h3>Dictionary variables</h3>

The following example shows the use of modules that store their state in a variable. This example uses the sha1 module in the ffl library. First include the module in your forth engine:<br>
<br>
<blockquote><code>include ffl/sh1.fs</code></blockquote>

Then create a sha1 variable which will contain the state of the calculation. You can create the variable in the dictionary by entering:<br>
<br>
<blockquote><code>sh1-create var1</code></blockquote>

This creates a new word, var1, which is a sha1 variable. Next you can start calculating the sha1 algorithm by feeding data to the variable:<br>
<br>
<blockquote><code>s" ab" var1 sh1-update</code><br>
<code>s" c"  var1 sh1-update</code></blockquote>

In this example the sha1 variable is fed with the string abc. The next step is finishing the calculation. This is done by:<br>
<br>
<blockquote><code>var1 sh1-finish sh1+to-string type</code></blockquote>

So the sha1 calculation is finished and the resulting hash value is converted to a string, which is then typed. If all went well, it should show: A9993E364706816ABA3E25717850C26C9CD0D89D which is the secure hash1 result of the string "abc".<br>
<br>
<h3>Heap variables</h3>

Besides creating a sha1 variable in the dictionary, you can also allocate a sha1 variable on the heap by entering:<br>
<br>
<blockquote><code>sh1-new</code></blockquote>

Now there is a dynamically allocated sha1 variable on the stack. Feeding data to this variable can be done by:<br>
<br>
<blockquote><code>dup s" ab" rot sh1-update</code><br>
<code>dup s" c"   rot sh1-update</code></blockquote>

And finishing the calculation by:<br>
<br>
<blockquote><code>dup sh1-finish sh1+to-string type</code></blockquote>

Which resulted in the same sha1 value. When you don't need the dynamic variable anymore you can free the memory used by the variable by entering:<br>
<br>
<blockquote><code>sh1-free</code></blockquote>

<h2>Index</h2>

Some modules allow you to use an index to access members of a collection (an array, list, etc.). All indices are zero-based, so index 0 is the first element, index 1 is the second element, and so on.<br>
<br>
Negative indices can also be used, and index backward from the end of the collection. That is, index -1 is the last element in the collection, index -2 is the next-to-last element, and so on.<br>
<br>
If the value of an index is not valid in a collection, the exception exp-index-out-of-range is thrown.<br>
<br>
<h2>Reader</h2>

The tis, xis and dom modules use a reader word. This word makes it possible to feed these modules with data from different sources.<br>
<br>
For example for feeding data from a text file, the following reader can be used:<br>
<br>
<pre><code>: file-reader  ( file-id -- c-addr u | 0 = Read data from a text file)<br>
  pad 80 rot read-file throw<br>
  dup IF<br>
    pad swap<br>
  THEN<br>
;<br>
</code></pre>

The stack notation for the reader is <code>( x -- c-addr u | 0)</code>, in which <i>x</i> is the specific state for the reader and <i>c-addr u</i> is the read data.<br>
<br>
To let the <b>tis</b> module use this reader word, use the following example code:<br>
<br>
<pre><code>\ Open the 'file.txt' file and save the file-id in variable txt-file<br>
s" file.txt" r/o open-file throw   value txt-file<br>
<br>
\ Setup a file reader for tis1 variable<br>
txt-file  ' file-reader  tis1 tis-set-reader<br>
<br>
\ Close the file<br>
txt-file file-close throw<br>
</code></pre>

The stack notation for the <i>tis-set-reader</i> word is <code>(x xt tis -- )</code>, in which <i>x</i> is the specific state for the reader. This <i>x</i> is fed to the reader (see above). <i>Xt</i> is the reader word itself.<br>
<br>
The <b>xis</b> module can also use this reader word. See the following example code:<br>
<br>
<pre><code>\ Open the 'file.xml' file and save the file-id in variable reader-file<br>
s" file.xml" r/o open-file throw   value xml-file<br>
<br>
\ Setup a file reader for xis1 variable<br>
xml-file  ' file-reader  xis1 xis-set-reader<br>
<br>
\ Close the file<br>
xml-file file-close throw<br>
</code></pre>

<h2>Versions</h2>

As soon as a module of the ffl is included, the library version constant ffl.version is created. This constant returns the version number of the library. For example for version 0.4.0 the constant returns 000400.<br>
<br>
When a module is included, it defines a constant with the version of that module. The name of this constant is the name of the module followed by '.version'. So the constant frc.version returns the version of the fraction module.<br>
<br>
The constant for the module version starts with number 1 after the initial creation of the module. Then every time the interface of the module changes or the functionality of the module increases, the version number is increased by 1. The version number is not changed if a small bug is fixed in the module.<br>
<br>
The version constants can be used to check if the library or module is present in the forth dictionary: <code>[DEFINED] frc.version</code>. It can also be used to check if the code in the dictionary is up to date: <code>frc.version 2 =</code>.<br>
<br>
<br>
<h2>Collections</h2>

There are two types of collections in the library: generic collections and cell based collections.<br>
<br>
<h3>Generic collections</h3>

Generic collections implements data structures like linked lists, hash tables, trees. These collections do not store any data; they are the base code for implementing more specialised collections. Using a generic collection involves extending the generic code with code for the specific task.<br>
<br>
For example: using a single linked list for storing financial information about companies:<br>
<br>
First extend the generic single linked list node (invoice>node) with specific variables (invoice>customer, invoice>year, invoice>month, invoice>value):<br>
<br>
<pre><code>include ffl/snl.fs<br>
<br>
begin-structure invoice%<br>
  snn%<br>
  +field  invoice&gt;node<br>
  field:  invoice&gt;customer<br>
  field:  invoice&gt;year<br>
  field:  invoice&gt;month<br>
  ffield: invoice&gt;value<br>
end-structure<br>
</code></pre>

After that make a word that allocates and initialises a new invoice node:<br>
<br>
<pre><code>: invoice-new  ( F: r -- ; n1 n2 n3 -- invoice = Allocates and initialise an invoice node with r value, n1 customer, n2 year and n3 month )<br>
  invoice% allocate throw    \ Allocate the specific single list node<br>
  &gt;r<br>
  r@ invoice&gt;node snn-init   \ Initialise the single list node fields<br>
  r@ invoice&gt;month !         \ Initialise the specific fields<br>
  r@ invoice&gt;year !<br>
  r@ invoice&gt;customer !<br>
  r@ invoice&gt;value f!<br>
  r&gt;<br>
;<br>
</code></pre>

Then create the single list where the new invoice nodes are stored:<br>
<br>
<pre><code>snl-create invoices<br>
</code></pre>

Next is the allocation of some invoice nodes, these nodes are added to the invoices list:<br>
<br>
<pre><code>10.00E+0 1 2008 02 invoice-new invoices snl-append<br>
25.00E+0 1 2008 03 invoice-new invoices snl-append<br>
12.75E+0 1 2008 04 invoice-new invoices snl-append<br>
</code></pre>

After that you can use the words from the generic single linked list. For example print all the invoices in the list with the <code>snl-execute</code> word:<br>
<br>
<pre><code>: print-invoice ( invoice -- )<br>
  dup invoice&gt;year @ 4 .r   <br>
  [char] / emit  <br>
  dup invoice&gt;month @ 2 .r  <br>
  space [char] : emit space  <br>
  dup invoice&gt;customer @ 8 .r space <br>
  invoice&gt;value f@ f. cr<br>
;<br>
<br>
." Date      Customer Value" cr<br>
' print-invoice invoices snl-execute<br>
</code></pre>

Also the words from the generic single linked list iterator can be used with the invoices list:<br>
<br>
<pre><code>include ffl/sni.fs<br>
<br>
invoices sni-create invoices-iterator<br>
<br>
: print-invoices<br>
  ." Date      Customer Value" cr<br>
  invoices-iterator sni-first<br>
  BEGIN<br>
     nil&lt;&gt;?<br>
  WHILE<br>
    print-invoice<br>
    invoices-iterator sni-next<br>
  REPEAT<br>
;<br>
<br>
print-invoices<br>
</code></pre>

<h3>Cell based collections</h3>

The cell based collections implements data structures like linked list, hash tables, trees, that can store a single cell value.<br>
<br>
For example: using a single linked list for storing temperatures.<br>
<br>
First create the single linked list:<br>
<br>
<pre><code>include ffl/scl.fs<br>
<br>
scl-create temperatures<br>
</code></pre>

Then add the temperatures to the list:<br>
<br>
<pre><code>23 temperatures scl-append<br>
27 temperatures scl-append<br>
26 temperatures scl-append<br>
22 temperatures scl-append<br>
</code></pre>

After that you can use the list. For example: print the contents of the list:<br>
<br>
<pre><code>: print-temperature ( n1 n2 -- n3 = Print temperature n2 with ordered list number n1 )<br>
  over 2 .r space 4 .r cr<br>
  1+<br>
;<br>
<br>
1 ' print-temperature temperatures scl-execute drop<br>
</code></pre>

The list can also be iterated with the single linked cell based iterator:<br>
<br>
<pre><code>include ffl/sci.fs<br>
<br>
temperatures sci-create temperatures-iterator<br>
<br>
: print-temperatures ( -- )<br>
  1<br>
  temperatures-iterator sci-first<br>
  BEGIN<br>
  WHILE<br>
    print-temperature<br>
    temperatures-iterator sci-next<br>
  REPEAT<br>
  drop<br>
;<br>
<br>
print-temperatures<br>
</code></pre>

<h2>Inspection</h2>

A number of modules in the ffl library store their state in variables. These variables are based on a structure with different fields. To inspect those fields the modules define a word for inspecting their variables. The name of the inspecting word ends with <i>-dump</i>. For example the sha1 module defines the word <i>sh1-dump</i> for inspecting the sh1 variables. These words expect the variable that must be inspected on the stack. As a result they print the contents of all the fields of the variable on the console.<br>
<br>
<h2>Exceptions</h2>

At the moment the ffl library can raise ten different exceptions. Exceptions are raised if an unexpected situation is detected that can not be resolved. The exception numbers can also be returned by words as io-results. The following exceptions are present:<br>
<br>
exp-index-out-of-range (-2050)<br>
<blockquote>an index is used in a collection, array or string that is not within the range of valid  indices<br>
exp-invalid-state (-2051)<br>
a word of the module is called that is not allowed in the current state of the variable<br>
exp-no-data (-2052)<br>
a word tries to fetch data, but unexpectly there is no data available<br>
exp-invalid-parameters (-2053)<br>
a parameter on the stack is invalid<br>
exp-wrong-file-type (-2054)<br>
a file for which an import is attempted, is not of the correct type<br>
exp-wrong-file-version (-2055)<br>
a file for which an import is attempted, has not the correct version<br>
exp-wrong-file-data (-2056)<br>
a file for which an import is attempted, has data outside the expected ranges<br>
exp-wrong-checksum<br>
the calculated checksum and the checksum of the source do not match<br>
exp-wrong-length<br>
the actual length and the length of the source do not match<br>
exp-invalid-data<br>
the data of the source is different from what is expected</blockquote>

GForth (and FLK and ALF) report the exception with a message, all the other forth engines report the numerical value of the exception.<br>
<br>
<img src='http://c14.statcounter.com/1434121/0/407ca2a5/0/?img=stats.png' />
