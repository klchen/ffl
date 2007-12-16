\ ==============================================================================
\
\        xos_test - the test words for the xos module in the ffl
\
\               Copyright (C) 2007  Dick van Oudheusden
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
\  $Date: 2007-12-16 19:37:18 $ $Revision: 1.4 $
\
\ ==============================================================================

include ffl/xis.fs
include ffl/est.fs
include ffl/tst.fs


.( Testing: xis) cr

t{ xis-create xis1 }t

t{ s" <!--Comment1--><![CDATA[Hallo]]>&gt;Test1&amp;Test2&unknown;Test3&lt;<!--Comment2--></tag ><bold />" xis1 xis-set-string }t 

t{ xis1 xis-read xis.comment       ?s s" Comment1" compare ?0 }t
t{ xis1 xis-read xis.cdata         ?s s" Hallo"    compare ?0 }t
t{ xis1 xis-read xis.text          ?s s" >Test1&Test2&unknown;Test3<" compare ?0 }t
t{ xis1 xis-read xis.comment       ?s s" Comment2" compare ?0 }t
t{ xis1 xis-read xis.end-tag       ?s s" tag"      compare ?0 }t
t{ xis1 xis-read xis.empty-element ?s s" bold"     compare ?0 }t
\ ==============================================================================