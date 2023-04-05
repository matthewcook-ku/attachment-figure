using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

// Controls a floating panel with text for the user to read

public class SubjectPrompterController : MonoBehaviour
{
	// UI Elements
    public TMP_Text PrompterPanelTitle;
    public TMP_Text PrompterText;
	private const string PrompterBlankingValue = "^__^";

	// Fade animation elements
	private Tween fadeTween;
	public CanvasGroup canvasFadeGroup;
	public float fadeDuration;	// fade duration in seconds
	public bool isVisible
	{
		get
		{
			return (canvasFadeGroup.alpha > 0.0f);
		}
	}

	private void OnEnable()
	{
		// listen for prompter event from the UI
		SpeechPanelController.OnShowPromptButtonClick += OnPrompterEvent;
		//SpeechPanelController.OnSpeakPromptButtonClick += OnNonPrompterEvent;
		StudyController.OnUXFTrialBegin += OnTrialBegin;
		StudyController.OnUXFTrialEnd += OnTrialEnd;
	}

	private void OnDisable()
	{
		// un-register for events
		SpeechPanelController.OnShowPromptButtonClick -= OnPrompterEvent;
		//SpeechPanelController.OnSpeakPromptButtonClick -= OnNonPrompterEvent;
		StudyController.OnUXFTrialBegin -= OnTrialBegin;
		StudyController.OnUXFTrialEnd -= OnTrialEnd;
	}

	private void Start()
	{
		// turn the prompter off on startup
		togglePrompter(false);

		// test fading
		//StartCoroutine(TestFade());
	}

	private void OnTrialBegin()
	{
		// do nothing
	}
	private void OnTrialEnd()
	{
		fade(false, fadeDuration);
		setPrompterText(PrompterBlankingValue);
	}

	private void OnPrompterEvent(string message)
	{
		// update the currently displayed message
		setPrompterText(message);

		// make sure the prompter is showing
		if(!isVisible)
		{
			fade(true, fadeDuration);
		}
	}
	private void OnNonPrompterEvent(string message)
	{
		// make sure the prompter is NOT showing
		if (isVisible)
		{
			fade(false, fadeDuration);
		}
		// blank the currently displayed message
		PrompterText.text = PrompterBlankingValue;
	}

	public void setPrompterTitle(string title)
	{
		PrompterPanelTitle.text = title;
	}
	public void setPrompterText(string text)
	{
		PrompterText.text = text;
	}
	public void togglePrompterVisibility(bool active, float fadeDuration)
	{
		fade(active, fadeDuration);
	}

	private void togglePrompter(bool active)
	{
		canvasFadeGroup.interactable = active;
		canvasFadeGroup.blocksRaycasts = active;
		canvasFadeGroup.alpha = (active) ? 1f : 0f;
	}

	private void fade(bool active, float duration)
	{
		if(fadeTween != null)   // we are currently fadinging, so we need to stop that animation
		{
			fadeTween.Kill(false);	// don't run the callback after kill
		}
		
		fadeTween = canvasFadeGroup.DOFade((active) ? 1f : 0f, duration);
		fadeTween.onComplete += () => { togglePrompter(active); };	
	}

	// fade the panel in and out forever
	private IEnumerator TestFade()
	{
		while(true)
		{
			yield return new WaitForSeconds(2f);
			Debug.Log("fading in");
			fade(true, fadeDuration);
			yield return new WaitForSeconds(3f);
			Debug.Log("fading out");
			fade(false, fadeDuration);
		}
	}
}
