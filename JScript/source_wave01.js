//++
// SOURCE_WAVE01.JS
// Title: SOURCE WAVEFUNC Example
// Category: Isolated Command
// Keywords: SOURCE, WAVEFUNC
// Description: Allows defining the source
// point of rays in a grid to start from a
// wavefront surface defined by a surface function.
// In this example, the rays in the grid are moved
// along Z to the nearest point on the wavefront
// surface made with the OPTICAL command. 
// Edit History (latest first)
// 12/13/2002 - ma - Converted to JScript
// 03/11/2002 - cp - reformatted
// 10/13/2000 - cp - modified format; added description
// 01/01/1996 - bro - creation
//--

kSystem ("NEW");
kReset ();

kUnits ("CM"); 
kFunction ();
  kOptical ("Z 0 -0.5 -1");
   
kBeams ("INCOHERENT GEOMETRIC");
kWavelengths ("0.6328 UM");
kGrid ("ELLIPTIC Z 0.1 -4@1 2@10");
  kSource ("WAVEFUNC 0.1 Z");
  
// Display rays to see result  
kWindow ("X Z");
kPlot ("RAYS 1");
k_View ();
kReturn ();
