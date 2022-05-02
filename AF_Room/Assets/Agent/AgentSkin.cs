using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSkin : MonoBehaviour
{
    [Help("A game object parented to the head bone, positioned where a person would look at this person in the face.")]
    public GameObject gazeTarget = null;
    public Animator animator
    {
        get
        {
            return GetComponent<Animator>();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (gazeTarget == null) Debug.Log("AgentSkin missing gaze target!");
    }
}
