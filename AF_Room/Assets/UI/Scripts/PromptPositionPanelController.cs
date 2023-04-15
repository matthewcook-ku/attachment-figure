using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PromptPositionPanelController : MonoBehaviour
{
	public Canvas PopUpUICanvas;

	public SubjectPrompterController Prompter;
	public SubscriberCamera SubjectPOVRenderTextureCamera;

	private bool prompterVisibleState;	// was the prompter visible when we started.

	// called when panel is opened by SetActive call
	public void OnEnable()
	{
		// save the prompter status when we started
		prompterVisibleState = Prompter.isVisible();

		// make sure the prompter is visible
		Prompter.fadeVisibility(true);
	}
	void OnDisable()
	{
		// remove self from cameras
		if (SubjectPOVRenderTextureCamera != null) SubjectPOVRenderTextureCamera.Unsubscribe(this);

		// turn off prompter
		Prompter.fadeVisibility(prompterVisibleState);
	}

	private void Start()
	{
		// subscribe to the needed cameras, this will turn them on if needed
		if (SubjectPOVRenderTextureCamera != null) SubjectPOVRenderTextureCamera.Subscribe(this);
	}

	public void OpenPanel()
	{
		// open the popup canvas, and then turn on this panel.
		PopUpUICanvas.gameObject.SetActive(true);
		gameObject.SetActive(true);
	}
	public void ClosePanel()
	{
		// make sure to close the panel AND canvas, as canvas will block other conavas's input.
		gameObject.SetActive(false);
		PopUpUICanvas.gameObject.SetActive(false);
	}
}