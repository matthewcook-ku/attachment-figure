using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR;

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
	[Tooltip("The camera representing the VR view.")]
	public Camera SubjectViewCamera;
	[Tooltip("A game object marking the default starting psoition of the player's view.")]
	public GameObject SubjectResetPosition;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("<color=teal>SubjectController: started</color>");

        // collect references to proximics items
        proxemicsTracker = GetComponentInChildren<ProxemicsTracker>();
        proxemicsAverageTracker = GetComponentInChildren<ProxemicsAverageTracker>();

        GazeTarget = transform.Find("Main Camera");
    }

    // Reset the player's view. This means moving the XR origin to align it with the subject as our start point
    [ContextMenu("Reset Player Position")]
    public void ResetSubjectPosition()
    {
        Debug.Log("Resetting player position");

        // first rotate the XROrigin to match the camera's position.
        float rotationAngleY = SubjectResetPosition.transform.rotation.eulerAngles.y - SubjectViewCamera.transform.eulerAngles.y;
        XROrigin.transform.Rotate(0.0f, rotationAngleY, 0.0f);

        // now move
        Vector3 dist = SubjectResetPosition.transform.position - SubjectViewCamera.transform.position;
        XROrigin.transform.position += dist;
    }
}
