#define RTVOICE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Crosstales.RTVoice.Model;

#if READSPEAKER
//using ReadSpeaker;
#endif

#if RTVOICE
using Crosstales.RTVoice;
#endif

// TTS Manager
//
// This component manages conversion of text to speech, and serves as a common wrapper around whatever TTS plugin we are using.

public class TextSpeaker : MonoBehaviour
{
#if RTVOICE

	// Events
	public static event Action OnInit;

	public float VoiceVolume;   // 0.0 - 1.0, default 1 in RT-Voice
	public static float VoiceVolumeMin = 0.0f;
	public static float VoiceVolumeMax = 1.0f;
	public float VoicePitch;    // 0.0 - 2.0, default 1 in RT-Voice
	public static float VoicePitchMin = 0.0f;
	public static float VoicePitchMax = 2.0f;
	public float VoiceSpeed;    // 0.01 - 3.0, default 1 in RT-Voice
	public static float VoiceSpeedMin = 0.01f;
	public static float VoiceSpeedMax = 3.0f;

	public bool Ready
	{
		get
		{
			return Speaker.Instance.areVoicesReady;
		}
	}

	private string _voicename;
	public string VoiceName
	{
		get
		{
			if(Voice == null)
			{
				VoiceName = Speaker.Instance.DefaultVoiceName;
				Voice = Speaker.Instance.VoiceForName(VoiceName);
			}
			return _voicename;
		}
		set
		{
			//Debug.Log("Setting voice for name: " + value + " ...");
			Crosstales.RTVoice.Model.Voice foundVoice = Speaker.Instance.VoiceForName(value);
			if(foundVoice != null)
			{
				//Debug.Log("Voice found! Setting voice to: " + value);
				_voicename = value;
				Voice = foundVoice;
			}
			else
			{
				//Debug.LogError("TextSpeaker: Voice not found for voice name: " + value);
			}
		}
	}
	public List<string> AvailableVoices
	{
		get
		{
			// make a list of voice names from the list of voices
			return Speaker.Instance.Voices.ConvertAll<string>(x => x.Name);
		}
	}

	Crosstales.RTVoice.Model.Voice Voice;

	private void Start()
	{
		Speaker.Instance.OnVoicesReady += OnVoicesReady;
		Speaker.Instance.OnSpeakStart += OnSpeakStart;
		Speaker.Instance.OnSpeakComplete += OnSpeakComplete;
	}
	private void OnDisable()
	{
		Speaker.Instance.OnVoicesReady -= OnVoicesReady;
		Speaker.Instance.OnSpeakStart -= OnSpeakStart;
		Speaker.Instance.OnSpeakComplete -= OnSpeakComplete;
	}

	private void OnVoicesReady()
	{
		//Debug.Log("Voices are ready!");

		// set the default voice
		VoiceName = Speaker.Instance.DefaultVoiceName;

		// select the default voice to start
		//Debug.Log("startup speech test");
		//Say("Hello world!");

		// let anyone listening know the TTS system is ready
		OnInit?.Invoke();
	}
	private void OnSpeakStart(Wrapper wrapper)
	{
		//Debug.Log("Speak Start: " + wrapper);
	}
	private void OnSpeakComplete(Wrapper wrapper)
	{
		//Debug.Log("Speak Complete: " + wrapper);
	}

	public void Say(string text)
	{
		if(!Speaker.Instance.areVoicesReady)
		{
			Debug.LogError("TextSpeaker: The voice system is not ready to speak.");
			return;
		}
		Speaker.Instance.SpeakNative(text, Voice, VoiceSpeed, VoicePitch, VoiceVolume, true);
	}

	public void StopSpeaking()
	{
		Speaker.Instance.Silence();
	}

#endif

#if READSPEAKER
	//public TTSSpeaker speaker;
	public float delay = 0.0f;

	private void Start()
	{
		TTS.Init();

		Debug.Log("Speaking some test text from ReadSpeaker");
		Say("This is a test!");
	}

	void Update()
	{
		// press "x" to stop playback
		if (UnityEngine.InputSystem.Keyboard.current.xKey.wasPressedThisFrame)
		{
			TTS.InterruptAll();
			StopAllCoroutines();
		}
	}

	public void Say(string text)
	{
		StartCoroutine(SpeakerCoroutine(text));
	}

	IEnumerator SpeakerCoroutine(string text)
	{
		TTS.Say(text, speaker);
		yield return new WaitUntil(() => !speaker.audioSource.isPlaying);
		yield return new WaitForSeconds(delay);
	}
#endif
}
