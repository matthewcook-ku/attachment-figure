using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class ApplicationQuitUIController : MonoBehaviour
{
    public void endTrialButtonPressed()
    {
        Debug.Log("UI: Application Quit Button Pressed.");

        Debug.Log("UI: Ending current trial.");
        if (Session.instance != null) Session.instance.EndCurrentTrial();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
