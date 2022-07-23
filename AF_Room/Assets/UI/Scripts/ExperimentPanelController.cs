using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

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
}
