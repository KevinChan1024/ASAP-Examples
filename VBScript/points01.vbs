'++
' POINTS01.VBS
' Title: POINTS and Weighting
' Category: Isolated Command
' Keywords: POINTS, EDGES  
' Description: Shows the effect of varying
' weighting factors and control vertices.
' Edit History (latest first)
' 12/13/2002 - ma - Converted to VBScript
' 10/04/2000 - cp - modified format; added description
' 01/01/1996 - bro - creation
'--

kSYSTEM "NEW"
kRESET

dim i, SS, TT       
       
SS = 1
TT = 0.02

sub VERTEX (arg1)
kEDGE
  kPOINTS 0, (SS), 0, 2, (SS), (SS), 0, kCOS (arg1, 1), (SS), 0, 0, 0
kOBJECT "'q_CONTROL=COS[" & arg1 & "]'"
end sub

for i = 0 to 90 step 15                
  call VERTEX (i)          
next
  
kEDGE
  kPOINTS 0, (SS), 0, 1, (SS), (SS), 0, 1, (SS), 0, 0, 0
kOBJECT "'OUTER'"
 
kSEGMENTS 20
  kWINDOW "Y", 0, 1, "X", 0, 1
kTITLE "'POINTS CONNECTED WITH q=COS(X], X=0-90 IN STEPS OF 15 DEGREES'"
kPLOT "EDGES", "TEXT"  ' To label plot
  kFollow "-0.17", "0.87", 0, (TT), 0, 0, 0, (TT), 0, "'FIRST POINT'"
  kFollow "0.70", 0, 0, (TT), 0, 0, 0, (TT), 0, "'SECOND POINT'"
  kFollow "0.70", "0.95", 0, (TT), 0, 0, 0, (TT), 0, "'CONTROL VERTEX'" 

kRETURN
