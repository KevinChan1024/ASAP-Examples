//++
// GetSet.CS
// Title: Get Set Example
// Category: Misc scripting commands
// Keywords: kGetVar, kSetVar
// Description: Methods for getting and setting variables
// between the kernel and script languages
// Edit History (latest first)
// 10/10/2016 - ma - creation
//--

using System;
using System.Windows.Forms;
using Kernel;

public class Script
{
    public static void Main(IKernel kernel)
    {
        string txt = "";
        int nRays = 500;
        int nLen = 24;
        float flux;

        txt = "";

        // Reset the system
        txt += kernel.kSystem("NEW");
        txt += kernel.kReset();

        nRays = 500;
        nLen = 24;

        // set the variables in the kernel
        kernel.kSetVar("NRAYS", nRays);
        kernel.kSetVar("NLEN", nLen);
        txt += kernel.kEmitting("CONE", "Z", 0, .3, .3, "(NLEN)", .3, .3, "(NRAYS)"); // Volume emitter

        // This will create a bunch of kernel variables
        txt += kernel.kGet();

        // Get a variable from the kernel
        flux = (float)kernel.kGetVar("FLUX");

        kernel.kMsgBox(flux);
        kernel.kMsgBox(txt);
    }
}









