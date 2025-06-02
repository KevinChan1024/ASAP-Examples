//++
// SOURCE_BULB_FLUOR_STR.CS
// Title: Straight Fluorescent Tube
// Category: Simple Problem
// Keywords: Sources, Flourescent, bulb, CONE, RACETRACK, SCATTER, random, SUBSET
// Description: This example shows a simple straight fluorescent tube
// using a volume emitting source at center which is the
// length of the tube.  An inner tube surrounds this
// emitter whose surface properties are set up so that
// only the scattered rays get through and no specular rays.
// This simulates a plasma. Two macros are used, one
// calling another, to create the ends of the tube.
// Edit History (latest first)
// 10/10/2016 - ma - Converted to CSharp
// 10/07/2002 - cp - reformatted
// 02/03/2000 - cp - creation
//--

using System;
using System.Windows.Forms;
using Kernel;

public class Script
{
    public static int NRAYS {get; set;}
    public static double OT_RAD {get; set;}
    public static double T_LEN {get; set;}
    public static double END_THK {get; set;}

    public static void Main(IKernel kernel)
    {
        kernel.k_Case("upper");
        kernel.kSystem("NEW");
        kernel.kReset();

        kernel.kUnits("IN", "'LUMENS'");
        kernel.kWavelength(650, "NM");
        kernel.kBeams("INCOHERENT", "GEOMETRIC");
        kernel.kXmemory("MIN");
        kernel.kFresnel("AVE");

        kernel.kMedia();
        kernel.kFollow(1.49, "'GLASS'");

        kernel.kCoating("PROPERTIES");
        kernel.kFollow(1, 0, "'REFLECT'");
        kernel.kFollow(0, 1, "'TRANSMIT'");
        kernel.kFollow(0, 0, "'ABSORB'");

        NRAYS = 5000;
        OT_RAD = 1;  //Outer tube radius
        T_LEN = 24;  //Length of bulb body
        END_THK = 0.5;

        // Make tubes
        kernel.kSurface();
        kernel.kTube("Z", 0, .5, .5, T_LEN, .5, .5, 0, 0);
        kernel.kTube("Z", 0, OT_RAD, OT_RAD, T_LEN, OT_RAD, OT_RAD, 0, 0);

        kernel.kObject();
        kernel.kFollow(".2", "'INNER_TUBE'");
        kernel.kInterface(0, 1E-12, "GLASS", "GLASS"); //Allows just enough trans to scatter
        kernel.kLevel(1); //One level scattering
        kernel.kScatter("RANDOM", 1, 10, Math.PI / 2);
        kernel.kObject();
        kernel.kFollow(".1", "'OUTER_TUBE'");
        kernel.kInterface("COATING", "+BARE", "AIR", "GLASS");
        kernel.kRedefine("COLOR", 5);
        kernel.kSplit(1);
        kernel.kFresnel("AVE");

        // End Caps
        kernel.kSurface();
        kernel.kPlane("Z", 0, "ELLIP", OT_RAD, OT_RAD);
        kernel.kObject("'LEFT_END_CAP'");
        kernel.kInterface("COATING", "REFLECT", "GLASS", "AIR");
        kernel.kRedefine("COLOR", 1);
        kernel.kSurface();
        kernel.kPlane("Z", T_LEN, "ELLIP", OT_RAD, OT_RAD);
        kernel.kObject("'RIGHT_END_CAP'");
        kernel.kInterface("COATING", "REFLECT", "GLASS", "AIR");
        kernel.kRedefine("COLOR", 1);

        MAKE_BOTH_ENDS(kernel, 0); //Make end caps and pins

        // Source
        kernel.kImmerse("GLASS"); //Start rays in glass
        kernel.kEmitting("CONE", "Z", 0, .3, .3, T_LEN, .3, .3, NRAYS); //Volume emitter
        kernel.kMissed("ARROW", 2);
        kernel.kWindow("Y", "Z");
        kernel.kPlot("FACETS", 2, 9, "OVERLAY");
        kernel.kTrace("PLOT", 75, "COLOR", 13);
        kernel.k_View();

        // Analysis
        kernel.kConsider("ONLY", "OUTER_TUBE");
        kernel.kSubset(); //Eliminate all rays except on outer tube
        kernel.kGet();
        float F0 = (float)kernel.kGetVar("F0");
        kernel.kFlux(0, (F0 * 680) / (24 * 20)); //Plots should scale in 20
        kernel.kStats(); //lumens per sq.inch

        kernel.kWindow("Y", 10, -10, "Z", -4, 34);
        kernel.kPixels(50);
        kernel.kRadiant(); //For analysis in flux/cone angle

        kernel.kDisplay();
        for (int ii = 0; ii < 3; ii++)
            kernel.kAverage(); //Reduces statistical noise
        kernel.kPlot3d ("'3D/ISOMETRIC FAR-FIELD INTENSITY'");
        kernel.kPicture ("'FAR-FIELD INTENSITY'");
        kernel.kReturn ();

        kernel.kSpots ("DIRECTION", "ATTRIBUTE", 0);  //For analysis in dir cos space

        // Replaces dist file with new data
        kernel.kDisplay ();
        //'DIRECTIONAL 'DIRECTION PLOT'  #Directional plot
        //'DIRECTIONAL UNWRAP  'LINEAR DIRECTION PLOT' # Linear plot of direction
        //'AVERAGE -1 -1  # Use median value in smoothing over 3X3 pixels
        kernel.kRadial (0.5);     //Radially average about the midpoint of the distribution
        kernel.kDirectional ();   //Directional plot of flux/solid angle as projected in dir-cos space
        kernel.kContour (10);

        kernel.kReturn ();
    }

