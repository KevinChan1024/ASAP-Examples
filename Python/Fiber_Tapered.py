#++
# FIBER_TAPERED.PY
# Title: Tapered Fiber as One Object
# Category: Simple Problem
# Keywords: Edges, fiber, taper, EDGES, POINTS, OBJECT
# Description: This creates a tapered fiber as one object
# using a set of EDGE POINTS at equal angular separation,
# which are extruded together using the second form of
# the OBJECT command.
# Note that the corners of the taper can have a tendency
# to leak if rays hit there exactly on axis from a grid.
# Avoid this by using a random set of rays. 
# Edit History (latest first)
# 04/01/2016 - mpa - Converted to Python
# 02/03/2000 - cp - creation
#--

IKernel.kSystem ("NEW")
IKernel.kReset ()

RCX = -10
RCY = 0
RCZ = 0
ANGLE = 45 
RADIUS = 30

IKernel.kEdges ()
IKernel.kPoints (0, 0, 0, 1, (RCX + RADIUS), 0, 0, -2, RCX, RCY, RCZ, ANGLE, RCX + RADIUS * IKernel.kCos(ANGLE, 1), 0, -RADIUS * IKernel.kSin(ANGLE, 1), 1, 0, 0, -RADIUS * IKernel.kSin (ANGLE, 1), 0)
IKernel.kRotate ("Z", 45, 0, 0)

for i in xrange(3):
  IKernel.kEdges ()
  IKernel.kRepeat ("0.1")
  IKernel.kRotate ("Z", 90, 0, 0)

IKernel.kObject ()
IKernel.kFollow (-4, 1, 1, 1, "'EXTRUDED_WALLS'")  # Last 4 edges, connect odd to odd, connect 
                                                   # even to even numbered, and connect
                                                   # last to first.
IKernel.kPlot ("FACETS", 7, 7)
IKernel.k_View ()
IKernel.kReturn ()