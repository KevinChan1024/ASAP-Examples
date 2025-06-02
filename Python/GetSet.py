#++
# GetSet.PY
# Title: Get Set Example
# Category: Misc scripting commands
# Keywords: kGetVar, kSetVar
# Description: Methods for getting and setting variables
# between the kernel and script languages
# Edit History (latest first)
# 04/01/2016 - mpa - creation
#--

txt = ""
       
# Reset the system
txt += IKernel.kSystem ("NEW")
txt += IKernel.kReset ()

nRays = 500
nLen = 24

# set the variables in the kernel           
IKernel.kSetVar ("NRAYS", nRays);
IKernel.kSetVar ("NLEN", nLen);
txt += IKernel.kEmitting ("CONE", "Z", 0, .3, .3, "(NLEN)", .3, .3, "(NRAYS)") # Volume emitter

# This will create a bunch of kernel variables
txt += IKernel.kGet ();

# Get a variable from the kernel
flux = IKernel.kGetVar ("FLUX");

IKernel.kMsgBox (flux);
IKernel.kMsgBox (txt);