    static void MAKE_END(IKernel kernel, double pos, int obj) //# Endcaps
    {
        kernel.kEdges();
        kernel.kRacetrack("X", 0, OT_RAD / 2, END_THK / 2, 0.05, 0.05);
        kernel.kShift("Y", -OT_RAD / 2);
        kernel.kShift("Z", (pos)-END_THK / 2);
        kernel.kSweep("AXIS 361 0 0 1");

        kernel.kObject("'END_BODY." + obj.ToString() + "'");
        kernel.kRedefine("COLOR 1");

        kernel.kSurface();
        kernel.kPlane("Z", 0, "ELLIPSE", OT_RAD - OT_RAD / 10, OT_RAD - OT_RAD / 10, 0, 0, 0);

        kernel.kPlane("Z", -.5, "ELLIPSE", OT_RAD - OT_RAD / 10, OT_RAD - OT_RAD / 10, 0, 0, 0);

        kernel.kObject();
        kernel.kFollow(".1", "'ENDCAP1." + obj.ToString() + "'");
        kernel.kRedefine("COLOR",  1);
        kernel.kObject();
        kernel.kFollow(.2, "'ENDCAP2." + obj.ToString() + "'");
        kernel.kRedefine("COLOR", 1);
    }

    // This macro calls the sub above to make the
    // thick end caps of the tube.
    static void MAKE_BOTH_ENDS(IKernel kernel, double pos) // Do both ends
    {
        MAKE_END(kernel, pos, 1); //Make end cap on left

        //Connector pins first set
        kernel.kSurface(); //Left pins
        kernel.kTube("Z", pos - END_THK, .05, .05, pos - .75 - END_THK, .05, .05, 0, 0);
        kernel.kShift("Y", 0.4);
        kernel.kRepeat();
        kernel.kShift("Y", -0.8);
        kernel.kPlane("Z", pos - END_THK - .75, "ELLIPSE", .05, .05, 0, 0, 0);
        kernel.kShift("Y", 0.4);
        kernel.kRepeat();
        kernel.kShift("Y", -0.8);

        kernel.kObject();
        kernel.kFollow(".1", "'END.2'");
        kernel.kRedefine("COLOR",  1);
        kernel.kObject();
        kernel.kFollow(".2", "'END.1'");
        kernel.kRedefine("COLOR", 1);
        kernel.kObject();
        kernel.kFollow(".3", "'CONN.2'");
        kernel.kRedefine("COLOR", 1);
        kernel.kObject();
        kernel.kFollow(".4", "'CONN.1'");
        kernel.kRedefine("COLOR", 1);

        MAKE_END(kernel, T_LEN + END_THK, 2); //Make end on right

        // Make second set of connector pins
        kernel.kSurface(); // Left pins
        kernel.kTube("Z", pos + T_LEN + END_THK, .05, .05, .75 + T_LEN + END_THK, .05, .05, 0, 0);
        kernel.kShift("Y", 0.4);
        kernel.kRepeat();
        kernel.kShift("Y", -0.8);
        kernel.kPlane("Z", pos + T_LEN + END_THK + .75, "ELLIPSE", .05, .05, 0, 0, 0);
        kernel.kShift("Y", 0.4);
        kernel.kRepeat();
        kernel.kShift("Y", -0.8);

        kernel.kObject();
        kernel.kFollow(".1", "'END.4'");
        kernel.kRedefine("COLOR", 1);
        kernel.kObject();
        kernel.kFollow(".2", "'END.3'");
        kernel.kRedefine("COLOR", 1);
        kernel.kObject();
        kernel.kFollow(".3", "'CONN.4'");
        kernel.kRedefine("COLOR", 1);
        kernel.kObject();
        kernel.kFollow(".4", "'CONN.3'");
        kernel.kRedefine("COLOR", 1);
    }
}


