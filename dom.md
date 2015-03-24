### Module Description ###
The dom module implements a simplified XML Document Object Model. The
module reads a XML source into a tree of nodes. The tree can then be
iterated and modified. After modification the tree can be written to a
XML destination. As with every DOM implementation the tree will use a
lot of memory for large XML documents. Keep in mind that tree
modifications checks are limited. DTDs are not stored in the tree.
Depending on the node type the following stack state is expected by
dom-set, dom-append-node, dom-insert-node-before and
dom-insert-node-after:
```
dom.element:   -- c-addr u              = Tag name
dom.attribute: -- c-addr1 u1 c-addr2 u2 = Attribute name c-addr1 u1 and value c-addr2 u2
dom.text:      -- c-addr u              = Normal xml text
dom.cdata:     -- c-addr u              = CDATA section text
dom.pi:        -- c-addr u              = Proc. instr. target c-addr u
dom.comment:   -- c-addr n              = Comment
dom.document:  --                       = Document root
```

### Module Words ###
#### XML node types ####
**dom.not-used** ( -- n )
> DOM node: Not used
**dom.element** ( -- n )
> DOM node: Tag
**dom.attribute** ( -- n )
> DOM node: Attribute
**dom.text** ( -- n )
> DOM node: Text
**dom.cdata** ( -- n )
> DOM node: CDATA
**dom.entity-ref** ( -- n )
> DOM node: Entity reference [used](not.md)
**dom.entity** ( -- n )
> DOM node: Entitiy [used](not.md)
**dom.pi** ( -- n )
> DOM node: Processing Instruction
**dom.comment** ( -- n )
> DOM node: Comment
**dom.document** ( -- n )
> DOM node: Start document
**dom.doc-type** ( -- n )
> DOM node: Document type [used](not.md)
**dom.doc-fragment** ( -- n )
> DOM node: Document fragment [used](not.md)
**dom.notation** ( -- n )
> DOM node: Notation [used](not.md)
#### DOM structure ####
**dom%** ( -- n )
> Get the required space for a dom variable
#### DOM creation, initialisation and destruction ####
**dom-init** ( dom -- )
> Initialise the DOM
**dom-(free)** ( dom -- )
> Free the internal, private variables from the heap
**dom-create** ( "`<`spaces`>`name" -- ; -- dom )
> Create a named DOM in the dictionary
**dom-new** ( -- dom )
> Create a new DOM on the heap
**dom-free** ( dom -- )
> Free the DOM from the heap
#### Iterating the DOM tree ####
**dom-get** ( dom -- n true | false )
> Get the xml node type of the current node
**dom-get-type** ( dom -- n )
> Get the xml node type of the current node
**dom-get-name** ( dom -- c-addr u )
> Get the name from the current node
**dom-get-value** ( dom -- c-addr u )
> Get the value from the current node
**dom-document** ( dom -- true | false )
> Move the iterator to the document [=root] node
**dom-document?** ( dom -- flag )
> Check if the current node is the document [=root] node
**dom-parent** ( dom -- n true | false )
> Move the iterator to the parent node, return the xml type of this node
**dom-children** ( dom -- n )
> Return the number of children for the current node
**dom-children?** ( dom -- flag )
> Check if the current node has children
**dom-child** ( dom -- n true | false )
> Move the iterator to the first child node, return the xml type of this node
**dom-first** ( dom -- n true | false )
> Move the iterator to the first sibling node, return the xml type of this node
**dom-first?** ( dom -- flag )
> Check if the current node is the first sibling node
**dom-next** ( dom -- n true | false )
> Move the iterator to the next sibling node, return the xml type of this node
**dom-prev** ( dom -- n true | false )
> Move the iterator to the previous sibling node, return the xml type of this node
**dom-last** ( dom -- n true | false )
> Move the iterator to the last sibling node, return the xml type of this node
**dom-last?** ( dom -- flag )
> Check if the current node is the last sibling node
#### Modifying the DOM tree ####
**dom-set** ( i\*x dom -- )
> Update the current node
**dom-append-node** ( i\*x n dom -- )
> Append a node to the current node, exception if not allowed, iterator is moved to the new node
**dom-insert-node-before** ( i\*x n dom -- )
> Insert a node before the current node, exception if not allowed
**dom-insert-node-after** ( i\*x n -- )
> Insert a node after the current node, exception if not allowed
**dom-remove** ( dom -- flag )
> Remove the current sibling node without children from the tree, iterator is moved to the next, previous or parent node, return the removed node
#### Reading the DOM tree ####
**dom-read-string** ( c-addr u flag1 dom -- flag2 )
> Read xml source from the string c-addr u into the dom tree, flag1 indicates whitespace stripping, throw exception if tree is not empty, return success in flag2
**dom-read-reader** ( x xt flag1 dom -- flag2 )
> Read xml source with the reader xt with its state x into the dom tree, flag1 indicates whitespace stripping, throw exception if tree is not empty, return success in flag2
#### Writing the DOM tree ####
**dom-write-string** ( dom -- c-addr u true | false )
> Write the tree to xml returning a string c-addr u if successful
**dom-write-writer** ( x xt dom -- flag )
> Write the tree to xml using writer xt and its data x, flag indicate success
#### Inspection ####
**dom-dump** ( dom - )
> Dump the DOM tree
### Examples ###
```
include ffl/dom.fs


\ Example 1: Read xml from file, iterate and write to string


\ Create the xml-dom in the dictionary

dom-create dom1


\ Setup a file reader for the dom

: dom-reader   ( file-id -- c-addr u | 0 = Read the next chunk of the file )
  pad 64 rot read-file throw
  dup IF
    pad swap
  THEN
;


\ Open the source xml file

s" test.xml" r/o open-file throw value dom.input 


\ Read the test.xml file with the dom-reader in the dom, leading and trailing whitespace is skipped

dom.input ' dom-reader true dom1 dom-read-reader [IF]
  .( XML File is successfully read ) cr
[ELSE]
  .( XML File is not correct ) cr
[THEN]


\ Iterate in the dom the xml-document, start with the root

dom1 dom-document [IF]
  .( Iterate the start of the xml document ) cr
[ELSE]
  .( No document start ?? ) cr
[THEN]


\ Move the iterator to the first child of the xml-document

dom1 dom-child [IF]
  dup dom.attribute = [IF]
    drop
    .( Attribute with name: ) dom1 dom-get-name type .(  and value: ) dom1 dom-get-value type cr
  [ELSE]
    dup dom.element = [IF]
      drop
      .( Tag with name: ) dom1 dom-get-name type .(  and value: ) dom1 dom-get-value type cr
    [ELSE]
      dup dom.comment = [IF]
        drop
        .( Comment with value: ) dom1 dom-get-value type cr
      [ELSE]
        dup dom.text = [IF]
          drop
          .( Text with value: ) dom1 dom-get-value type cr
        [ELSE]
          dom.pi = [IF]
            .( Processing instruction with name: ) dom1 dom-get-name type .(  and value: ) dom1 dom-get-value type cr
          [ELSE]
            .( Perhaps a CDATA section ?) cr
          [THEN]
        [THEN]
      [THEN]
    [THEN]
  [THEN]
[ELSE]
  .( xml document has no children.) cr
[THEN]


\ Move the iterator to the next child of the xml-document

dom1 dom-next [IF]
  dup dom.attribute = [IF]
    drop
    .( Attribute with name: ) dom1 dom-get-name type .(  and value: ) dom1 dom-get-value type cr
  [ELSE]
    dup dom.element = [IF]
      drop
      .( Tag with name: ) dom1 dom-get-name type .(  and value: ) dom1 dom-get-value type cr
    [ELSE]
      dup dom.comment = [IF]
        drop
        .( Comment with value: ) dom1 dom-get-value type cr
      [ELSE]
        dup dom.text = [IF]
          drop
          .( Text with value: ) dom1 dom-get-value type cr
        [ELSE]
          dom.pi = [IF]
            .( Processing instruction with name: ) dom1 dom-get-name type .(  and value: ) dom1 dom-get-value type cr
          [ELSE]
            .( Perhaps a CDATA section ?) cr
          [THEN]
        [THEN]
      [THEN]
    [THEN]
  [THEN]
[ELSE]
  .( xml document has no more children.) cr
[THEN]


\ Write the xml

dom1 dom-write-string [IF]
  .( xml document: ) type cr
[ELSE]
  .( Problems writing the xml document.)  cr
[THEN]
[THEN]



\ Example 2:  Read xml from string and write to a file


\ Create the xml-dom on the heap

dom-new value dom2


\ Read xml from a string, skipping any leading and trailing whitespace

s" <?xml version='1.1'?>  <!-- test -->  <car>  <color>  blue  </color>  </car>" true dom2 dom-read-string [IF] 
  .( XML is sucessfully read.) cr
[ELSE]
  .( XML was not correct.) cr
[THEN]


\ Write the xml-dom to a file using a writer

: dom-writer  ( c-addr u file-id -- flag = Write the xml using a writer )
  write-file throw
  true
;


\ Open the file for the writer

s" out.xml" w/o create-file throw value dom.output


\ Write the xml-dom to the writer

dom.output ' dom-writer dom2 dom-write-writer [IF]
  .( XML is successfully written.) cr
[ELSE]
  .( Problems writing the xml-dom.) cr
[THEN]


\ Free the dom from the heap

dom2 dom-free


\ Example 3: build a xml document from scratch using the xml-dom


\ Create the xml-dom on the heap

dom-new value dom3


\ Start with the root, the xml-document

dom.document dom3 dom-append-node


\ Add the version attribute to the xml-document

s" version" s" 1.0" dom.attribute dom3 dom-append-node


\ Move back to the xml-document and add a tag

dom3 dom-parent 2drop
s" tag" dom.element dom3 dom-append-node


\ Add text to the element

s" hello" dom.text dom3 dom-append-node


\ Write the xml to a string

dom3 dom-write-string [IF]
  .( XML successfully written: ) type cr
[ELSE]
  .( Problems...) cr
[THEN]


\ Free the dom from the heap

dom3 dom-free
```

---

Generated by **ofcfrth-0.10.0**