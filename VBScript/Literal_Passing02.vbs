'++
' LITERAL_PASSING02.VBS
' Title: Passing Variables to Plot Titles
' Category: Simple Problem
' Keywords: Scripts, literal, string, variable, macro, plot, title
' Description: Passing variables to plot titles using the LIT
' operator and a macro. 
' Edit History (latest first)
' 12/13/2002 - ma - Converted to VBScript
' 02/03/2000 - cp - creation
'--

kSYSTEM "NEW"
kRESET

' The following is only for creating a display file for the graph
dim RFNSS
RFNSS = 0.1

kGRID "RECT", "Z", 5, -2, 2, -2, 2, 100, 100
  kSOURCE "DIRECTION", 0, 0, 1
kSURFACE
  kOPTICAL "Z", "10.001", "-10.001", "ELLIPSE", 10
  kOBJECT "'REFLECTOR'"
    kINTERFACE 1, 0
    kROUGHNESS "RANDOM", 0, (RFNSS), 2
kSURFACE
  kPLANE "Z", -5, "RECT", 15
  kOBJECT "'TARGET'"
 
kTRACE
kCONSIDER "ONLY", "TARGET"
kWINDOW "Y", "X"
kSPOTS "POSITION", "ATTRIBUTE", 0

' The following demonstrates passing a literal to a graph title
sub GRAPHIT (arg1)
  kDISPLAY ' Put in display mode here for GRAPH to work.
    kAVERAGE 0, 39
    kGRAPH "'DISTRIBUTION OF LIGHT ON TARGET WITH " & arg1 & " ROUGHNESS'"
  kRETURN
end sub

call GRAPHIT (RFNSS)
kRETURN

