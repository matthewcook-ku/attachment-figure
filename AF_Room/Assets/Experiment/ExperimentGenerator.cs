using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// add the UXF namespace
using UXF;

public class ExperimentGenerator : MonoBehaviour
{
    public int numberOfTrials = 1; // we'll start with 1

    // Generate the settings for each block, and trials within those blocks.
    // This method will be called by the OnSessionBegin event in the UXF Rig
    public void Generate(Session uxfSession)
    {
        // we will just have 1 block (At least for now)
        Block attachmentFigureBlock = uxfSession.CreateBlock(numberOfTrials);

        // make any settings needed to each trial
        // currently there are none.
        //foreach(Trial trial in attachmentFigureBlock.trials)
        //{
        //    trial.settings.SetValue("key", "value");
        //}
    }
}