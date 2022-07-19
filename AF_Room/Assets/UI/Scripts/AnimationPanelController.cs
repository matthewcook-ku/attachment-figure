using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// Controller for the Animation UI Panel

public class AnimationPanelController : MonoBehaviour
{
    public Toggle LookingToggle;

    public event Action<int> OnExpressionButtonClick;
    public event Action<int> OnGestureButtonClick;
    public event Action<bool> OnAttentionChange;

    private void Start()
	{
        //Debug.Log("AnimationPanelController Start");
        
        // Read any settings from the system to reflect in the UI
        updateLookingToggle();

        // register for updates
        AgentController.ActiveSkinChanged += updateLookingToggle;
    }
	private void OnDisable()
	{
        AgentController.ActiveSkinChanged -= updateLookingToggle;
    }

	private void updateLookingToggle()
	{
        LookingToggle.SetIsOnWithoutNotify(AFManager.Instance.agent.activeSkin.currentlyLooking);
	}

	public void smileExpButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Smile);
        OnExpressionButtonClick?.Invoke((int)AgentSkin.FaceExpression.Smile);
    }
    public void neutralExpButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Neutral);
        OnExpressionButtonClick?.Invoke((int)AgentSkin.FaceExpression.Neutral);
    }
    public void concernExpButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Concern);
        OnExpressionButtonClick?.Invoke((int)AgentSkin.FaceExpression.Concern);
    }
    public void frownExpButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Frown);
        OnExpressionButtonClick?.Invoke((int)AgentSkin.FaceExpression.Frown);
    }
    public void disgustExpButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Disgust);
        OnExpressionButtonClick?.Invoke((int)AgentSkin.FaceExpression.Disgust);
    }
    public void angerExpButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Anger);
        OnExpressionButtonClick?.Invoke((int)AgentSkin.FaceExpression.Anger);
    }
    public void laughExpButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Laugh);
        OnExpressionButtonClick?.Invoke((int)AgentSkin.FaceExpression.Laugh);
    }
    public void shakeHeadButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.PerformBodyAction(AgentSkin.BodyAction.HeadShake);
        OnGestureButtonClick?.Invoke((int)AgentSkin.BodyAction.HeadShake);
    }
    public void nodHeadButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.PerformBodyAction(AgentSkin.BodyAction.HeadNod);
        OnGestureButtonClick?.Invoke((int)AgentSkin.BodyAction.HeadNod);
    }
    public void attentionToggleChanged(bool value)
	{
        Debug.Log("Toggle Changed: " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + value);
        AFManager.Instance.agent.activeSkin.currentlyLooking = value;
        OnAttentionChange?.Invoke(value);
    }
    public void tiltLeftButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.PerformBodyAction(AgentSkin.BodyAction.HeadTiltLeft);
        OnGestureButtonClick?.Invoke((int)AgentSkin.BodyAction.HeadTiltLeft);
    }
    public void tiltNeutralButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.PerformBodyAction(AgentSkin.BodyAction.HeadTiltNeutral);
        OnGestureButtonClick?.Invoke((int)AgentSkin.BodyAction.HeadTiltNeutral);
    }
    public void tiltRightButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.PerformBodyAction(AgentSkin.BodyAction.HeadTiltRight);
        OnGestureButtonClick?.Invoke((int)AgentSkin.BodyAction.HeadTiltRight);
    }
    public void sitShiftButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.PerformBodyAction(AgentSkin.BodyAction.SitShift);
        OnGestureButtonClick?.Invoke((int)AgentSkin.BodyAction.SitShift);
    }
    public void glanceShiftButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.PerformBodyAction(AgentSkin.BodyAction.GlanceShift);
        OnGestureButtonClick?.Invoke((int)AgentSkin.BodyAction.GlanceShift);
    }
    public void LeanValueSliderChanged(float value)
    {
        AFManager.Instance.agent.activeSkin.LeanDegree = value;
    }
}
