using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UXF;

// Controller for the Experiment UI
//
// This is the pannel where info about the current running tiral / block / session should be placed. It's purpose is to let the experimenter monitor things.
//
// Connections:
// ExperimenterUI - ExperimentPanel

public class ExperimentPanelController : MonoBehaviour
{

    public InputField distanceField;
    public InputField gazeField;

    public void endTrialButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        if(Session.instance != null) Session.instance.EndCurrentTrial();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
