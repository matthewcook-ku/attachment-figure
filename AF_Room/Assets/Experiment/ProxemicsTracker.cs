using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

//
// Tracker to record proxemics data for the trial.
// Attach this tracker to an object that represents the location and direction of the player's view. Probably a camera.
//

public class ProxemicsTracker : Tracker
{
    private AgentController agent = null;
    private int HMDFieldOfView = 110;

    public override string MeasurementDescriptor => "proxemics";
    public override IEnumerable<string> CustomHeader => new string[]
        {
            "distance",
            "gaze"
        };

    // the most recent calculations for these properties. 
    public float distance { get; private set; }
    public float gaze { get; private set; }

    // initialize variables
    void Start()
    {
        // collect a local reference to the agent from the AFManager
        agent = AFManager.Instance.studyController.agent;
        if (null == agent) Debug.LogError("agent null in ProxemicsTracker!");
        HMDFieldOfView = AFManager.Instance.studyController.HMDFieldOfView;
    }

    // return one row of values to be written to file by the UXF tracking system.
    protected override UXFDataRow GetCurrentValues()
    {
        distance = agentSubjectDistance();
        gaze = gazeScore();

        //Debug.Log("Writing data row...\n" +
         //   "distance: " + distance + " " + "gaze: " + gaze
          //  );

        var values = new UXFDataRow()
        {
            ("distance", distance),
            ("gaze", gaze)
        };

        return values;
    }

    // calcualte the distance between agent and subject
    public float agentSubjectDistance()
    {
        Vector3 agentLocation = agent.gazeTarget.transform.position;
        Vector3 subjectLocation = transform.position;

        return Vector3.Distance(agentLocation, subjectLocation);
    }

    // The degree to which the agent falls within the subject's field of view.
    // 1.0 means looking right at the agent.
    // 0.0 means out of the field of view.
    public float gazeScore()
    {
        // find the vector from subject to agent
        Vector3 agentLocation = agent.gazeTarget.transform.position;
        Vector3 subjectLocation = transform.position;
        Vector3 directionVector = agentLocation - subjectLocation;
        directionVector.Normalize();

        Debug.DrawLine(agentLocation, subjectLocation, Color.red, 2.0f, false);

        // find the forward sight line vector of the subject
        // assuming this is a camera, in unity this will be the subject's positive Z vector.
        Vector3 subjectForwardVector = transform.forward;

        Debug.DrawRay(subjectLocation, subjectForwardVector * 10.0f, Color.blue, 2.0f, false);

        // find the angle (degrees) between these 2 vectors
        float angleBetween = Vector3.Angle(subjectForwardVector, directionVector);

        // find proportion within field of view.
        float halfFOV = (float)HMDFieldOfView / 2.0f;
        if (angleBetween >= (halfFOV))
            return 0.0f;
        else
            return (1.0f - (angleBetween / halfFOV));
    }
}