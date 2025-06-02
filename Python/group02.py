#++
# GROUP02.PY
# Title: Grouping Objects for Shifting/Rotating
# Category: Isolated Command
# Keywords: GROUP, Manipulating
# Description: Select groups of objects to
# shift and/or rotate. Nine elliptical surfaces
# are created. The first 3 are shifted and rotated.
# The second 3 are shifted and rotated. The last 3
# are shifted. The plot shows the before and after.
# Edit History (latest first)
# 04/01/2016 - mpa - Converted to Python
# 10/18/2000 - cp - modified format and header
# 04/25/2000 - bf - creation
#--

IKernel.kSystem ("NEW")
IKernel.kReset ()

for i in xrange(1, 10):
  IKernel.kSurface ()
  IKernel.kPlane ("Z", -1 + i, "ELLIPSE", i, i)
  IKernel.kObject ("'ELLIPSE" + str(i) + "'")
  
IKernel.kWindow ("Y", -10, 20, "Z", -5, 35)
IKernel.kPlot ("FACETS", 9, 9, "OVERLAY")

IKernel.kGroup ("1:3") 
IKernel.kShift ("Z", 30)
IKernel.kRotate ("X", 45, 0, 30)

IKernel.kGroup ("4:6")
IKernel.kShift ("Y", 20)
IKernel.kRotate ("X", 30, 0, 0)

IKernel.kGroup ("7:9")
IKernel.kShift ("Z", 10)

IKernel.kWindow ("Y", "Z")
IKernel.kPlot ("FACETS", 9, 9)
IKernel.kReturn ()

