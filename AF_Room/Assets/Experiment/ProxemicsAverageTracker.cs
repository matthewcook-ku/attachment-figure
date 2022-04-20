using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

// This tracker is used to pull average values from the ProxemicsTracker and log them through the tracker system. The only reason for a new tracker is to create a secondary tracker log file for this data. All actual calculations are part of the ProxemicsTracker.
// Make sure a ProxemicsTracker is attached to the same object and linked. 
public class ProxemicsAverageTracker : Tracker
{
    public override string MeasurementDescriptor => "average proxemics";
    public override IEnumerable<string> CustomHeader => new string[]
        {
            "average distance",
            "distance samples",
            "average gaze",
            "gaze samples"
        };
    public ProxemicsTracker proxemicsTracker = null;

    private void Start()
    {
        if (null == proxemicsTracker) Debug.LogError("ProxemicsAverageTracker requires a link to a ProxemicsTracker!");
    }

    protected override UXFDataRow GetCurrentValues()
    {
        var values = proxemicsTracker.GetAverageValues();
        return values;
    }
}