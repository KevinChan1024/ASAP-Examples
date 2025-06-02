//++
// SOURCE_WAVE01.CS
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
// 10/10/2016 - ma - Converted to CSharp
// 03/11/2002 - cp - reformatted
// 10/13/2000 - cp - modified format; added description
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

        kernel.kUnits("CM");
        kernel.kFunction();
        kernel.kOptical("Z 0 -0.5 -1");

        kernel.kBeams("INCOHERENT GEOMETRIC");
        kernel.kWavelengths(0.6328,"UM");
        kernel.kGrid("ELLIPTIC Z 0.1 -4@1 2@10");
        kernel.kSource("WAVEFUNC 0.1 Z");

        // Display rays to see result
        kernel.kWindow("X Z");
        kernel.kPlot("RAYS 1");
        kernel.k_View();
        kernel.kReturn();
    }
}
