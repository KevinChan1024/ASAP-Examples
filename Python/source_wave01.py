#++
# SOURCE_WAVE01.PY
# Title: SOURCE WAVEFUNC Example
# Category: Isolated Command
# Keywords: SOURCE, WAVEFUNC
# Description: Allows defining the source
# point of rays in a grid to start from a
# wavefront surface defined by a surface function.
# In this example, the rays in the grid are moved
# along Z to the nearest point on the wavefront
# surface made with the OPTICAL command. 
# Edit History (latest first)
# 04/01/2002 - mpa - Converted to Python
# 03/11/2002 - cp - reformatted
# 10/13/2000 - cp - modified format; added description
# 01/01/1996 - bro - creation
#--

IKernel.kSystem ("NEW")
IKernel.kReset ()

IKernel.kUnits ("CM") 
IKernel.kFunction ()
IKernel.kOptical ("Z",0, -0.5, -1)
   
IKernel.kBeams ("INCOHERENT", "GEOMETRIC")
IKernel.kWavelengths (0.6328, "UM")
IKernel.kGrid ("ELLIPTIC", "Z", 0.1, "-4@1", "2@10")
IKernel.kSource ("WAVEFUNC", 0.1, "Z")
  
# Display rays to see result  
IKernel.kWindow ("X", "Z")
IKernel.kPlot ("RAYS", 1)
IKernel.k_View ()
IKernel.kReturn ()
