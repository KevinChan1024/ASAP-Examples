//++
// GetSet.JS
// Title: Get Set Example
// Category: Misc scripting commands
// Keywords: kGetVar, kSetVar
// Description: Methods for getting and setting variables
// between the kernel and script languages
// Edit History (latest first)
// 12/13/2002 - ma - creation
//--

var txt
var nRays
var nLen, flux
       
txt = "";       
       
// Reset the system
txt += kSystem ("NEW");
txt += kReset ();

nRays = 500;
nLen = 24;

// set the variables in the kernel           
kSetVar ("NRAYS", nRays);
kSetVar ("NLEN", nLen);
txt += kEmitting ("CONE", "Z", 0, .3, .3, "(NLEN)", .3, .3, "(NRAYS)"); // Volume emitter

// This will create a bunch of kernel variables
txt += kGet ();

// Get a variable from the kernel
flux = kGetVar ("FLUX");

kMsgBox (flux);
kMsgBox (txt);









