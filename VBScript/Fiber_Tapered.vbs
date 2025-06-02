'++
' FIBER_TAPERED.VBS
' Title: Tapered Fiber as One Object
' Category: Simple Problem
' Keywords: Edges, fiber, taper, EDGES, POINTS, OBJECT
' Description: This creates a tapered fiber as one object
' using a set of EDGE POINTS at equal angular separation,
' which are extruded together using the second form of
' the OBJECT command.
' Note that the corners of the taper can have a tendency
' to leak if rays hit there exactly on axis from a grid.
' Avoid this by using a random set of rays. 
' Edit History (latest first)
' 12/13/2002 - ma - Converted to VBScript
' 02/03/2000 - cp - creation
'--

kSYSTEM "NEW"
kRESET

dim i, RCX, RCY, RCZ, ANGLE, RADIUS
RCX = -10
RCY = 0
RCZ = 0
ANGLE = 45
RADIUS = 30

kEDGE
  kPOINTS 0, 0, 0, 1, (RCX + RADIUS), 0, 0, -2, (RCX), (RCY), (RCZ), (ANGLE), (RCX + RADIUS * kCOS((ANGLE), 1)), 0, (-RADIUS * kSIN((ANGLE), 1)), 1, 0, 0, (-RADIUS * kSIN ((ANGLE), 1)), 0
  kROTATE "Z 45 0 0"

for i = 1 to 3          
  kEDGES
  kREPEAT "0.1"
  kROTATE "Z 90 0 0"
next  

kOBJECT                                ' Last 4 edges, connect odd to odd, connect 
  kFollow "-4 1 1 1 'EXTRUDED_WALLS'"  ' even to even numbered, and connect
                                       ' last to first.
kPLOT "FACETS 7 7"
k_VIEW
kRETURN