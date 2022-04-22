using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadSpeaker;

// ReadSpeaker.ai TTS Manager
//
// This component initializes and manages the ReadSpeaker TTS system.
// This should be added to a game object with a TTSSpeaker and an AudioSource.
//
// see: https://www.readspeaker.ai/blog/unity-text-to-speech-plugin/
// see: https://youtu.be/VjTudTubypE
//
// InputSystem:
// 'x' - stop voice playback

public class TextSpeaker : MonoBehaviour
{
    public TTSSpeaker speaker;
    public float delay = 0.0f;

    private void Start()
    {
        TTS.Init();
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
}
