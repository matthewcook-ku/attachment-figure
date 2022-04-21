using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectController : MonoBehaviour
{
    // will be collected from children in Start
    public ProxemicsTracker proxemicsTracker { get; set; }
    public ProxemicsAverageTracker proxemicsAverageTracker { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        proxemicsTracker = GetComponentInChildren<ProxemicsTracker>();
        if(null == proxemicsTracker)
        {
            Debug.LogError("Missing ProxemicsTracker - make sure some child of the subject game object has a tracker attached.");
        }
        proxemicsAverageTracker = GetComponentInChildren<ProxemicsAverageTracker>();
        if (null == proxemicsAverageTracker)
        {
            Debug.LogError("Missing ProxemicsAverageTracker - make sure some child of the subject game object has a tracker attached.");
        }
    }
}
