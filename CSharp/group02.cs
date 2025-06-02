//++
// GROUP02.CS
// Title: Grouping Objects for Shifting/Rotating
// Category: Isolated Command
// Keywords: GROUP, Manipulating
// Description: Select groups of objects to
// shift and/or rotate. Nine elliptical surfaces
// are created. The first 3 are shifted and rotated.
// The second 3 are shifted and rotated. The last 3
// are shifted. The plot shows the before and after.
// Edit History (latest first)
// 10/10/2016 - ma - Converted to CSharp
// 10/18/2000 - cp - modified format and header
// 04/25/2000 - bf - creation
//--

using System;
using System.Windows.Forms;
using Kernel;

public class Script
{
    public static void Main(IKernel kernel)
    {
        kernel.kSystem("NEW");
        kernel.kReset();

        for (int i = 1; i <= 9; i++)
        {
            kernel.kSurface();
            kernel.kPlane("Z", -1 + i, "ELLIPSE", i, i);
            kernel.kObject("'ELLIPSE" + i + "'");
        }

        kernel.kWindow("Y", -10, 20, "Z", -5, 35);
        kernel.kPlot("FACETS", 9, 9, "OVERLAY");

        kernel.kGroup("1:3");
        kernel.kShift("Z", 30);
        kernel.kRotate("X", 45, 0, 30);

        kernel.kGroup("4:6");
        kernel.kShift("Y", 20);
        kernel.kRotate("X", 30, 0, 0);

        kernel.kGroup("7:9");
        kernel.kShift("Z", 10);

        kernel.kWindow("Y", "Z");
        kernel.kPlot("FACETS", 9, 9);
        kernel.kReturn();
    }
}
