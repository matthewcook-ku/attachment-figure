using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controller for the Animation UI Panel

public class AnimationPanelController : MonoBehaviour
{
    public void smileExpButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Smile);
    }
    public void neutralExpButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Neutral);
    }
    public void concernExpButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Concern);
    }
    public void frownExpButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Frown);
    }
    public void disgustExpButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Disgust);
    }
    public void angerExpButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Anger);
    }
    public void laughExpButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Laugh);
    }
    public void shakeHeadButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.PerformBodyAction(AgentSkin.BodyAction.HeadShake);
    }
    public void nodHeadButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.PerformBodyAction(AgentSkin.BodyAction.HeadNod);
    }
    public void tiltLeftButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.PerformBodyAction(AgentSkin.BodyAction.HeadTiltLeft);
    }
    public void tiltNeutralButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.PerformBodyAction(AgentSkin.BodyAction.HeadTiltNeutral);
    }
    public void tiltRightButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.PerformBodyAction(AgentSkin.BodyAction.HeadTiltRight);
    }
    public void sitShiftButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.PerformBodyAction(AgentSkin.BodyAction.SitShift);
    }
    public void glanceShiftButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.PerformBodyAction(AgentSkin.BodyAction.GlanceShift);
    }
    public void LeanValueSliderChanged(float value)
    {
        AFManager.Instance.agent.activeSkin.LeanDegree = value;
    }
}
