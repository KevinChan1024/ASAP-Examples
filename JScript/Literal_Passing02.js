//++
// LITERAL_PASSING02.JS
// Title: Passing Variables to Plot Titles
// Category: Simple Problem
// Keywords: Scripts, literal, string, variable, macro, plot, title
// Description: Passing variables to plot titles using the LIT
// operator and a macro. 
// Edit History (latest first)
// 02/03/2000 - ma - Converted to JScript
// 02/03/2000 - cp - creation
//--

// The following demonstrates passing a literal to a graph title
function GraphIt (arg1)
{
  kDisplay (); // Put in display mode here for GRAPH to work.
    kAverage (0, 39);
    kGraph ("'DISTRIBUTION OF LIGHT ON TARGET WITH " + arg1 + " ROUGHNESS'");
  kReturn ();
}

kSystem ("NEW");
kReset ();

// The following is only for creating a display file for the graph
var RFNSS = 0.1;

kGrid ("RECT", "Z", 5, -2, 2, -2, 2, 100, 100);
  kSource ("DIRECTION 0 0 1");
kSurface ();
  kOptical ("Z", 10.001, -10.001, "ELLIPSE", 10);
  kObject ("'REFLECTOR'");
    kInterface (1, 0);
    kRoughness (0, RFNSS, 2);
kSurface ();
  kPlane ("Z", -5, "RECT", 15);
  kObject ("'TARGET'");
 
kTrace ();
kConsider ("ONLY TARGET");
kWindow ("Y X");
kSpots ("POSITION ATTRIBUTE 0");

GraphIt (RFNSS);
kReturn ();


