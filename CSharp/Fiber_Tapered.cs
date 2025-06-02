//++
// FIBER_TAPERED.CS
// Title: Tapered Fiber as One Object
// Category: Simple Problem
// Keywords: Edges, fiber, taper, EDGES, POINTS, OBJECT
// Description: This creates a tapered fiber as one object
// using a set of EDGE POINTS at equal angular separation,
// which are extruded together using the second form of
// the OBJECT command.
// Note that the corners of the taper can have a tendency
// to leak if rays hit there exactly on axis from a grid.
// Avoid this by using a random set of rays.
// Edit History (latest first)
// 10/10/2016 - mpa - Converted top CSharp
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

        int RCX = -10;
        int RCY = 0;
        int RCZ = 0;
        int ANGLE = 45;
        int RADIUS = 30;

        kernel.kEdges();
        kernel.kPoints(0, 0, 0, 1);
        kernel.kFollow(RCX + RADIUS, 0, 0, -2);
        kernel.kFollow(RCX + ".", RCY + ".", RCZ + ".", ANGLE + ".");
        kernel.kFollow(RCX + RADIUS * kernel.kCos(ANGLE, 1), 0, -RADIUS * kernel.kSin(ANGLE, 1), 1);
        kernel.kFollow(0, 0, -RADIUS * kernel.kSin(ANGLE, 1), 0);
        kernel.kRotate("Z", 45, 0, 0);

        for (int i = 0; i < 3; i++)
        {
            kernel.kEdges();
            kernel.kRepeat("0.1");
            kernel.kRotate("Z", 90, 0, 0);
        }

        kernel.kObject();
        kernel.kFollow(-4, 1, 1, 1, "'EXTRUDED_WALLS'"); // Last 4 edges, connect odd to odd, connect
                                                         // even to even numbered, and connect
                                                         // last to first.
        kernel.kPlot("FACETS", 7, 7);
        kernel.k_View();
        kernel.kReturn();
    }
}
