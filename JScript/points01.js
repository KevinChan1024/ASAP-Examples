//++
// POINTS01.JS
// Title: POINTS and Weighting
// Category: Isolated Command
// Keywords: POINTS, EDGES  
// Description: Shows the effect of varying
// weighting factors and control vertices.
// Edit History (latest first)
// 12/13/2002 - ma - Converted to JScript
// 10/04/2000 - cp - modified format; added description
// 01/01/1996 - bro - creation
//--

kSystem ("NEW");
kReset ();

var i, SS = 1, TT = 0.02;

function VERTEX (arg1)
{
  kEdges ();
    kPoints (0, SS, 0, 2, SS, SS, 0, kCos (arg1, 1), SS, 0, 0, 0);
  kObject ("'q_CONTROL=" + kCos (arg1, 1) + "'");
} 

for (i = 0; i <= 90; i += 15)
  VERTEX (i);
  
kEdges ();
  kPoints (0, SS, 0, 1, SS, SS, 0, 1, SS, 0, 0, 0);
kObject ("'OUTER'");
 
kSegments (20);
  kWindow ("Y 0 1 X 0 1");
kTitle ("'POINTS CONNECTED WITH q=COS(X], X=0-90 IN STEPS OF 15 DEGREES'");
kPlot ("EDGES TEXT");  // To label plot
  kFollow ("-0.17", "0.87", 0, TT, 0, 0, 0, TT, 0, "'FIRST POINT'");
  kFollow ("0.70", 0, 0, TT, 0, 0, 0, TT, 0, "'SECOND POINT'");
  kFollow ("0.70", "0.95", 0, TT, 0, 0, 0, TT, 0, "'CONTROL VERTEX'"); 

kReturn ();
