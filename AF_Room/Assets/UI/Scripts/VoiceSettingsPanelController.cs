using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VoiceSettingsPanelController : MonoBehaviour
{
	public Canvas PopUpUICanvas;
	public TextSpeaker Speaker;
	public TMP_Dropdown VoiceSelectionDropdown;
	public Slider VolumeSlider;
	public TMP_InputField VolumeInputField;
	public Slider PitchSlider;
	public TMP_InputField PitchInputField;
	public Slider SpeedSlider;
	public TMP_InputField SpeedInputField;

	private string demo_text = "Peter piper picked a patch of pickled peppers.";


	// Start is called before the first frame update
	void Start()
	{
		// Set up UI values
		VolumeSlider.minValue = TextSpeaker.VoiceVolumeMin;
		VolumeSlider.maxValue = TextSpeaker.VoiceVolumeMax;
		PitchSlider.minValue = TextSpeaker.VoicePitchMin;
		PitchSlider.maxValue = TextSpeaker.VoicePitchMax;
		SpeedSlider.minValue = TextSpeaker.VoiceSpeedMin;
		SpeedSlider.maxValue = TextSpeaker.VoiceSpeedMax;

		// wait for the TTS to initialize if needed, and then we'll set up the availalbe voices
		if (Speaker.Ready)
		{
			UpdateUI();
		}
		else
		{
			Debug.Log("VoiceSettingsPanelController: TTS is not ready yet...");
			TextSpeaker.OnInit += UpdateUI;
		}
	}

	public void UpdateUI()
	{
		Debug.Log("VoiceSettingsPanelController: Updating UI...");

		VolumeSlider.SetValueWithoutNotify(Speaker.VoiceVolume);
		VolumeInputField.text = Speaker.VoiceVolume.ToString("F2");
		PitchSlider.SetValueWithoutNotify(Speaker.VoicePitch);
		PitchInputField.text = Speaker.VoicePitch.ToString("F2");
		SpeedSlider.SetValueWithoutNotify(Speaker.VoiceSpeed);
		SpeedInputField.text = Speaker.VoiceSpeed.ToString("F2");

		// add available voices to the list, and select current
		List<string> voices = Speaker.AvailableVoices;
		int currentVoiceIndex = voices.IndexOf(Speaker.VoiceName);
		VoiceSelectionDropdown.ClearOptions();
		VoiceSelectionDropdown.AddOptions(voices);
		VoiceSelectionDropdown.SetValueWithoutNotify(currentVoiceIndex);

		// take us off the event if we were added. Note that this is safe, even if we were not added.
		TextSpeaker.OnInit -= UpdateUI;
	}

	public void OnVolumeSliderValueChanged(float value)
	{
		VolumeInputField.text = Speaker.VoiceVolume.ToString("F2");
		Speaker.VoiceVolume = value;
	}
	public void OnPitchSliderValueChanged(float value)
	{
		PitchInputField.text = Speaker.VoicePitch.ToString("F2");
		Speaker.VoicePitch = value;
	}
	public void OnSpeedSliderValueChanged(float value)
	{
		SpeedInputField.text = Speaker.VoiceSpeed.ToString("F2");
		Speaker.VoiceSpeed = value;
	}
	public void OnVoiceSelectionChanged(TMP_Dropdown dropdown)
	{
		Speaker.VoiceName = VoiceSelectionDropdown.options[VoiceSelectionDropdown.value].text;
	}
	public void OnTestButtonPressed()
	{
		Speaker.StopSpeaking();
		Speaker.Say(demo_text);
	}

	public void OpenPanel()
	{
		// open the popup canvas, and then turn on this panel.
		PopUpUICanvas.gameObject.SetActive(true);
		gameObject.SetActive(true);
	}
	public void ClosePanel()
	{
		Speaker.StopSpeaking();

		// make sure to close the panel AND canvas, as canvas will block other conavas's input.
		gameObject.SetActive(false);
		PopUpUICanvas.gameObject.SetActive(false);
	}
}
