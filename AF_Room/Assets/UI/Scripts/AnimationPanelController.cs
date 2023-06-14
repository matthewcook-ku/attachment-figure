using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

// Controller for the Animation UI Panel

public class AnimationPanelController : MonoBehaviour
{
    public Toggle LookingToggle;
    public Toggle RandomGlanceToggle;
    public Slider AffinitySlider;
    public TMP_Text AffinitySliderLabel;

    public EventToggleGroup TiltToggleGroup;
    public EventToggleGroup ExpressionToggleGroup;

    // these are used for logging actions to the log file
    public static event Action<int> OnExpressionButtonClick;
    public static event Action<int> OnGestureButtonClick;
    public static event Action<bool> OnAttentionChange;
    public static event Action<bool> OnGlancingChange;
	//public static event Action<float> OnAffinityPercentChanged;    //


	private void Start()
	{
        //Debug.Log("AnimationPanelController Start");

        // Read any settings from the system to reflect in the UI
        StartCoroutine(Initialize());

        // register for updates
        AgentController.ActiveSkinChanged += initializeUIDisplays;
    }
	private void OnDisable()
	{
        AgentController.ActiveSkinChanged -= initializeUIDisplays;
    }

    IEnumerator Initialize()
	{
        //Debug.Log("*** initializing AnimationPanelController");
        
        // wait for the agent skin to be set up
        yield return new WaitUntil(() => AFManager.Instance.agent.activeSkin != null);

        //Debug.Log("*** elements ready for setup");

        initializeUIDisplays();
	}

    // Attention Cues:
	private void initializeUIDisplays()
	{
        AgentSkin activeskin = AFManager.Instance.agent.activeSkin;
        LookingToggle.SetIsOnWithoutNotify(activeskin.currentlyLooking);

        RandomGlanceToggle.SetIsOnWithoutNotify(activeskin.randomGlances);
        RandomGlanceToggle.interactable = LookingToggle.isOn;

        AffinitySlider.SetValueWithoutNotify(activeskin.affinityPercent * 100.0f);
        AffinitySliderLabel.text = AffinitySlider.value.ToString() + " %";
        AffinitySlider.interactable = RandomGlanceToggle.interactable;

        Toggle defaultTiltToggle = TiltToggleGroup.getToggles().FirstOrDefault(t => ToggleForAction(t, AgentSkin.defaultHeadTilt));
        defaultTiltToggle.isOn = true;
        //Debug.Log("Setting Default Tilt Toggle: " + defaultTiltToggle);

        Toggle defaultExpressionToggle = ExpressionToggleGroup.getToggles().FirstOrDefault(t => ToggleForExpression(t, AgentSkin.defaultExpression));
        defaultExpressionToggle.isOn = true;
        //Debug.Log("Setting Default Exp Toggle: " + defaultExpressionToggle);
    }

    private static bool ToggleForAction(Toggle item, AgentSkin.BodyAction action)
	{
        AgentAnimationPanelSelector selector = item.GetComponent<AgentAnimationPanelSelector>();
        return (selector.BodyAction == action);
	}
    private static bool ToggleForExpression(Toggle item, AgentSkin.FaceExpression exp)
    {
        AgentAnimationPanelSelector selector = item.GetComponent<AgentAnimationPanelSelector>();
        return (selector.FaceExpression == exp);
    }

    public void lookToggleChanged(bool value)
    {
        Debug.Log("Toggle Changed: " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + value);
        AFManager.Instance.agent.activeSkin.currentlyLooking = value;
        OnAttentionChange?.Invoke(value);

        RandomGlanceToggle.interactable = value;
    }
    public void randomGlancingToggleChanged(bool value)
    {
        Debug.Log("Toggle Changed: " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + value);
        AFManager.Instance.agent.activeSkin.randomGlances = value;
        OnGlancingChange?.Invoke(value);

        AffinitySlider.interactable = value;
    }
    public void affinitySliderValueChanged(float value)
    {
        //Debug.Log("Slider Changed: " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + value);
        AFManager.Instance.agent.activeSkin.affinityPercent = value * 0.01f;
        //OnAffinityPercentChanged?.Invoke(value);

        AffinitySliderLabel.text = value.ToString() + " %";
    }

    // Body Movement:
    //public void tiltLeftButtonPressed()
    //{
    //    Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    //    AFManager.Instance.agent.activeSkin.PerformBodyAction(AgentSkin.BodyAction.HeadTiltLeft);
    //    OnGestureButtonClick?.Invoke((int)AgentSkin.BodyAction.HeadTiltLeft);
    //}
    //public void tiltNeutralButtonPressed()
    //{
    //    Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    //    AFManager.Instance.agent.activeSkin.PerformBodyAction(AgentSkin.BodyAction.HeadTiltNeutral);
    //    OnGestureButtonClick?.Invoke((int)AgentSkin.BodyAction.HeadTiltNeutral);
    //}
    //public void tiltRightButtonPressed()
    //{
    //    Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    //    AFManager.Instance.agent.activeSkin.PerformBodyAction(AgentSkin.BodyAction.HeadTiltRight);
    //    OnGestureButtonClick?.Invoke((int)AgentSkin.BodyAction.HeadTiltRight);
    //}

