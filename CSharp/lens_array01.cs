//++
// LENS_ARRAY01.CS
// Title: A Discrete Lens Array
// Category: Simple Problem
// Keywords: Macros_user, array, lenslet, macro
// Description: A micro-lens array with discrete lens
// objects using nested macro/do loops. This is better
// performed with bounding and using the ARRAY command.
// Edit History (latest first)
// 10/10/2016 - ma - Converted to CSharp
// 11/16/2000 - cp - creation
//--

using System;
using System.Windows.Forms;
using Kernel;

public class Script
{
    public static void Main(IKernel kernel)
    {
        kernel.k__Tic();
        kernel.kSystem("NEW");
        kernel.kReset();

        kernel.kMedia();
        kernel.kFollow("1.5 'GLASS'");

        kernel.kCoating("PROPERTIES");
        kernel.kFollow("0 1 'TRANS'");

        DoArray(kernel);
        kernel.kPlot("FACETS 3 3");
        kernel.k_View();
        kernel.kReturn();

        kernel.k_Tic();
    }

    static void DoLenslet(IKernel kernel, int arg1, int arg2)
    {
        // create a single circular LENSLET at (x,y)
        kernel.kSurfaces();
        kernel.kOptical("Z", 0, 1, "ELLIPSE 0.4");
        kernel.kObject("'LENS_SURF_FRONT_X" + arg1.ToString() + "_Y" + arg2.ToString() + "'");
        kernel.kInterface("COATING +TRANS AIR GLASS");

        kernel.kSurfaces();
        kernel.kPlane("Z 0.25 1 ELLIPSE 0.4");
        kernel.kObject("'LENS_SURF_BACK_X" + arg1.ToString() + "_Y" + arg2.ToString() + "'");
        kernel.kInterface("COATING +TRANS AIR GLASS");
        kernel.kGroup(-2);
        kernel.kShift("X", arg1);
        kernel.kShift("Y", arg2);
    }

    static void DoRow(IKernel kernel, int arg1)
    {
        // create a row of LENSLETs
        for (int i = 1; i <= 8; i++)
            DoLenslet(kernel, i, arg1);
    }

    static void DoArray(IKernel kernel)
    {
        // create the entire ARRAY from nested functions
        for (int i = 1; i <= 5; i++)
            DoRow(kernel, i);
    }
}
