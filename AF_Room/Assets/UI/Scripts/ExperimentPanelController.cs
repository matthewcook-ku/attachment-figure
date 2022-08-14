using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
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

	public FPSMovementController fps;
	public TMP_Text FPSIndicator;

	private void OnEnable()
	{
		ProxemicsTracker.OnTakeMeasurement += UpdateHeadsetReadout;
		AFManager.Instance.inputManager.InputActions.ExperimenterControls.ToggleFPS.performed += OnFPSTogglePerformed;
	}
	private void OnDisable()
	{
		ProxemicsTracker.OnTakeMeasurement -= UpdateHeadsetReadout;
		AFManager.Instance.inputManager.InputActions.ExperimenterControls.ToggleFPS.performed -= OnFPSTogglePerformed;
	}

	private void UpdateHeadsetReadout(ProxemicsTracker tracker)
	{
		distanceField.SetTextWithoutNotify(tracker.distance.ToString());
		gazeField.SetTextWithoutNotify(tracker.gaze.ToString());
	}

	private void OnFPSTogglePerformed(InputAction.CallbackContext obj)
	{
		bool fpsToggledState = !fps.enabled;
		Debug.Log("FPS Controls: " + (fpsToggledState ? "on" : "off"));
		FPSIndicator.enabled = fpsToggledState;

		// turn on/off the FPS controls
		fps.enabled = fpsToggledState;
	}

	void Start()
	{
		// disable the FPS on startup
		fps.enabled = false;
		FPSIndicator.enabled = false;
	}

	public void OnEndTrialButtonPressed()
	{
		DialogueBoxController.Popup quitPopup = new DialogueBoxController.Popup()
		{
			message = "You are about to end the experiment. Are you sure you want to quit?",
			messageType = DialogueBoxController.MessageType.Warning,
			onOK = () => { EndExperimentAction(); }
		};
		DialogueBoxController.Instance().DisplayPopup(quitPopup);
	}

	public void EndExperimentAction()
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
