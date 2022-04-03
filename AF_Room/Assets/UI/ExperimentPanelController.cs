using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class ExperimentPanelController : MonoBehaviour
{
    public void endTrialButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        Session.instance.EndCurrentTrial();
    }
}
