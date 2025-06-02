'++
' SOURCE_BULB_FLUOR_STR.VBS
' Title: Straight Fluorescent Tube 
' Category: Simple Problem
' Keywords: Sources, Flourescent, bulb, CONE, RACETRACK, SCATTER, random, SUBSET
' Description: This example shows a simple straight fluorescent tube
' using a volume emitting source at center which is the
' length of the tube.  An inner tube surrounds this
' emitter whose surface properties are set up so that
' only the scattered rays get through and no specular rays.
' This simulates a plasma. Two macros are used, one
' calling another, to create the ends of the tube.
' Edit History (latest first)
' 12/13/2002 - ma - Converted to VBScript
' 10/07/2002 - cp - reformatted
' 02/03/2000 - cp - creation
'--

k_Case "upper"        
kSystem "NEW"
kReset

kUnits "IN 'LUMENS'"
kWavelength "650 NM"
kBeams "INCOHERENT GEOMETRIC"
kXmemory "MIN"
kFresnel "AVE"

kMedia
  kFollow "1.49 'GLASS'"

kCoating "PROPERTIES"
  kFollow "1 0 'REFLECT'"
  kFollow "0 1 'TRANSMIT'"
  kFollow "0 0 'ABSORB'"

NRAYS = 5000
OT_RAD = 1  ' Outer tube radius
T_LEN = 24  ' Length of bulb body
END_THK = 0.5

sub MAKE_END (arg1, arg2) '!! Endcaps'
  kEdges 
    kRacetrack "X", 0, OT_RAD / 2, END_THK / 2, 0.05, 0.05
      kShift "Y", -OT_RAD / 2 
      kShift "Z", (arg1) - END_THK / 2
      kSweep "AXIS 361 0 0 1"

  kObject "'END_BODY." & (arg2) & "'"
    kRedefine "COLOR 1"

  kSurface
    kPlane "Z", 0, "ELLIPSE", OT_RAD - OT_RAD / 10, OT_RAD - OT_RAD / 10, 0, 0, 0

    kPlane "Z", "-.5", "ELLIPSE", OT_RAD - OT_RAD / 10, OT_RAD - OT_RAD / 10, 0, 0, 0

  kObject
    kFollow ".1 'ENDCAP1." & arg2 & "'"
    kRedefine "COLOR 1"
  kObject
    kFollow ".2 'ENDCAP2." & arg2 & "'"
    kRedefine "COLOR 1"
end sub

' This macro calls the sub above to make the
' thick end caps of the tube.
sub MAKE_BOTH_ENDS (arg1) ' Do both ends
  call MAKE_END (arg1, 1) ' Make end cap on left

' Connector pins first set
  kSurface  ' Left pins
    kTube "Z", arg1 - END_THK, .05, .05, arg1 - .75 - END_THK, .05, .05 , 0, 0
    kShift "Y", 0.4
  kRepeat
    kShift "Y", -0.8
  kPlane "Z", arg1 - END_THK - .75, "ELLIPSE", .05, .05, 0, 0, 0
    kShift "Y", 0.4
  kRepeat
    kShift "Y", -0.8
 
  kObject
    kFollow ".1 'END.2'"
    kRedefine "COLOR 1"
  kObject
    kFollow ".2 'END.1'"
    kRedefine "COLOR 1"
  kObject
    kFollow ".3 'CONN.2'"
    kRedefine "COLOR 1"
  kObject 
    kFollow ".4 'CONN.1'"
    kRedefine "COLOR 1"
  
  call MAKE_END (T_LEN + END_THK, 2) ' Make end on right

' Make second set of connector pins
  kSurface  '!! Left pins
    kTube "Z", (arg1) + T_LEN + END_THK, .05, .05, .75 + T_LEN + END_THK, .05, .05, 0, 0
      kShift "Y", 0.4
  kRepeat
    kShift "Y -0.8"
  kPlane "Z", arg1 + T_LEN + END_THK + .75, "ELLIPSE", .05, .05, 0, 0, 0
    kShift "Y 0.4"
  kRepeat
    kShift "Y -0.8"

  kObject
    kFollow ".1 'END.4'"
    kRedefine "COLOR 1"
  kObject
    kFollow ".2 'END.3'"
    kRedefine "COLOR 1"
  kObject 
    kFollow ".3 'CONN.4'"
    kRedefine "COLOR 1"
  kObject 
    kFollow ".4 'CONN.3'"
    kRedefine "COLOR 1"
end sub

' Make tubes
kSurface
  kTube "Z", 0, .5, .5, (T_LEN), .5, .5, 0, 0
  kTube "Z", 0, (OT_RAD), (OT_RAD), (T_LEN), (OT_RAD), (OT_RAD), 0, 0 

kObject
  kFollow ".2 'INNER_TUBE'"
  kInterface "0 1.E-12 GLASS GLASS"  ' Allows just enough trans to scatter
  kLevel 1  ' One level scattering
  kScatter "RANDOM 1 10 3.1415912/2"
kObject
  kFollow ".1 'OUTER_TUBE'"
  kInterface "COATING +BARE AIR GLASS"
  kRedefine "COLOR 5"
  kSplit 1
  kFresnel "AVE"

' End Caps
kSurface
  kPlane "Z", 0, "ELLIP", (OT_RAD), (OT_RAD)
kObject "'LEFT_END_CAP'"
  kInterface "COATING  REFLECT  GLASS  AIR"
  kRedefine "COLOR 1"
kSurface
  kPlane "Z", (T_LEN), "ELLIP", (OT_RAD), (OT_RAD)
kObject "'RIGHT_END_CAP'"
  kInterface "COATING  REFLECT  GLASS  AIR"
  kRedefine "COLOR 1"

call MAKE_BOTH_ENDS (0)  ' Make end caps and pins

' Source 
kImmerse "GLASS"  ' Start rays in glass
kEmitting "CONE", "Z", 0, .3, .3, (T_LEN), .3, .3, (NRAYS)  ' Volume emitter
kMissed "ARROW 2" 
kWindow "Y Z"
kPlot "FACETS 2 9 OVER" 
kTrace "PLOT 75 COLOR 13"
k_View

' Analysis
kConsider "ONLY OUTER_TUBE"
kSubset  ' Eliminate all rays except on outer tube
kGet
kFlux "0 (F0*680)/(24*20)"  ' Plots should scale in 20
kStats                      ' lumens per sq.inch

kWindow "Y +10 -10 Z -4 +34"
kPixels 50
kRadiant  ' For analysis in flux/cone angle

dim i
          
kDisplay
  for i = 1 to 3 step 1
    kAverage ' Reduces statistical noise
  next
  kPlot3d "'3D/ISOMETRIC FAR-FIELD INTENSITY'"
kDisplay
  kPicture "'FAR-FIELD INTENSITY'"

kSpots "DIRECTION ATTRIBUTE 0"  ' For analysis in dir cos space
                                ' Replaces dist file with new data
kDisplay
  'DIRECTIONAL 'DIRECTION PLOT'  'Directional plot
  'DIRECTIONAL UNWRAP  'LINEAR DIRECTION PLOT' ' Linear plot of direction
  'AVERAGE -1 -1  ' Use median value in smoothing over 3X3 pixels
  kRadial "0.5"       ' Radially average about the midpoint of the distribution
  kDirectional      ' Directional plot of flux/solid angle as projected in dir-cos space
  kContour "10"
kReturn

                             