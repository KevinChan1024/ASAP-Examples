//++
// POINTS01.CS
// Title: POINTS and Weighting
// Category: Isolated Command
// Keywords: POINTS, EDGES
// Description: Shows the effect of varying
// weighting factors and control vertices.
// Edit History (latest first)
// 10/10/2016 - ma - Converted to CSharp
// 10/04/2000 - cp - modified format; added description
// 01/01/1996 - bro - creation
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

        int SS = 1;
        float TT = 0.02f;

        for (int i = 0; i <= 90; i += 15)
            VERTEX(kernel, (double)i, SS);

        kernel.kEdges();
        kernel.kPoints(0, SS, 0, 1, SS, SS, 0, 1, SS, 0, 0, 0);
        kernel.kObject("'OUTER'");

        kernel.kSegments(20);
        kernel.kWindow("Y 0 1 X 0 1");
        kernel.kTitle("'POINTS CONNECTED WITH q=COS(X], X=0-90 IN STEPS OF 15 DEGREES'");
        kernel.kPlot("EDGES TEXT");  // To label plot
        kernel.kFollow("-0.17", "0.87", 0, TT, 0, 0, 0, TT, 0, "'FIRST POINT'");
        kernel.kFollow("0.70", 0, 0, TT, 0, 0, 0, TT, 0, "'SECOND POINT'");
        kernel.kFollow("0.70", "0.95", 0, TT, 0, 0, 0, TT, 0, "'CONTROL VERTEX'");

        kernel.kReturn();
    }

    static double toRadians(double degrees)
    {
        return degrees * (Math.PI / 180);
    }

    static void VERTEX(IKernel kernel, double degrees, float SS)
    {
        kernel.kEdges();
        // Below are two different ways to calculate COS from an angle in degrees
        //kernel.kPoints(0, SS, 0, 2, SS, SS, 0, kernel.kCos(arg1, 1), SS, 0, 0, 0);
        kernel.kPoints(0, SS, 0, 2, SS, SS, 0, Math.Cos(toRadians(degrees)), SS, 0, 0, 0);
        kernel.kObject(string.Format("'q_CONTROL=COS[{0}]'", degrees));
    }
}

