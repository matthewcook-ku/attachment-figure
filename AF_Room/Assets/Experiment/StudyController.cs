using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

// This script drives the major events of the study session.

public class StudyController : MonoBehaviour
{
    public GameObject subject;
    public GameObject agent;

    public GameObject experimenterUI;
    public GameObject subjectUI;

    [Help("Set the frequency of tracking data collection. 0.1 is 1/10 second.")]
    public float trackingInterval = 0.3f;




    // Start the events of the study. This will be called by the UXF Rig at the OnTrialBegin event. Think of this like a normal unity Start function. 
    public void StartSession(Trial trial)
    {
        // collect any needed settings from the trial object here.

        // set up the agent
        initializeAgent();

        // start experimenter UI
        experimenterUI.SetActive(true);

        // start subject UI
        //subjectUI.SetActive(true);

        // start tracking
        StartCoroutine(recordTrackingData());


        // when all trial actions are done, end the trial
        // this will trigger any actions in the UXF rig OnTrialEnd event.
        //trial.End();
    }

    IEnumerator recordTrackingData()
    {
        // record distance and gaze data
        Debug.Log("TODO: Record Tracking Data here...");

        // pause for the tracking interval
        yield return new WaitForSeconds(trackingInterval);
    }    

    // Update is called once per frame
    void Update()
    {
        
    }

    // set up the agent paramters for this session. 
    void initializeAgent()
    {
        Debug.Log("TODO: Set selected agent model here...");
        Debug.Log("TODO: Set selected agent voice here...");
    }
}
