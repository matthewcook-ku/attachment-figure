using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using UnityEngine.InputSystem;
using System;

// Controls a floating panel with text for the user to read

public class SubjectPrompterController : MonoBehaviour
{
	// UI Elements
    public TMP_Text PrompterPanelTitle;
    public TMP_Text PrompterText;

	// Fade animation elements
	private Tween fadeTween;
	public CanvasGroup canvasFadeGroup;
	public float fadeDuration;  // fade duration in seconds
	public const float defaultFadeDuration = 1.0f;
	private bool visible = true;
	private bool inputControlsActive = true;

	public GameObject defaultPosition;
	public float translateSpeed = 1.0f;
	public float rotateSpeed = 1.0f;
	public InputManager inputManager;

	private void OnEnable()
	{
		// listen for prompter event from the UI
		SpeechPanelController.OnShowPromptButtonClick += OnPrompterEvent;
		StudyController.OnUXFTrialBegin += OnTrialBegin;
		StudyController.OnUXFTrialEnd += OnTrialEnd;
	}

	private void OnDisable()
	{
		// un-register for events
		SpeechPanelController.OnShowPromptButtonClick -= OnPrompterEvent;
		StudyController.OnUXFTrialBegin -= OnTrialBegin;
		StudyController.OnUXFTrialEnd -= OnTrialEnd;
	}

	private void Start()
	{
		// store this as the default position on startup
		AFUtilities.alignTransforms(this.transform, defaultPosition.transform);

		// turn the prompter off on startup
		setVisibility(false);
		setInputControlsActive(false);
		

		// test fading
		//StartCoroutine(TestFade());
	}

	private void OnTrialBegin()
	{
		// do nothing
	}
	private void OnTrialEnd()
	{
		fadeVisibility(false, fadeDuration, () => { setPrompterText(""); }); // clear the prompter text after fade is complete.
	}

	private void OnPrompterEvent(string message)
	{
		// update the currently displayed message
		setPrompterText(message);

		// make sure the prompter is showing
		if(!visible)
		{
			fadeVisibility(true, fadeDuration);
		}
	}

	public void setPrompterTitle(string title)
	{
		PrompterPanelTitle.text = title;
	}
	public void setPrompterText(string text)
	{
		PrompterText.text = text;
	}


	public void resetPosition()
	{
		AFUtilities.alignTransforms(defaultPosition.transform, this.transform);
	}
	public void trasnformPosition(Vector3 diff)
	{
		this.transform.position += (diff);	
	}
	public void transformRotation(Vector3 rot)
	{
		this.transform.Rotate(rot);
	}


	public bool isVisible()
	{
		return visible;
	}

	public void setVisibility(bool active)
	{
		if (visible == active) return;

		canvasFadeGroup.interactable = active;
		canvasFadeGroup.blocksRaycasts = active;
		canvasFadeGroup.alpha = (active) ? 1f : 0f;
		visible = active;
	}

	public void setInputControlsActive(bool active)
	{
		if (active == inputControlsActive) return;
		
		// enable or disable the input controls
		InputControls.PrompterControlsActions actions = inputManager.InputActions.PrompterControls;

		if (active)
		{
			// register for input events
			actions.Position.performed += OnPositionPerformed;
			actions.Rotation.started += OnRotationPerformed;    // button down
			actions.Rotation.canceled += OnRotationPerformed;   // button up
			InputManager.EnableActionMap(actions, true);
		}
		else
		{
			// remove input events
			actions.Position.performed -= OnPositionPerformed;
			actions.Rotation.performed -= OnRotationPerformed;
			// deactivate controls
			InputManager.EnableActionMap(actions, false);
		}
		inputControlsActive = active;
	}

	public void fadeVisibility(bool active, float duration = defaultFadeDuration, Action callback = null)
	{
		if(fadeTween != null)   // we are currently fadinging, so we need to stop that animation
		{
			fadeTween.Kill(false);	// don't run any callbacks after kill
		}
		
		// start a new fade
		fadeTween = canvasFadeGroup.DOFade((active) ? 1f : 0f, duration);
		
		// set visible when done
		fadeTween.onComplete += () => { setVisibility(active); };
		// call any additional callbacks
		fadeTween.onComplete += () => { callback?.Invoke(); };
	}

	// fade the panel in and out forever
	private IEnumerator TestFade()
	{
		while(true)
		{
			yield return new WaitForSeconds(2f);
			Debug.Log("fading in");
			fadeVisibility(true, fadeDuration);
			yield return new WaitForSeconds(3f);
			Debug.Log("fading out");
			fadeVisibility(false, fadeDuration);
		}
	}


	// Input System values from controls
	Vector3 positionDelta;
	bool rotationOn;

	// input event functions
	void OnPositionPerformed(InputAction.CallbackContext context)
	{
		positionDelta = context.ReadValue<Vector3>();

		if(rotationOn)
		{
			this.transform.Rotate(rotateSpeed * positionDelta);
		}
		else
		{
			// reverse the x direction for this POV
			positionDelta.x *= -1;
			this.transform.position += (translateSpeed * positionDelta);
		}
	}
	void OnRotationPerformed(InputAction.CallbackContext context)
	{
		rotationOn = context.ReadValueAsButton();
		//Debug.Log("Rotation: " + (rotationOn ? "ON" : "OFF"));
	}
}
