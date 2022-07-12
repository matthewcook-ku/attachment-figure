using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Central App Manager Class
//
// This class acts as a central service locator object for the application. The class includes a singleton object that can be easily accessed from scripts burried deeply in the scene hierarchy.
// Feel free to stash references to important app components here so that you can access them without having to dig.
//
// WARNING: the singelton is wired up in Awake, and it's Awake might be called after the Awake of any other elements. So only use the Instance in Start function of other scripts, never Awake
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

    // Event fires when all elements are initialized
    //public static event Action OnInit;

    // connnections to important elements
    public StudyController studyController { get; private set; }
    public AgentController agent { get { return studyController.agent; } }
    public SubjectController subject { get { return studyController.subject; } }
    public InputManager inputManager { get; private set; }

    private void Awake()
    {
        // if there is already an instance and it's not me, delete myself.
        if (Instance != null && Instance != this)
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
        //if (studyController == null) StudyController.OnInit += OnStudyControllerInit;

        inputManager = GetComponent<InputManager>();
        //if (inputManager == null) InputManager.OnInit += OnInputManagerInit;
    }
    /*
    void OnStudyControllerInit()
	{
        //StudyController.OnInit -= OnStudyControllerInit;
        //CheckInitialized();
    }
    void OnInputManagerInit()
    {
        //InputManager.OnInit -= OnInputManagerInit;
        //CheckInitialized();
    }

    bool _initialized = false;
    bool CheckInitialized()
	{
        // if we already initilized, then we are from then on
        if (_initialized) return true;

        // check all the parts to see if they are ready
        if (studyController == null) return false;
        if (agent == null) return false;
        if (subject == null) return false;
        if (inputManager == null) return false;

        // if we make it this far, we must be initiulized for the first time
        _initialized = true;
        OnInit?.Invoke();

        return _initialized;
	}
    */
	private void Start()
	{
        Debug.Log("<color=teal>AFManager: started</color>");
    }
}
