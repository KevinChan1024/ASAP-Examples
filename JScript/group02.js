//++
// GROUP02.JS
// Title: Grouping Objects for Shifting/Rotating
// Category: Isolated Command
// Keywords: GROUP, Manipulating
// Description: Select groups of objects to
// shift and/or rotate. Nine elliptical surfaces
// are created. The first 3 are shifted and rotated.
// The second 3 are shifted and rotated. The last 3
// are shifted. The plot shows the before and after.
// Edit History (latest first)
// 12/13/2002 - ma - Converted to JScript
// 10/18/2000 - cp - modified format and header
// 04/25/2000 - bf - creation
//--

kSystem ("NEW");
kReset ();

var i;          

for (i = 1; i <= 9; i++)
{
  kSurface ();
  kPlane ("Z", -1 + i, "ELLIPSE", i, i);
  kObject ("'ELLIPSE" + i + "'");
}  
  
kWindow ("Y", -10, 20, "Z", -5, 35);
kPlot ("FACETS", 9, 9, "OVERLAY");

kGroup ("1:3"); 
  kShift ("Z", 30);
  kRotate ("X", 45, 0, 30);

kGroup ("4:6");
  kShift ("Y", 20);
  kRotate ("X", 30, 0, 0);

kGroup ("7:9"); 
  kShift ("Z", 10);

kWindow ("Y", "Z");
kPlot ("FACETS", 9, 9);
kReturn ();