    public void tiltLeftToggleActivated(bool value)
    {
        if (value == false) return;
        Debug.Log("Toggle Activated: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.PerformBodyAction(AgentSkin.BodyAction.HeadTiltLeft);
        OnGestureButtonClick?.Invoke((int)AgentSkin.BodyAction.HeadTiltLeft);
    }
    public void tiltCenterToggleActivated(bool value)
    {
        if (value == false) return;
        Debug.Log("Toggle Activated: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.PerformBodyAction(AgentSkin.BodyAction.HeadTiltNeutral);
        OnGestureButtonClick?.Invoke((int)AgentSkin.BodyAction.HeadTiltNeutral);
    }
    public void tiltRightToggleActivated(bool value)
    {
        if (value == false) return;
        Debug.Log("Toggle Activated: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.PerformBodyAction(AgentSkin.BodyAction.HeadTiltRight);
        OnGestureButtonClick?.Invoke((int)AgentSkin.BodyAction.HeadTiltRight);
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

    // Expressions:

    public void smileExpToggleActivated(bool value)
    {
        if (value == false) return;
        Debug.Log("Toggle Activated: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Smile);
        OnExpressionButtonClick?.Invoke((int)AgentSkin.FaceExpression.Smile);
    }
    public void neutralExpToggleActivated(bool value)
    {
        if (value == false) return;
        Debug.Log("Toggle Activated: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Neutral);
        OnExpressionButtonClick?.Invoke((int)AgentSkin.FaceExpression.Neutral);
    }
    public void concernExpToggleActivated(bool value)
    {
        if (value == false) return;
        Debug.Log("Toggle Activated: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Concern);
        OnExpressionButtonClick?.Invoke((int)AgentSkin.FaceExpression.Concern);
    }
    public void frownExpToggleActivated(bool value)
    {
        if (value == false) return;
        Debug.Log("Toggle Activated: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Frown);
        OnExpressionButtonClick?.Invoke((int)AgentSkin.FaceExpression.Frown);
    }
    public void disgustExpToggleActivated(bool value)
    {
        if (value == false) return;
        Debug.Log("Toggle Activated: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Disgust);
        OnExpressionButtonClick?.Invoke((int)AgentSkin.FaceExpression.Disgust);
    }
    public void angerExpToggleActivated(bool value)
    {
        if (value == false) return;
        Debug.Log("Toggle Activated: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Anger);
        OnExpressionButtonClick?.Invoke((int)AgentSkin.FaceExpression.Anger);
    }
    public void laughExpToggleActivated(bool value)
    {
        if (value == false) return;
        Debug.Log("Toggle Activated: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Laugh);
        OnExpressionButtonClick?.Invoke((int)AgentSkin.FaceExpression.Laugh);
    }

    //public void smileExpButtonPressed()
    //{
    //    Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    //    AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Smile);
    //    OnExpressionButtonClick?.Invoke((int)AgentSkin.FaceExpression.Smile);
    //}
    //public void neutralExpButtonPressed()
    //{
    //    Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    //    AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Neutral);
    //    OnExpressionButtonClick?.Invoke((int)AgentSkin.FaceExpression.Neutral);
    //}
    //public void concernExpButtonPressed()
    //{
    //    Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    //    AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Concern);
    //    OnExpressionButtonClick?.Invoke((int)AgentSkin.FaceExpression.Concern);
    //}
    //public void frownExpButtonPressed()
    //{
    //    Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    //    AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Frown);
    //    OnExpressionButtonClick?.Invoke((int)AgentSkin.FaceExpression.Frown);
    //}
    //public void disgustExpButtonPressed()
    //{
    //    Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    //    AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Disgust);
    //    OnExpressionButtonClick?.Invoke((int)AgentSkin.FaceExpression.Disgust);
    //}
    //public void angerExpButtonPressed()
    //{
    //    Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    //    AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Anger);
    //    OnExpressionButtonClick?.Invoke((int)AgentSkin.FaceExpression.Anger);
    //}
    //public void laughExpButtonPressed()
    //{
    //    Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    //    AFManager.Instance.agent.activeSkin.MakeFace(AgentSkin.FaceExpression.Laugh);
    //    OnExpressionButtonClick?.Invoke((int)AgentSkin.FaceExpression.Laugh);
    //}

}
