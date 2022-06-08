using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controller for elements related to the study subject.
//
// This is the central place to access and adjust anything to do with the subject. This component should be on a main subject object with all subject related elements as children.
//
// This currently includes:
// - access to proximity trackers
// - VR display elements and camera should be children under this game object


public class SubjectController : MonoBehaviour
{
    // will be collected from children in Start
    public ProxemicsTracker proxemicsTracker { get; set; }
    public ProxemicsAverageTracker proxemicsAverageTracker { get; set; }

    // Where to look in order to look at the subject
    public Transform GazeTarget { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        proxemicsTracker = GetComponentInChildren<ProxemicsTracker>();
        proxemicsAverageTracker = GetComponentInChildren<ProxemicsAverageTracker>();

        GazeTarget = transform.Find("Main Camera");
    }
}
