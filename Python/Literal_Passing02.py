#++
# LITERAL_PASSING02.PY
# Title: Passing Variables to Plot Titles
# Category: Simple Problem
# Keywords: Scripts, literal, string, variable, macro, plot, title
# Description: Passing variables to plot titles using the LIT
# operator and a macro. 
# Edit History (latest first)
# 04/01/2016 - ma - Converted to Python
# 02/03/2000 - cp - creation
#--

# The following demonstrates passing a literal to a graph title
def GraphIt (arg1):
  IKernel.kDisplay () # Put in display mode here for GRAPH to work.
  IKernel.kAverage (0, 39)
  IKernel.kGraph ("'DISTRIBUTION OF LIGHT ON TARGET WITH " + str(arg1) + " ROUGHNESS'")
  IKernel.kReturn ()

IKernel.kSystem ("NEW")
IKernel.kReset ()

# The following is only for creating a display file for the graph
RFNSS = 0.1

IKernel.kGrid ("RECT", "Z", 5, -2, 2, -2, 2, 100, 100)
IKernel.kSource ("DIRECTION 0 0 1")
IKernel.kSurface ()
IKernel.kOptical ("Z", 10.001, -10.001, "ELLIPSE", 10)
IKernel.kObject ("'REFLECTOR'")
IKernel.kInterface (1, 0)
IKernel.kRoughness (0, RFNSS, 2)
IKernel.kSurface ()
IKernel.kPlane ("Z", -5, "RECT", 15)
IKernel.kObject ("'TARGET'")
 
IKernel.kTrace ()
IKernel.kConsider ("ONLY TARGET")
IKernel.kWindow ("Y X")
IKernel.kSpots ("POSITION ATTRIBUTE 0")

GraphIt (RFNSS)
IKernel.kReturn ()


