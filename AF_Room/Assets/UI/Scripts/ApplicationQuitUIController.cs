using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class ApplicationQuitUIController : MonoBehaviour
{
    public void endTrialButtonPressed()
    {
        Debug.Log("UI: Application Quit Button Pressed.");

        if (Session.instance != null)
        {
            // this will trigger OnTrialEnd
            Session.instance.EndCurrentTrial();
            // this will trigger PreSessionEnd and then OnSessionEnd
            Session.instance.End();
        }
        else
            Debug.LogWarning("UI: session was null!!");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
