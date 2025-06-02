//++
// LITERAL_PASSING02.CS
// Title: Passing Variables to Plot Titles
// Category: Simple Problem
// Keywords: Scripts, literal, string, variable, macro, plot, title
// Description: Passing variables to plot titles using the LIT
// operator and a macro.
// Edit History (latest first)
// 10/10/2016 - ma - Converted to CSharp
// 02/03/2000 - cp - creation
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

        // The following is only for creating a display file for the graph
        float RFNSS = 0.1f;

        kernel.kGrid("RECT", "Z", 5, -2, 2, -2, 2, 100, 100);
        kernel.kSource("DIRECTION 0 0 1");
        kernel.kSurface();
        kernel.kOptical("Z", 10.001, -10.001, "ELLIPSE", 10);
        kernel.kObject("'REFLECTOR'");
        kernel.kInterface(1, 0);
        kernel.kRoughness(0, RFNSS, 2);
        kernel.kSurface();
        kernel.kPlane("Z", -5, "RECT", 15);
        kernel.kObject("'TARGET'");

        kernel.kTrace();
        kernel.kConsider("ONLY TARGET");
        kernel.kWindow("Y X");
        kernel.kSpots("POSITION ATTRIBUTE 0");

        GraphIt(kernel, RFNSS);
        kernel.kReturn();
    }

    // The following demonstrates passing a literal to a graph title
    static void GraphIt(IKernel kernel, float arg1)
    {
        kernel.kDisplay(); // Put in display mode here for GRAPH to work.
        kernel.kAverage(0, 39);
        kernel.kGraph("'DISTRIBUTION OF LIGHT ON TARGET WITH " + arg1.ToString() + " ROUGHNESS'");
        kernel.kReturn();
    }

}


