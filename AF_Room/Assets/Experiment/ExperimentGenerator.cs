using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

// UXF Setup Object
//
// This object is responsible for setting up the sessions, blocks, and trials for the UXF framrwork. This is also a place to store any settings into the trial or settings objects.
// see: https://github.com/immersivecognition/unity-experiment-framework/wiki/Session-generation
//
// Connections:
// UXF_Rig Session component


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

        // set the session to cleanup safely if the application quits
        uxfSession.endOnDestroy = true;
        uxfSession.endOnQuit = true;
    }
}