'++
' SOURCE_WAVE01.VBS
' Title: SOURCE WAVEFUNC Example
' Category: Isolated Command
' Keywords: SOURCE, WAVEFUNC
' Description: Allows defining the source
' point of rays in a grid to start from a
' wavefront surface defined by a surface function.
' In this example, the rays in the grid are moved
' along Z to the nearest point on the wavefront
' surface made with the OPTICAL command. 
' Edit History (latest first)
' 12/13/2002 - ma - Converted to VBScript
' 03/11/2002 - cp - reformatted
' 10/13/2000 - cp - modified format; added description
' 01/01/1996 - bro - creation
'--

kSYSTEM "NEW"
kRESET

kUNITS "CM" 
kFUNCTION
  kOPTICAL "Z 0 -0.5 -1"
   
kBEAMS "INCOHERENT GEOMETRIC"
kWAVELENGTHS "0.6328 UM"
kGRID "ELLIPTIC Z 0.1 -4@1 2@10"
  kSOURCE "WAVEFUNC 0.1 Z"
  
' Display rays to see result  
kWINDOW "X Z"
kPLOT "RAYS 1"
k_VIEW
kRETURN
