using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

// This script drives the major events of the study session.

public class StudyController : MonoBehaviour
{
    [Help("Object representing the subject.")]
    public SubjectController subject;

    [Help("Object representing the agent.")]
    public AgentController agent;

    [Help("Refs to the UI canvas objects.")]
    public ExperimenterUIController experimenterUI;
    public SubjectUIController subjectUI;

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

        // turn on the FPS controller
        //Debug.Log("Turning on FPS controller.");
        //subject.fps.enabled = true;

        // start experimenter UI
        Debug.Log("Load experimenter UI.");
        experimenterUI.gameObject.SetActive(true);

        // start subject UI
        Debug.Log("Load subject UI.");
        subjectUI.gameObject.SetActive(true);

        // start the proxemics tracker
        Debug.Log("Starting proxemics tracker.");
        StartCoroutine(ManualRecord());
    }

    // set up the agent paramters for this session. 
    void initializeAgent()
    {
        Debug.Log("TODO: Set selected agent model here...");
        Debug.Log("TODO: Set selected agent voice here...");
    }

    // This coroutine method will set up recording and then continue every trackingInterval seconds to manually signal the porixemics tracker to record a row of data.
    IEnumerator ManualRecord()
    {
        while(true)
        {
            //if (subject == null) Debug.Log("subject null in manual record.");
            //if (subject.proxemicsTracker == null) Debug.Log("tracker null in manual record.");

            if (subject.proxemicsTracker.Recording)
            {
                //Debug.Log("Recording data row...");
                subject.proxemicsTracker.RecordRow();
            }

            float distance = subject.proxemicsTracker.agentSubjectDistance();
            float gaze = subject.proxemicsTracker.gazeScore();

            // display the new data in the UI
            //Debug.Log("Updating UI with tracking data.\n" +
            //    "distance: " + distance + "\n" +
            //    "gaze: " + gaze
            //    );

            //if (experimenterUI == null) Debug.Log("experimenterUI null in manual record.");
            //if (experimenterUI.experimentPanelController == null) Debug.Log("experimentPanelController null in manual record.");
            //if (experimenterUI.experimentPanelController.distanceField == null) Debug.Log("distanceField null in manual record.");

            experimenterUI.experimentPanelController.distanceField.SetTextWithoutNotify(distance.ToString());
            experimenterUI.experimentPanelController.gazeField.SetTextWithoutNotify(gaze.ToString());

            subjectUI.headsetReadoutController.distanceField.SetTextWithoutNotify(distance.ToString());
            subjectUI.headsetReadoutController.gazeField.SetTextWithoutNotify(gaze.ToString());

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
