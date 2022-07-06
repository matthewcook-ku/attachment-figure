using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

// UXF Tracker to track Average Proxemics
//
// This tracker is used to pull average values from the ProxemicsTracker and log them through the tracker system. The only reason for a new tracker is to create a secondary tracker log file for this data. All actual calculations are part of the ProxemicsTracker.
//
// This tracker should be attached to the object to be tracked, or some child object with the same position. Make sure a ProxemicsTracker is attached to the same object and linked.
//
// See: https://github.com/immersivecognition/unity-experiment-framework/wiki/Tracker-system
//
// Connections:
// UXF_Rig - Data Collection Tab: add to tracked objects
// Subject - add to some child

public class ProxemicsAverageTracker : Tracker
{
    // define columns for the ouput file
    public override string MeasurementDescriptor => "average proxemics";
    public override IEnumerable<string> CustomHeader => new string[]
        {
            "system time",
            "average distance",
            "distance samples",
            "average gaze",
            "gaze samples"
        };
    public ProxemicsTracker proxemicsTracker = null;

    // called by UXF to write data to file
    protected override UXFDataRow GetCurrentValues()
    {
        var values = proxemicsTracker.GetAverageValues();
        return values;
    }
}