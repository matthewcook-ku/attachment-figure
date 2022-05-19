using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UXF;

public class ExprSettings : MonoBehaviour
{
    /* public static string FilePath { get; set; }
     public static string Subjectname { get; set; }
     public static int ID { get; set; }
     public static int SessionNum { get; set; }
     */

    public int experiment = 1;

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

    public void BeginExperiment()
    {
        Block attachmentFigureBlock = Session.instance.CreateBlock(1);
        Session.instance.Begin("Experiment1", "Tiger Ruan", 1, null, null);

        // Settings newSettings = Settings.empty;
        // session = Session.instance;
        // Debug.Log(session);
        // Debug.Log(session.settings);
        Session.instance.settings.SetValue("FilePath", 100);
        Session.instance.settings.SetValue("SubjectName", "Tiger Ruan");
        Session.instance.settings.SetValue("ID", "2871659");
        Session.instance.settings.SetValue("Session", "10");


        SceneManager.LoadScene(experiment, LoadSceneMode.Single);
    }


}
