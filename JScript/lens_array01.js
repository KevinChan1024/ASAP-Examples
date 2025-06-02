//++
// LENS_ARRAY01.JS
// Title: A Discrete Lens Array
// Category: Simple Problem
// Keywords: Macros_user, array, lenslet, macro
// Description: A micro-lens array with discrete lens
// objects using nested macro/do loops. This is better
// performed with bounding and using the ARRAY command.
// Edit History (latest first)
// 12/13/2002 - ma - Converted to JScript
// 11/16/2000 - cp - creation
//--

function DoLenslet (arg1, arg2) 
{  
  // create a single circular LENSLET at (x,y) 
  kSurfaces ();
    kOptical ("Z", 0, 1, "ELLIPSE 0.4");
    kObject ("'LENS_SURF_FRONT_X" + arg1 + "_Y" + arg2 + "'");
      kInterface ("COATING +TRANS AIR GLASS");
  
  kSurfaces ();
    kPlane ("Z 0.25 1 ELLIPSE 0.4");
    kObject ("'LENS_SURF_BACK_X" + arg1 + "_Y" + arg2 + "'");
      kInterface ("COATING +TRANS AIR GLASS");
  kGroup (-2);
  kShift ("X", arg1);
  kShift ("Y", arg2);
}

function DoRow (arg1)
{  
  // create a row of LENSLETs
  var i;
  
  for (i = 1; i <= 8; i++)
    DoLenslet (i, arg1);
}

function DoArray ()
{  
  // create the entire ARRAY from nested functions
  var i;
  
  for (i = 1; i <= 5; i++)
    DoRow (i);
    
  return 0;
}
           
k__Tic ();           
kSystem ("NEW");
kReset ();

kMedia ();
  kFollow ("1.5 'GLASS'");
  
kCoating ("PROPERTIES");
  kFollow ("0 1 'TRANS'");

DoArray ();
kPlot ("FACETS 3 3");
k_View ();
kReturn ();

k_Tic ();