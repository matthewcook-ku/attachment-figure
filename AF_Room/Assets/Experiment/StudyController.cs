using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

// This script drives the major events of the study session.

public class StudyController : MonoBehaviour
{
    [Help("Object representing the view of the subject, probably a camera. This object should have a ProxemicsTracker attached.")]
    public GameObject subject;
    [Help("Object representing the agent.")]
    public GameObject agent;

    [Help("Refs to the UI canvas objects.")]
    public GameObject experimenterUI;
    public GameObject subjectUI;

    [Help("The maximum field of view in degrees of the head mounted display. Some common vlues include:\n- Vive Pro Eye: 110\n- Oculus Quest 2: 89")]
    public int HMDFieldOfView = 110;

    [Help("Set the frequency of tracking data collection. 0.1 is 1/10 second.")]
    public float trackingInterval = 0.3f;

    // This method should be called by the OnTrialBegin event in the UXF rig.
    public void TrialStart(Trial trial)
    {
        // collect any needed settings from the trial object here.

        // set up the agent
        initializeAgent();

        // start experimenter UI
        experimenterUI.SetActive(true);

        // start subject UI
        //subjectUI.SetActive(true);

        // start the proxemics tracker
        StartCoroutine(ManualRecord());
    }

    // set up the agent paramters for this session. 
    void initializeAgent()
    {
        Debug.Log("TODO: Set selected agent model here...");
        Debug.Log("TODO: Set selected agent voice here...");
    }

    // This method will be called every trackingInterval seconds to manually signal the orixemics tracker to record a row of data.
    IEnumerator ManualRecord()
    {
        // get a reference to the tracker, its attached to the subject.
        ProxemicsTracker proxemicsTracker = subject.GetComponent<ProxemicsTracker>();
        if(proxemicsTracker == null)
        {
            Debug.LogError("ProxemicsTracker not found on Subject!!");
        }

        while(true)
        {
            if(proxemicsTracker.Recording)
            {
                proxemicsTracker.RecordRow();
            }

            // pause for the tracking interval
            yield return new WaitForSeconds(trackingInterval);
        }
    }

    // This method should be called by the OnTrialBegin event in the UXF rig.
    public void TrialEnd()
    {
        // stop the tracking
        StopAllCoroutines();
    }
}
