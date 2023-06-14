using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

// Controller to access the Subject's UI

public class SubjectHUDController : MonoBehaviour
{
	bool showHUD;
	public GameObject HeadsetReadoutPanel;
	public GameObject Reticle;

	/*
	private void OnEnable()
	{
		AFManager.Instance.inputManager.InputActions.ExperimenterControls.ToggleSubjectUI.performed += OnToggleInputAction;
	}
	private void OnDisable()
	{
		AFManager.Instance.inputManager.InputActions.ExperimenterControls.ToggleSubjectUI.performed -= OnToggleInputAction;
	}
	void OnToggleInputAction(InputAction.CallbackContext obj)
	{
		ToggleSubjectUI();
	}*/

	
	public void ToggleSubjectHUD()
	{
		showHUD = !showHUD;
		
		HeadsetReadoutPanel.SetActive(showHUD);
		Reticle.SetActive(showHUD);
	}
}
