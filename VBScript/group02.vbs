'++
' GROUP02.VBS
' Title: Grouping Objects for Shifting/Rotating
' Category: Isolated Command
' Keywords: GROUP, Manipulating
' Description: Select groups of objects to
' shift and/or rotate. Nine elliptical surfaces
' are created. The first 3 are shifted and rotated.
' The second 3 are shifted and rotated. The last 3
' are shifted. The plot shows the before and after.
' Edit History (latest first)
' 13/13/2002 - ma - converted to VBScript
' 10/18/2000 - cp - modified format and header
' 04/25/2000 - bf - creation
'--

kSYSTEM "NEW"
kRESET

dim i

for i = 1 to 9 step 1 
  kSURFACE
  kPLANE "Z", -1 + (i), "ELLIPSE", (i), (i)
  kOBJECT "'ELLIPSE ", (i), "'"
next
  
kWINDOW "Y -10 20 Z -5 35"
kPLOT "FACETS 9 9 OVERLAY"

kGROUP "1:3" 
  kSHIFT "Z 30"
  kROTATE "X", 45, 0, 30

kGROUP "4:6"
  kSHIFT "Y 20"
  kROTATE "X", 30, 0, 0

kGROUP "7:9" 
  kSHIFT "Z 10"

kWINDOW "Y Z"
kPLOT "FACETS", 9, 9
kRETURN

