using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// add the UXF namespace
using UXF;

public class ExperimentGenerator : MonoBehaviour
{
    // generate the blocks and trials for the session.
    // the session is passed as an argument by the event call.
    public void Generate(Session session)
    {
        // single trial for now, maybe separate later
        int numTrials = 2;
        session.CreateBlock(numTrials);
    }

    public void StartNextTrial(Session session)
    {
        session.BeginNextTrial();
    }
}
