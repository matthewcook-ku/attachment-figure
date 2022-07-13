using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

// Controller to access the Subject's UI

public class SubjectUIController : MonoBehaviour
{
    public HeadsetReadoutController headsetReadoutController;

	/*
	private void OnEnable()
	{
		AFManager.Instance.inputManager.InputActions.ExperimenterControls.ToggleSubjectUI.performed += OnToggleInputAction;
	}
	private void OnDisable()
	{
		AFManager.Instance.inputManager.InputActions.ExperimenterControls.ToggleSubjectUI.performed -= OnToggleInputAction;
	}
	*/

	void OnToggleInputAction(InputAction.CallbackContext obj)
	{
		ToggleSubjectUI();
	}
	public void ToggleSubjectUI()
	{
		Canvas uiCanvas = gameObject.GetComponent<Canvas>();
		uiCanvas.enabled = !(uiCanvas.enabled);
	}
}
