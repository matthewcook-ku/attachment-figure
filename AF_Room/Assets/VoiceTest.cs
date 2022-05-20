using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ReadSpeaker;

public class VoiceTest : MonoBehaviour
{
    public GameObject gameObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void testVoice()
    {
        Slider voice_slider;
        TTSSpeaker speaker = gameObj.GetComponent<TTSSpeaker>();

        voice_slider = GameObject.Find("VolSlider").GetComponent<Slider>();
        float voice_vol = voice_slider.value;

        voice_slider = GameObject.Find("PitchSlider").GetComponent<Slider>();
        float voice_pitch = voice_slider.value;

        // Need to figure out the setter functions for TTSSpeaker

        // speaker.volume = voice_vol * 500; 
        // speaker.pitch = 50 + (voice_pitch * 140);
    }
}
