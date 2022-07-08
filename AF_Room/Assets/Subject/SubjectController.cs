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

    // player start position info
    [Tooltip("Object representing the height of player head off the ground.")]
    public GameObject CameraOffset;
    [Tooltip("This is the object locomotion movement is applied to.")]
    public GameObject XROrigin;
    public float SubjectHeight
    {   
        get
        {
            return CameraOffset.transform.localPosition.y;
        }
        set
        {
            CameraOffset.transform.localPosition = new Vector3(0.0f, value, 0.0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // collect references to proximics items
        proxemicsTracker = GetComponentInChildren<ProxemicsTracker>();
        proxemicsAverageTracker = GetComponentInChildren<ProxemicsAverageTracker>();

        GazeTarget = transform.Find("Main Camera");
    }

    // return the subject's XR origin to the start point, which is local origin of this GameObject
    public void ResetSubjectPosition()
    {
        Debug.Log("Resetting player position to [0,0,0].");
        XROrigin.transform.localPosition = Vector3.zero;
    }
}
