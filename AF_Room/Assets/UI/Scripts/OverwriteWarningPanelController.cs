using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OverwriteWarningPanelController : MonoBehaviour
{
	public event Action<bool> OnUserSelection;
	public Canvas PopUpUICanvas;

	public void OnClickCancel()
	{
		OnUserSelection?.Invoke(false);
		ClosePanel();
	}
	public void OnClickReplace()
	{
		OnUserSelection?.Invoke(true);
		ClosePanel();
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
