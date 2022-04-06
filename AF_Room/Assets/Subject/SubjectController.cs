using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectController : MonoBehaviour
{
    public ProxemicsTracker proxemicsTracker { get; set; }

    public FirstPersonController fps { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        fps = GetComponent<FirstPersonController>();
        if (null == fps)
        {
            Debug.LogError("Missing FPS Controller - make sure some child of the subject game object has a FPS controller attached.");
        }
        // disable the FPS on startup
        fps.enabled = false;

        proxemicsTracker = GetComponentInChildren<ProxemicsTracker>();
        if(null == proxemicsTracker)
        {
            Debug.LogError("Missing ProxemicsTracker - make sure some child of the subject game object has a tracker attached.");
        }
    }
}
