#++
# LENS_ARRAY01.PY
# Title: A Discrete Lens Array
# Category: Simple Problem
# Keywords: Macros_user, array, lenslet, macro
# Description: A micro-lens array with discrete lens
# objects using nested macro/do loops. This is better
# performed with bounding and using the ARRAY command.
# Edit History (latest first)
# 04/01/2016 - ma - Converted to Python
# 11/16/2000 - cp - creation
#--

def DoLenslet (row, column): 
  # create a single circular LENSLET at (x,y) 
  IKernel.kSurfaces ()
  IKernel.kOptical ("Z", 0, 1, "ELLIPSE 0.4")
  IKernel.kObject ("'LENS_SURF_FRONT_X" + str(column) + "_Y" + str(row) + "'")
  IKernel.kInterface ("COATING +TRANS AIR GLASS")
  
  IKernel.kSurfaces ()
  IKernel.kPlane ("Z 0.25 1 ELLIPSE 0.4")
  IKernel.kObject ("'LENS_SURF_BACK_X" + str(column) + "_Y" + str(row) + "'")
  IKernel.kInterface ("COATING +TRANS AIR GLASS")
  IKernel.kGroup (-2)
  IKernel.kShift ("X", column)
  IKernel.kShift ("Y", row)

def DoRow (row, columns):
  # create a row of LENSLETs
  for i in xrange(0, columns):
    DoLenslet (row, i+1)

def DoArray (rows, columns):
  # create the entire ARRAY from nested functions
  for i in xrange(0, rows):
    DoRow (i+1, columns)
           
IKernel.k__Tic ()
IKernel.kSystem ("NEW")
IKernel.kReset ()

IKernel.kMedia ()
IKernel.kFollow ("1.5 'GLASS'")
  
IKernel.kCoating ("PROPERTIES")
IKernel.kFollow ("0 1 'TRANS'")

rows = 5
columns = 8
DoArray (rows, columns)
IKernel.kPlot ("FACETS 3 3")
IKernel.k_View ()
IKernel.kReturn ()

IKernel.k_Tic ()