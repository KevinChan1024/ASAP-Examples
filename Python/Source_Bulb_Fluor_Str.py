#++
# SOURCE_BULB_FLUOR_STR.PY
# Title: Straight Fluorescent Tube 
# Category: Simple Problem
# Keywords: Sources, Flourescent, bulb, CONE, RACETRACK, SCATTER, random, SUBSET
# Description: This example shows a simple straight fluorescent tube
# using a volume emitting source at center which is the
# length of the tube.  An inner tube surrounds this
# emitter whose surface properties are set up so that
# only the scattered rays get through and no specular rays.
# This simulates a plasma. Two macros are used, one
# calling another, to create the ends of the tube.
# Edit History (latest first)
# 04/01/2016 - ma - Converted to Python
# 10/07/2002 - cp - reformatted
# 02/03/2000 - cp - creation
#--

IKernel.k_Case ("upper")        
IKernel.kSystem ("NEW")
IKernel.kReset ()

IKernel.kUnits ("IN", "'LUMENS'")
IKernel.kWavelength (650, "NM")
IKernel.kBeams ("INCOHERENT", "GEOMETRIC")
IKernel.kXmemory ("MIN")
IKernel.kFresnel ("AVE")

IKernel.kMedia ()
IKernel.kFollow (1.49, "'GLASS'")

IKernel.kCoating ("PROPERTIES")
IKernel.kFollow (1, 0, "'REFLECT'")
IKernel.kFollow (0, 1, "'TRANSMIT'")
IKernel.kFollow (0, 0, "'ABSORB'")

NRAYS = 5000
OT_RAD = 1  # Outer tube radius
T_LEN = 24  # Length of bulb body
END_THK = 0.5

def MAKE_END (arg1, arg2): # Endcaps
  IKernel.kEdges() 
  IKernel.kRacetrack ("X", 0, OT_RAD / 2, END_THK / 2, 0.05, 0.05)
  IKernel.kShift ("Y", -OT_RAD / 2) 
  IKernel.kShift ("Z", (arg1) - END_THK / 2)
  IKernel.kSweep ("AXIS 361 0 0 1")

  IKernel.kObject ("'END_BODY." + str(arg2) + "'")
  IKernel.kRedefine ("COLOR 1")

  IKernel.kSurface ()
  IKernel.kPlane ("Z", 0, "ELLIPSE", OT_RAD - OT_RAD / 10, OT_RAD - OT_RAD / 10, 0, 0, 0)

  IKernel.kPlane ("Z", -.5, "ELLIPSE", OT_RAD - OT_RAD / 10, OT_RAD - OT_RAD / 10, 0, 0, 0)

  IKernel.kObject ()
  IKernel.kFollow (".1", "'ENDCAP1." + str(arg2) + "'")
  IKernel.kRedefine ("COLOR",  1)
  IKernel.kObject ()
  IKernel.kFollow (.2, "'ENDCAP2." + str(arg2) + "'")
  IKernel.kRedefine ("COLOR", 1)

# This macro calls the sub above to make the
# thick end caps of the tube.
def MAKE_BOTH_ENDS (arg1): # Do both ends
  MAKE_END (arg1, 1) # Make end cap on left

  # Connector pins first set
  IKernel.kSurface ()  # Left pins
  IKernel.kTube ("Z", arg1 - END_THK, .05, .05, arg1 - .75 - END_THK, .05, .05 , 0, 0)
  IKernel.kShift ("Y", 0.4)
  IKernel.kRepeat ()
  IKernel.kShift ("Y", -0.8)
  IKernel.kPlane ("Z", arg1 - END_THK - .75, "ELLIPSE", .05, .05, 0, 0, 0)
  IKernel.kShift ("Y", 0.4)
  IKernel.kRepeat ()
  IKernel.kShift ("Y", -0.8)
 
  IKernel.kObject ()
  IKernel.kFollow (".1", "'END.2'")
  IKernel.kRedefine ("COLOR",  1)
  IKernel.kObject ()
  IKernel.kFollow (".2", "'END.1'")
  IKernel.kRedefine ("COLOR", 1)
  IKernel.kObject ()
  IKernel.kFollow (".3", "'CONN.2'")
  IKernel.kRedefine ("COLOR", 1)
  IKernel.kObject ()
  IKernel.kFollow (".4", "'CONN.1'")
  IKernel.kRedefine ("COLOR", 1)
  
  MAKE_END (T_LEN + END_THK, 2) # Make end on right

  # Make second set of connector pins
  IKernel.kSurface () #!! Left pins
  IKernel.kTube ("Z", arg1 + T_LEN + END_THK, .05, .05, .75 + T_LEN + END_THK, .05, .05, 0, 0)
  IKernel.kShift ("Y", 0.4)
  IKernel.kRepeat ()
  IKernel.kShift ("Y", -0.8)
  IKernel.kPlane ("Z", arg1 + T_LEN + END_THK + .75, "ELLIPSE", .05, .05, 0, 0, 0)
  IKernel.kShift ("Y", 0.4)
  IKernel.kRepeat ()
  IKernel.kShift ("Y", -0.8)

  IKernel.kObject ()
  IKernel.kFollow (".1", "'END.4'")
  IKernel.kRedefine ("COLOR", 1)
  IKernel.kObject ()
  IKernel.kFollow (".2", "'END.3'")
  IKernel.kRedefine ("COLOR", 1)
  IKernel.kObject ()
  IKernel.kFollow (".3", "'CONN.4'")
  IKernel.kRedefine ("COLOR", 1)
  IKernel.kObject ()
  IKernel.kFollow (".4", "'CONN.3'")
  IKernel.kRedefine ("COLOR", 1)

