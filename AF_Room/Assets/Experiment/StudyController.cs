using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

// UXF Session Driver
//
// This script drives the major events of the study session. This is the place to sequence out any events fro the study.
// This is also the central place to put settings for this session. Objects implementing the session should come here to collect those settings. 
// This will probably need to be broken up into different sessions for the different studies.
//
// Elements
// - UXF event handlers
// - manual tracking data collection
// - updating UI with live info
//
// Connections:
// Subject - subject controller, and down to proxemics trackers
// Agent - agent controller
// ExperimenterUI
// SubjectUI
//
// UXF_Rig - connect event handlers to Events tab

public class StudyController : MonoBehaviour
{
    [Tooltip("Object representing the subject.")]
    public SubjectController subject;

    [Tooltip("Object representing the agent.")]
    public AgentController agent;
    [Tooltip("Layer Mask for proxemics gaze target ray casting.")]
    public LayerMask gazeTargetLayerMask;

    [Tooltip("Refs to the UI canvas objects.")]
    public ExperimenterUIController experimenterUI;
    public SubjectUIController subjectUI;
    // public UIStartController startUI;

    [Tooltip("The maximum field of view in degrees of the head mounted display. Some common vlues include:\n- Vive Pro Eye: 110\n- Oculus Quest 2: 89")]
    public int HMDFieldOfView = 110;

    [Tooltip("Frequency of tracking data collection in sec. 0.1 is 1/10 second.")]
    public float trackingInterval = 0.3f;
    [Tooltip("Frequency of averaged data collection in sec. 60 is 1 minute.")]
    public float averageInterval = 60.0f;

    // This method should be called by the OnSessionBegin event in the UXF rig.
    public void SessionBegin(Session session)
	{
        Debug.Log("StudyController: Starting Session...");
    }

    // This method should be called by the OnTrialBegin event in the UXF rig.
    public void TrialStart(Trial trial)
    {
        Debug.Log("StudyController: Starting Trial...");
        
        // collect any needed settings from the trial object here.

        // turn on the FPS controller
        //Debug.Log("Turning on FPS controller.");
        //subject.fps.enabled = true;

        // begin start UI
        /*
        Debug.Log("Load start UI");
        startUI.gameObject.SetActive(true);
        */

        // start experimenter UI
        Debug.Log("Load experimenter UI.");
        experimenterUI.gameObject.SetActive(true);

        // start subject UI
        //Debug.Log("Load subject UI.");
        //subjectUI.gameObject.SetActive(true);

        // start the proxemics trackers
        Debug.Log("Starting proxemics trackers.");
        StartCoroutine(ProxemicsTrackingManualRecord());
        StartCoroutine(ProxemicsTrackingManualRecordAverage());
    }

    // This coroutine method will set up recording and then continue every trackingInterval seconds to manually signal the porixemics tracker to record a row of data.
    IEnumerator ProxemicsTrackingManualRecord()
    {
        while(true)
        {
            if (subject.proxemicsTracker.Recording)
            {
                //Debug.Log("Recording data row...");
                subject.proxemicsTracker.RecordRow();
            }

            //float distance = subject.proxemicsTracker.AgentSubjectDistance();
            //float gaze = subject.proxemicsTracker.GazeScore();

            // display the new data in the UI
            //Debug.Log("Updating UI with tracking data.\n" +
            //    "distance: " + distance + "\n" +
            //    "gaze: " + gaze
            //    );

            //if (experimenterUI == null) Debug.Log("experimenterUI null in manual record.");
            //if (experimenterUI.experimentPanelController == null) Debug.Log("experimentPanelController null in manual record.");
            //if (experimenterUI.experimentPanelController.distanceField == null) Debug.Log("distanceField null in manual record.");

            //experimenterUI.experimentPanelController.distanceField.SetTextWithoutNotify(distance.ToString());
            //experimenterUI.experimentPanelController.gazeField.SetTextWithoutNotify(gaze.ToString());

            //subjectUI.headsetReadoutController.distanceField.SetTextWithoutNotify(distance.ToString());
            //subjectUI.headsetReadoutController.gazeField.SetTextWithoutNotify(gaze.ToString());

            // pause for the tracking interval
            yield return new WaitForSeconds(trackingInterval);
        }
    }

    // Coroutine to record average data from the proxemics tracker.
    IEnumerator ProxemicsTrackingManualRecordAverage()
    {
        while (true)
        {
            if (subject.proxemicsTracker.Recording && subject.proxemicsAverageTracker.Recording)
            {
                //Debug.Log("Recording data average row...");
                subject.proxemicsAverageTracker.RecordRow();
            }

            // pause for the tracking interval
            yield return new WaitForSeconds(averageInterval);
        }
    }


    // This method should be called by the OnTrialEnd event in the UXF rig.
    public void TrialEnd(Trial trial)
    {
        Debug.Log("Ending Trial...");



        // write the trial tracking averages to the results
        //Debug.Log("Session Average Distance: " + subject.proxemicsTracker.globalAverageDistance);
        //Debug.Log("Session Average Gaze: " + subject.proxemicsTracker.globalAverageGaze);

        //trial.result["session average distance"] = subject.proxemicsTracker.globalAverageDistance;
        //trial.result["session average gaze score"] = subject.proxemicsTracker.globalAverageGaze;

        // write out the stats for this trial
        subject.proxemicsTracker.closeCurrentTrial();

        // if this is the last trial, save the global data as well.
        // if we wait to do this until PreSessionEnd the results file will already have been writen.
        if(trial == Session.instance.LastTrial)
            subject.proxemicsTracker.closeSession();
    }
    // This method should be called by the PreSessionEnd event in the UXF rig.
    public void PreSessionEnd(Session session)
    {
        Debug.Log("Ending Session...");

        // stop the tracking
        StopAllCoroutines();
    }
    // This method should be called by the OnSessionEnd event in the UXF rig.
    public void SessionEnd(Session session)
    {
        Debug.Log("Session Ended ... Safe to Quit");
    }
}
