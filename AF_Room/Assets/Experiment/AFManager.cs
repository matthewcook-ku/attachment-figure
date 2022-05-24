using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Central App Manager Class
//
// This class acts as a central service locator object for the application. The class includes a singleton object that can be easily accessed from scripts burried deeply in the scene hierarchy.
// Feel free to stash references to important app components here so that you can access them without having to dig.
//
// For more info on Singletons in Unity, see:
// https://gamedevbeginner.com/singletons-in-unity-the-right-way/#:~:text=Generally%20speaking%2C%20a%20singleton%20in,or%20to%20other%20game%20systems.
//
// Connections:
// studyController - on the App object


public class AFManager : MonoBehaviour
{
    // Singleton instance
    public static AFManager Instance { get; private set; }

    // connnections to important elements
    public StudyController studyController { get; private set; }
    public AgentController agent { get { return studyController.agent; } }
    public SubjectController subject { get { return studyController.subject; } }

    public InputManager InputManager { get; private set; }

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
        // store the instance
        Instance = this;

        // collect references here
        studyController = GetComponent<StudyController>();
        InputManager = GetComponent<InputManager>();
    }
}
