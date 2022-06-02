using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ReadSpeaker;

public class VoiceTest : MonoBehaviour
{
    public TTSSpeaker speaker;
    Slider vol_slider;
    Slider pitch_slider;

    // Start is called before the first frame update
    void Start()
    {
        vol_slider = GameObject.Find("VolSlider").GetComponent<Slider>();
        pitch_slider = GameObject.Find("PitchSlider").GetComponent<Slider>();

        // set initial values
        vol_slider.value = 0.5f;
        pitch_slider.value = 0.5f;
    }

    void testVoice()
    {
        float voice_vol = vol_slider.value;
        float voice_pitch = pitch_slider.value;

        // update settings in speaker
        speaker.GetSpeechCharacteristics().Volume = (int)(voice_vol * 500);
        speaker.GetSpeechCharacteristics().Pitch = (int)(50 + (voice_pitch * 140));
    }
}
