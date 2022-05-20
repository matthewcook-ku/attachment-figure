using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UXF;
using TMPro;

public class ExprSettings : MonoBehaviour
{
    /* public static string FilePath { get; set; }
     public static string Subjectname { get; set; }
     public static int ID { get; set; }
     public static int SessionNum { get; set; }
     */

    TMP_InputField input;
    TMP_Dropdown expr_drop;
    Slider voice_slider;
    public int experiment = 1;

    /*
    public enum Model
    {
        Male,
        Female
    }

    public enum Voice
    {
        Voice1, 
        Voice2
    }
    */

    public void BeginExperiment()
    {
        
        // Participant name get
        input = GameObject.Find("NameInput").GetComponent<TMP_InputField>();
        string part_name = input.text;
        Debug.Log("Participant name: " + part_name);

        // Filepath get
        input = GameObject.Find("FileSaveInput").GetComponent<TMP_InputField>();
        string file_path = input.text;
        Debug.Log("File path: " + file_path);


        // Session get
        input = GameObject.Find("SesInput").GetComponent<TMP_InputField>();
        int session_num = int.Parse(input.text);
        Debug.Log("Session number: " + session_num);


        // ID get
        input = GameObject.Find("IDInput").GetComponent<TMP_InputField>();
        int ID_num = int.Parse(input.text);
        Debug.Log("ID: " + ID_num);


        // Experiment selection get
        expr_drop = GameObject.Find("ExpSelectDropDown").GetComponent<TMP_Dropdown>();
        int expr_ind = expr_drop.value;
        string expr_str = expr_drop.options[expr_ind].text;
        Debug.Log("Experiment index: " + expr_ind);
        Debug.Log("Experiment string: " + expr_str);

        // Voice selection get
        expr_drop = GameObject.Find("VoiceSelectDropdown").GetComponent<TMP_Dropdown>();
        int voice_ind = expr_drop.value;
        Debug.Log("Voice index: " + voice_ind);

        // Voice pitch slider get (Range from 0 to 1)
        voice_slider = GameObject.Find("PitchSlider").GetComponent<Slider>();
        float voice_pitch = voice_slider.value;
        Debug.Log("Voice pitch: " + voice_pitch);

        // Voice volume slider get (Range from 0 to 1)
        voice_slider = GameObject.Find("VolSlider").GetComponent<Slider>();
        float voice_vol = voice_slider.value;
        Debug.Log("Voice pitch: " + voice_vol);



        // Experiment start
        Block attachmentFigureBlock = Session.instance.CreateBlock(1);
        Session.instance.Begin(expr_str, part_name, 1);

        // Settings storage
        Session.instance.settings.SetValue("FilePath", file_path);
        Session.instance.settings.SetValue("SubjectName", part_name);
        Session.instance.settings.SetValue("ID", ID_num);
        Session.instance.settings.SetValue("Session", session_num);

        Session.instance.settings.SetValue("Voice", voice_ind);
        Session.instance.settings.SetValue("Voice_pitch", voice_pitch);
        Session.instance.settings.SetValue("Voice_vol", voice_vol);

        // Hard coded model selection for now
        // TODO: 1 -> model_ind eventually
        Session.instance.settings.SetValue("Model", 1); 



        this.transform.parent.parent.gameObject.SetActive(false);

        // For multi-scene experiments
        // SceneManager.LoadScene(expr_ind, LoadSceneMode.Single);
    }


}
