'++
' GetSet.VBS
' Title: Get Set Example
' Category: Misc scripting commands
' Keywords: kGetVar, kSetVar
' Description: Methods for getting and setting variables
' between the kernel and script languages
' Edit History (latest first)
' 12/13/2002 - ma - creation
'--

dim txt
dim nRays
dim nLen
dim flux

' Reset the system
txt = txt + kSYSTEM ("NEW")
txt = txt + kRESET ()

nRays = 500
nLen = 24

' set the variables in the kernel
kSetVar "NRAYS", (nRays)
kSetVar "NLEN", (nLen)
txt = txt + kEmitting ("CONE", "Z", 0, .3, .3, "(NLEN)", .3, .3, "(NRAYS)")  ' Volume emitter

' This will create a bunch of kernel variables
txt = txt + kGet ()

' Get a variable from the kernel
flux = kGetVar ("FLUX")

MsgBox  flux
MsgBox  txt









