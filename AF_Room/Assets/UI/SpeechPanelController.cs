using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechPanelController : MonoBehaviour
{
    public void resButton01Pressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    public void resButton02Pressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    public void resButton03Pressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    public void unresButton01Pressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    public void unresButton02Pressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    public void unresButton03Pressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }



    public void chatTextFieldUpdated()
    {
        Debug.Log("Text Updated: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    public void chatSendButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
}
