#++
# POINTS01.PY
# Title: POINTS and Weighting
# Category: Isolated Command
# Keywords: POINTS, EDGES  
# Description: Shows the effect of varying
# weighting factors and control vertices.
# Edit History (latest first)
# 04/01/2016 - ma - Converted to Python
# 10/04/2000 - cp - modified format; added description
# 01/01/1996 - bro - creation
#--

IKernel.kSystem ("NEW")
IKernel.kReset ()

SS = 1
TT = 0.02

def VERTEX (arg1):
  IKernel.kEdges ();
  IKernel.kPoints (0, SS, 0, 2, SS, SS, 0, IKernel.kCos (arg1, 1), SS, 0, 0, 0);
  IKernel.kObject ("'q_CONTROL=" + str(IKernel.kCos (arg1, 1)) + "'");

for i in xrange(0, 91, 15):
  VERTEX (i)
  
IKernel.kEdges ()
IKernel.kPoints (0, SS, 0, 1, SS, SS, 0, 1, SS, 0, 0, 0)
IKernel.kObject ("'OUTER'")
 
IKernel.kSegments (20)
IKernel.kWindow ("Y 0 1 X 0 1")
IKernel.kTitle ("'POINTS CONNECTED WITH q=COS(X], X=0-90 IN STEPS OF 15 DEGREES'")
IKernel.kPlot ("EDGES TEXT")  # To label plot
IKernel.kFollow ("-0.17", "0.87", 0, TT, 0, 0, 0, TT, 0, "'FIRST POINT'")
IKernel.kFollow ("0.70", 0, 0, TT, 0, 0, 0, TT, 0, "'SECOND POINT'")
IKernel.kFollow ("0.70", "0.95", 0, TT, 0, 0, 0, TT, 0, "'CONTROL VERTEX'")

IKernel.kReturn ()
