using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ReadSpeaker;
using TMPro;

public class VoiceTest : MonoBehaviour
{
    public TTSSpeaker speaker;
    public Crosstales.RTVoice.LiveSpeaker rtvoice_speaker; 
    private int voice_select = 0;
    private string demo_text = "Peter piper picked a bunch of pickled peppers.";
    Slider vol_slider;
    Slider pitch_slider;
    Slider speed_slider;

    // Start is called before the first frame update
    void Start()
    {
        vol_slider = GameObject.Find("VolSlider").GetComponent<Slider>();
        pitch_slider = GameObject.Find("PitchSlider").GetComponent<Slider>();
        speed_slider = GameObject.Find("SpeedSlider").GetComponent<Slider>();
 
        // set initial values
        vol_slider.value = 0.5f;
        pitch_slider.value = 0.5f;
        speed_slider.value = 0.5f;
    }

    void testVoice()
    {
        // get voice selection from dropdown
        voice_select = transform.parent.Find("VoiceGrp").Find("VoiceSelectDropdown").GetComponent<TMP_Dropdown>().value;

        float voice_vol = vol_slider.value;
        float voice_pitch = pitch_slider.value;
        float voice_speed = speed_slider.value;

        // update settings in speaker
        if(voice_select == 0)
        {
            speaker.GetSpeechCharacteristics().Volume = (int)(voice_vol * 500);
            speaker.GetSpeechCharacteristics().Pitch = (int)(50 + (voice_pitch * 140));
            speaker.GetSpeechCharacteristics().Speed = (int)(80 + (voice_speed * 70));
            speaker.GetComponent<TextSpeaker>().Say(demo_text);
        }
        else if(voice_select == 1)
        {
            string[] rt_args = { demo_text, "", "", (voice_speed * 2).ToString(), (voice_pitch * 2).ToString(), voice_vol.ToString() } ;

            rtvoice_speaker.SpeakNativeLive(rt_args);
        }
        else if(voice_select == 2)
        {
            string[] rt_args = { demo_text, "", "Zira", (voice_speed *2).ToString(), (voice_pitch * 2).ToString(), voice_vol.ToString() };

            rtvoice_speaker.SpeakNativeLive(rt_args);
        }
    }
}