# Make tubes
IKernel.kSurface ()
IKernel.kTube ("Z", 0, .5, .5, T_LEN, .5, .5, 0, 0)
IKernel.kTube ("Z", 0, OT_RAD, OT_RAD, T_LEN, OT_RAD, OT_RAD, 0, 0) 

IKernel.kObject ()
IKernel.kFollow (".2", "'INNER_TUBE'")
IKernel.kInterface (0, 1.E-12, "GLASS", "GLASS")  # Allows just enough trans to scatter
IKernel.kLevel (1)  # One level scattering
IKernel.kScatter ("RANDOM", 1, 10, 3.1415912/2)
IKernel.kObject ()
IKernel.kFollow (".1", "'OUTER_TUBE'")
IKernel.kInterface ("COATING", "+BARE", "AIR", "GLASS")
IKernel.kRedefine ("COLOR", 5)
IKernel.kSplit (1)
IKernel.kFresnel ("AVE")

# End Caps
IKernel.kSurface ()
IKernel.kPlane ("Z", 0, "ELLIP", OT_RAD, OT_RAD)
IKernel.kObject ("'LEFT_END_CAP'")
IKernel.kInterface ("COATING", "REFLECT", "GLASS", "AIR")
IKernel.kRedefine ("COLOR", 1)
IKernel.kSurface ()
IKernel.kPlane ("Z", T_LEN, "ELLIP", OT_RAD, OT_RAD)
IKernel.kObject ("'RIGHT_END_CAP'")
IKernel.kInterface ("COATING", "REFLECT", "GLASS", "AIR")
IKernel.kRedefine ("COLOR", 1)

MAKE_BOTH_ENDS (0)  # Make end caps and pins

# Source 
IKernel.kImmerse ("GLASS")  # Start rays in glass
IKernel.kEmitting ("CONE", "Z", 0, .3, .3, T_LEN, .3, .3, NRAYS)  # Volume emitter
IKernel.kMissed ("ARROW" ,2) 
IKernel.kWindow ("Y", "Z")
IKernel.kPlot ("FACETS", 2, 9, "OVERLAY") 
IKernel.kTrace ("PLOT", 75, "COLOR", 13)
IKernel.k_View ()

# Analysis
IKernel.kConsider ("ONLY", "OUTER_TUBE")
IKernel.kSubset () # Eliminate all rays except on outer tube
IKernel.kGet ()
F0 = IKernel.kGetVar("F0")
IKernel.kFlux (0, (F0*680)/(24*20))  # Plots should scale in 20
IKernel.kStats ()                     # lumens per sq.inch

IKernel.kWindow ("Y", 10, -10, "Z", -4, 34)
IKernel.kPixels (50)
IKernel.kRadiant () # For analysis in flux/cone angle

IKernel.kDisplay ()
for i in xrange(1, 4, 1):
  IKernel.kAverage ()# Reduces statistical noise
IKernel.kPlot3d ("'3D/ISOMETRIC FAR-FIELD INTENSITY'")
IKernel.kPicture ("'FAR-FIELD INTENSITY'")
IKErnel.kReturn ()

IKernel.kSpots ("DIRECTION", "ATTRIBUTE", 0)  # For analysis in dir cos space

# Replaces dist file with new data
IKernel.kDisplay ()
#'DIRECTIONAL 'DIRECTION PLOT'  #Directional plot
#'DIRECTIONAL UNWRAP  'LINEAR DIRECTION PLOT' # Linear plot of direction
#'AVERAGE -1 -1  # Use median value in smoothing over 3X3 pixels
IKernel.kRadial (0.5)     # Radially average about the midpoint of the distribution
IKernel.kDirectional ()   # Directional plot of flux/solid angle as projected in dir-cos space
IKernel.kContour (10)

IKernel.kReturn ()

                             