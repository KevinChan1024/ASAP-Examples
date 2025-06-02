'++
' LENS_ARRAY01.VBS
' Title: A Discrete Lens Array
' Category: Simple Problem
' Keywords: Macros_user, array, lenslet, macro
' Description: A micro-lens array with discrete lens
' objects using nested macro/do loops. This is better
' performed with bounding and using the ARRAY command.
' Edit History (latest first)
' 12/13/2002 - ma - Converted to VBScript
' 11/16/2000 - cp - creation
'--

kSYSTEM "NEW"
kRESET

kMEDIA
  kFollow "1.5", "'GLASS'"
  
kCOATINGS "PROPERTIES"
  kFollow "0 1 'TRANS'"

' create a single circular LENSLET at (x,y)         
sub LENSLET (arg1, arg2) 
  kSURFACE
    kOPTICAL "Z 0 1 ELLIPSE 0.4"
    kOBJECT "'LENS_SURF_FRONT_X" & arg1 & "_Y" & arg2 & "'"
      kINTERFACE "COATING +TRANS AIR GLASS"
  
  kSURFACE
    kPLANE "Z 0.25 1 ELLIPSE 0.4"
    kOBJECT "'LENS_SURF_BACK_X" & arg1 & "_Y" & arg2 & "'"
      kINTERFACE "COATING +TRANS AIR GLASS"
  kGROUP -2
  kSHIFT "X", (arg1)
  kSHIFT "Y", (arg2)
end sub
       
' create a row of LENSLETs 
sub ROW (arg1)
  dim i
  
  for i = 1 to 8
    call LENSLET (i, arg1)  
  next  
end sub

' create the entire ARRAY from nested macros 
sub DoARRAY ()  
  dim i
  
  for i = 1 to 5
    call ROW (i)   
  next
end sub

call DoARRAY
kPLOT "FACETS 3 3"
k_VIEW
kRETURN