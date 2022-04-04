using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

//
// Tracker to record proxemics data for the trial.
// connect to the StudyController object so that it will have access to right game objects and data.
//

public class ProxemicsTracker : Tracker
{
    private GameObject subject;
    private GameObject agent;
    private int HMDFieldOfView = 110;

    public override string MeasurementDescriptor => "proxemics";
    public override IEnumerable<string> CustomHeader => new string[]
        {
            "distance",
            "gaze"
        };

    // initialize variables
    void Awake()
    {
        // find the StudyController attached to this GameObject
        StudyController studyController = GetComponent<StudyController>();

        // local copies of objects.
        subject = studyController.subject;
        agent = studyController.agent;
        HMDFieldOfView = studyController.HMDFieldOfView;
    }

    // return one row of values to be written to file by the UXF tracking system.
    protected override UXFDataRow GetCurrentValues()
    {
        float distance = agentSubjectDistance();
        float gaze = gazeScore();

        var values = new UXFDataRow()
        {
            ("distance", distance),
            ("gaze", gaze)
        };

        return values;
    }

    // calcualte the distance between agent and subject
    float agentSubjectDistance()
    {
        Vector3 agentLocation = agent.transform.position;
        Vector3 subjectLocation = subject.transform.position;

        return Vector3.Distance(agentLocation, subjectLocation);
    }

    // The degree to which the agent falls within the subject's field of view.
    // 1.0 means looking right at the agent.
    // 0.0 means out of the field of view.
    float gazeScore()
    {
        // find the vector from subject to agent
        Vector3 agentLocation = agent.transform.position;
        Vector3 subjectLocation = subject.transform.position;
        Vector3 directionVector = agentLocation - subjectLocation;
        directionVector.Normalize();

        // find the forward sight line vector of the subject
        // assuming this is a camera, in unity this will be the subject's positive Z vector.
        Vector3 subjectForwardVector = subject.transform.forward;

        // find the angle (degrees) between these 2 vectors
        float angleBetween = Vector3.Angle(subjectForwardVector, directionVector);

        // find proportion within field of view.
        if (angleBetween >= HMDFieldOfView)
            return 0.0f;
        else
            return (1.0f - (angleBetween / (float)HMDFieldOfView));
    }
}