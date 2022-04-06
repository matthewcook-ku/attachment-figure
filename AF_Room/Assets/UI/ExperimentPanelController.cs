using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UXF;

public class ExperimentPanelController : MonoBehaviour
{
    public InputField distanceField;
    public InputField gazeField;

    public void endTrialButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        if(Session.instance != null) Session.instance.EndCurrentTrial();
    }
}
