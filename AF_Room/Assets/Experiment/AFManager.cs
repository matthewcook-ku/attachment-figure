using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AFManager : MonoBehaviour
{
    // Singleton instance
    public static AFManager Instance { get; private set; }

    // connnections to important elements
    public StudyController studyController { get; private set; }

    private void Awake()
    {
        // if there is already an instance and it's not me, delete myself.
        if(Instance != null && Instance != this)
        {
            Destroy(this);

            // This object won't be destoryed until the next Update, so
            // return here to avoid the GetComponent calls below from happening.
            return;  
        }

        Instance = this;

        studyController = GetComponentInChildren<StudyController>();
    }
}
