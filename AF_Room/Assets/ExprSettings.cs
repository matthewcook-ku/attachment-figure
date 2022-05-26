using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using UXF;
using TMPro;

public class ExprSettings : MonoBehaviour
{
    /* public static string FilePath { get; set; }
     public static string Subjectname { get; set; }
     public static int ID { get; set; }
     public static int SessionNum { get; set; }
     */

    TMP_Dropdown expr_drop;
    Slider voice_slider;
    ModelSelectTracker model;
    public LocalFileDataHander fileDataHandler;
    public int experiment = 1;


    // member variables
    string part_name = "";
    string file_path = "";
    int session_num;
    int ID_num;
    int voice_ind;
    float voice_pitch;
    float voice_vol;
    int model_num;

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
        if(checkForms())
        {
            getForms();
        }
        else
        {
            activateErrorForm();
        }
        
    }

    public void activateErrorForm()
    {
        transform.root.Find("ErrorBox").GetComponent<ErrorBoxToggle>().DisplayError();
    }
    private bool checkForms()
    {
        if (checkName() || checkSession() || checkFile() || checkID())
        {
            return false;
        }
        return true;

    }

    private bool checkName()
    {
        return string.IsNullOrEmpty(GameObject.Find("NameInput").GetComponent<TMP_InputField>().text);
    }

    private bool checkSession()
    {
        return string.IsNullOrEmpty(GameObject.Find("SesInput").GetComponent<TMP_InputField>().text);
    }

    private bool checkFile()
    {
        return string.IsNullOrEmpty(GameObject.Find("FileSaveInput").GetComponent<TMP_InputField>().text);
    }

    private bool checkID()
    {
        return string.IsNullOrEmpty(GameObject.Find("IDInput").GetComponent<TMP_InputField>().text);

    }

    private void getForms()
    {
        // Participant name get
        part_name = GameObject.Find("NameInput").GetComponent<TMP_InputField>().text;
        Debug.Log("Participant name: " + part_name);

        // Filepath get
        file_path = GameObject.Find("FileSaveInput").GetComponent<TMP_InputField>().text;
        Debug.Log("File path: " + file_path);
        fileDataHandler.storagePath = file_path;

        // Session get
        session_num = int.Parse(GameObject.Find("SesInput").GetComponent<TMP_InputField>().text);
        Debug.Log("Session number: " + session_num);


        // ID get
        ID_num = int.Parse(GameObject.Find("IDInput").GetComponent<TMP_InputField>().text);
        Debug.Log("ID: " + ID_num);


        // Experiment selection get
        /*
        expr_drop = GameObject.Find("ExpSelectDropDown").GetComponent<TMP_Dropdown>();
        int expr_ind = expr_drop.value;
        string expr_str = expr_drop.options[expr_ind].text;
        Debug.Log("Experiment index: " + expr_ind);
        Debug.Log("Experiment string: " + expr_str);
        */

        // Voice selection get
        voice_ind = GameObject.Find("VoiceSelectDropdown").GetComponent<TMP_Dropdown>().value;
        Debug.Log("Voice index: " + voice_ind);

        // Voice pitch slider get (Range from 0 to 1)
        voice_pitch = GameObject.Find("PitchSlider").GetComponent<Slider>().value;
        Debug.Log("Voice pitch: " + voice_pitch);

        // Voice volume slider get (Range from 0 to 1)
        voice_vol = GameObject.Find("VolSlider").GetComponent<Slider>().value;
        Debug.Log("Voice pitch: " + voice_vol);


        // Model selection get
        model_num = GameObject.Find("ModelSlctGroup").GetComponent<ModelSelectTracker>().keyIndex;
        // Experiment start
        Block attachmentFigureBlock = Session.instance.CreateBlock(1);
        Session.instance.Begin("Experiment 1", part_name, 1);

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
        Session.instance.settings.SetValue("Model", model_num);


        // hard-coded disabling parent, disabling start UI
        // if you move the hierarchy you will need to +/- .parent
        this.transform.parent.gameObject.SetActive(false);

        // For multi-scene experiments
        // SceneManager.LoadScene(expr_ind, LoadSceneMode.Single);
    }


}
