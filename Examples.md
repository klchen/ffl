## Introduction ##

A number of modules are accompanied by small examples. These examples focus
mainly on the module itself. The examples on this page use several modules
and are more 'real life' examples.


## trk2rte ##

The purpose of the [trk2rte](http://ffl.googlecode.com/svn/trunk/examples/trk2rte.fs)
example is to accept multiple GPX files with track information, optional optimize
these tracks to reduce the number of points in the track and then export all tracks
as waypoints and routes in one GPX file or in a OZI waypoint and OZI route file.

For reducing the number of points the trk2rte example uses the angle method:
if the bearing of the next segment in comparison with the bearing of the current
segment is smaller than the threshold, the intermediate track point is removed.

The trk2rte example uses the following modules:
  * [snl - Generic Single Linked List](snl.md)
  * [str - Dynamic Strings](str.md)
  * [tos - Text Formatter](tos.md)
  * [xis - XML Parser](xis.md)
  * [xos - XML Writer](xos.md)

trk2rte requires FFL version 0.8.0.

![http://c14.statcounter.com/1434121/0/407ca2a5/0/?img=stats.png](http://c14.statcounter.com/1434121/0/407ca2a5/0/?img=stats.png)