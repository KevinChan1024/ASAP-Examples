//++
// FIBER_TAPERED.JS
// Title: Tapered Fiber as One Object
// Category: Simple Problem
// Keywords: Edges, fiber, taper, EDGES, POINTS, OBJECT
// Description: This creates a tapered fiber as one object
// using a set of EDGE POINTS at equal angular separation,
// which are extruded together using the second form of
// the OBJECT command.
// Note that the corners of the taper can have a tendency
// to leak if rays hit there exactly on axis from a grid.
// Avoid this by using a random set of rays. 
// Edit History (latest first)
// 12/13/2002 - ma - Converted to JScript
// 02/03/2000 - cp - creation
//--

kSystem ("NEW");
kReset ();

var RCX = -10;
var RCY = 0;
var RCZ = 0;
var ANGLE = 45; 
var RADIUS = 30;

kEdges ();
  kPoints ("0 0 0 1");
    kFollow (RCX + RADIUS, 0, 0, -2);
    kFollow (RCX + ".", RCY + ".", RCZ + ".", ANGLE + ".");
    kFollow (RCX + RADIUS * kCos (ANGLE, 1), 0, -RADIUS * kSin (ANGLE, 1), 1);
    kFollow (0, 0, -RADIUS * kSin (ANGLE, 1), 0);
  kRotate ("Z", 45, 0, 0);

var i;

for (i = 0; i < 3; i++)
{
  kEdges ();
  kRepeat ("0.1");
  kRotate ("Z", 90, 0, 0);
}  

kObject ();
  kFollow (-4, 1, 1, 1, "'EXTRUDED_WALLS'"); // Last 4 edges, connect odd to odd, connect 
                                             // even to even numbered, and connect
                                             // last to first.
kPlot ("FACETS", 7, 7);
k_View ();
kReturn ();