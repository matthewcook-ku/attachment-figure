using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using System.IO;
using System;
using System.Text.RegularExpressions;

// UXF Session Driver
//
// This script drives the major events of the study session. This is the place to sequence out any events fro the study.
// This is also the central place to put settings for this session. Objects implementing the session should come here to collect those settings. 
// This will probably need to be broken up into different sessions for the different studies.
//
// Elements
// - UXF event handlers
// - manual tracking data collection
// - updating UI with live info
//
// Connections:
// Subject - subject controller, and down to proxemics trackers
// Agent - agent controller
// ExperimenterUI
// SubjectUI
//
// UXF_Rig - connect event handlers to Events tab

public class StudyController : MonoBehaviour
{
    [Tooltip("Object representing the subject.")]
    public SubjectController subject;

    [Tooltip("Object representing the agent.")]
    public AgentController agent;
    [Tooltip("Layer Mask for proxemics gaze target ray casting.")]
    public LayerMask gazeTargetLayerMask;

    [Tooltip("Refs to the UI canvas objects.")]
    public ExperimenterUIController experimenterUI;
    public SubjectUIController subjectUI;
    // public UIStartController startUI;

    [Tooltip("The maximum field of view in degrees of the head mounted display. Some common vlues include:\n- Vive Pro Eye: 110\n- Oculus Quest 2: 89")]
    public int HMDFieldOfView = 110;

    [Tooltip("Frequency of tracking data collection in sec. 0.1 is 1/10 second.")]
    public float trackingInterval = 0.3f;
    [Tooltip("Frequency of averaged data collection in sec. 60 is 1 minute.")]
    public float averageInterval = 60.0f;

    // This method should be called by the OnSessionBegin event in the UXF rig.
    public void SessionBegin(Session session)
	{
        Debug.Log("StudyController: Starting Session...");

        // tell the system where to look for the settings file
        session.settings.SetValue("trial_specification_name", "question set.csv");

        // process the file
        /*
        try
        {
            // !!! there is already a block with 1 trial in the session. need to see what happens to that trial. 
            // !!! ALSO, this method does not handle items in the CSV with commas. Will need to address this somehow...
            BuildExperimentFromCSV(session, "trial_specification_name");
        }
        catch(System.Exception e)
		{
            // will error if no file sepcificed by the settings key, or if file does not exist.
            Debug.LogError("StudyController: Error reading study setting from file: " + e.Message);
            return;
		}
        */

        string settingsString = "";
        settingsString += "StudyController: session settings from file: " + session.settings.GetString("trial_specification_name") + " ";
        for(int i = 0; i < session.blocks.Count; i++)
		{
            settingsString += "Block[" + i + "]: " + session.blocks[i].trials.Count + " trials";
		}
        Debug.Log(settingsString);
    }

    // This method should be called by the OnTrialBegin event in the UXF rig.
    public void TrialStart(Trial trial)
    {
        Debug.Log("StudyController: Starting Trial...");
        
        // collect any needed settings from the trial object here.

        // turn on the FPS controller
        //Debug.Log("Turning on FPS controller.");
        //subject.fps.enabled = true;

        // begin start UI
        /*
        Debug.Log("Load start UI");
        startUI.gameObject.SetActive(true);
        */

        // start experimenter UI
        Debug.Log("Load experimenter UI.");
        experimenterUI.gameObject.SetActive(true);

        // start subject UI
        //Debug.Log("Load subject UI.");
        //subjectUI.gameObject.SetActive(true);

        // start the proxemics trackers
        Debug.Log("Starting proxemics trackers.");
        StartCoroutine(ProxemicsTrackingManualRecord());
        StartCoroutine(ProxemicsTrackingManualRecordAverage());
    }

    // This coroutine method will set up recording and then continue every trackingInterval seconds to manually signal the porixemics tracker to record a row of data.
    IEnumerator ProxemicsTrackingManualRecord()
    {
        while(true)
        {
            if (subject.proxemicsTracker.Recording)
            {
                //Debug.Log("Recording data row...");
                subject.proxemicsTracker.RecordRow();
            }

            //float distance = subject.proxemicsTracker.AgentSubjectDistance();
            //float gaze = subject.proxemicsTracker.GazeScore();

            // display the new data in the UI
            //Debug.Log("Updating UI with tracking data.\n" +
            //    "distance: " + distance + "\n" +
            //    "gaze: " + gaze
            //    );

            //if (experimenterUI == null) Debug.Log("experimenterUI null in manual record.");
            //if (experimenterUI.experimentPanelController == null) Debug.Log("experimentPanelController null in manual record.");
            //if (experimenterUI.experimentPanelController.distanceField == null) Debug.Log("distanceField null in manual record.");

            //experimenterUI.experimentPanelController.distanceField.SetTextWithoutNotify(distance.ToString());
            //experimenterUI.experimentPanelController.gazeField.SetTextWithoutNotify(gaze.ToString());

            //subjectUI.headsetReadoutController.distanceField.SetTextWithoutNotify(distance.ToString());
            //subjectUI.headsetReadoutController.gazeField.SetTextWithoutNotify(gaze.ToString());

            // pause for the tracking interval
            yield return new WaitForSeconds(trackingInterval);
        }
    }

    // Coroutine to record average data from the proxemics tracker.
    IEnumerator ProxemicsTrackingManualRecordAverage()
    {
        while (true)
        {
            if (subject.proxemicsTracker.Recording && subject.proxemicsAverageTracker.Recording)
            {
                //Debug.Log("Recording data average row...");
                subject.proxemicsAverageTracker.RecordRow();
            }

            // pause for the tracking interval
            yield return new WaitForSeconds(averageInterval);
        }
    }


    // This method should be called by the OnTrialEnd event in the UXF rig.
    public void TrialEnd(Trial trial)
    {
        Debug.Log("Ending Trial...");



        // write the trial tracking averages to the results
        //Debug.Log("Session Average Distance: " + subject.proxemicsTracker.globalAverageDistance);
        //Debug.Log("Session Average Gaze: " + subject.proxemicsTracker.globalAverageGaze);

        //trial.result["session average distance"] = subject.proxemicsTracker.globalAverageDistance;
        //trial.result["session average gaze score"] = subject.proxemicsTracker.globalAverageGaze;

        // write out the stats for this trial
        subject.proxemicsTracker.closeCurrentTrial();

        // if this is the last trial, save the global data as well.
        // if we wait to do this until PreSessionEnd the results file will already have been writen.
        if(trial == Session.instance.LastTrial)
            subject.proxemicsTracker.closeSession();
    }
    // This method should be called by the PreSessionEnd event in the UXF rig.
    public void PreSessionEnd(Session session)
    {
        Debug.Log("Ending Session...");

        // stop the tracking
        StopAllCoroutines();
    }
    // This method should be called by the OnSessionEnd event in the UXF rig.
    public void SessionEnd(Session session)
    {
        Debug.Log("Session Ended ... Safe to Quit");
    }






    // Replace the CSVExperimentBuilder build function 
    // This does the exact same thing, but uses our CSV parsing rather than the standard.
    public void BuildExperimentFromCSV(Session session, string csvFileKey)
    {
        // check if settings contains the csv file name
        if (!session.settings.ContainsKey(csvFileKey))
        {
            throw new Exception($"CSV file name not specified in settings. Please specify a CSV file name in the settings with key \"{csvFileKey}\".");
        }

        // get the csv file name
        string csvName = session.settings.GetString(csvFileKey);

        // check if the file exists
        string csvPath = Path.GetFullPath(Path.Combine(Application.streamingAssetsPath, csvName));
        if (!File.Exists(csvPath))
        {
            throw new Exception($"CSV file at \"{csvPath}\" does not exist!");
        }

        // read the csv file
        string[] csvLines = File.ReadAllLines(csvPath);

        // parse as table
        var table = ParseCSV(csvLines);

        // build the experiment.
        // this adds a new trial to the session for each row in the table
        // the trial will be created with the settings from the values from the table
        // if "block_num" is specified in the table, the trial will be added to the block with that number
        session.BuildFromTable(table, true);
    }


    // Build a table from lines of CSV text. This function replaces the function FromCSV from UXFDataTable
    // The original function does not handle CSV files with data containing commas or quotes.
    // This function will properly process those inputs.
    public static UXFDataTable ParseCSV(string[] csvLines)
    {
        // in a CSV file:
        // - if the field contains a comma, the field will be surrounded by ""
        //      a comma here, and then stuff  -->  "a comma here, and then stuff"
        // - if the field contains a quote, then the field will be surrounded by "" and the internal quotes will be doubled
        //      a field with "quotes" in it  -->  "a field with ""quotes"" in it"

        Regex regexCSV = new Regex(",(?=([^\"]*\"[^\"]*\")*[^\"]*$)", RegexOptions.ExplicitCapture); 
        // ,                    match a comma followed by...
        // (?= ... )            positive lookahead - match items in parens, but don't consume them (i.e. find the match, but don't split the string on it)
        // ([^\"]*\"[^\"]*\")*  match this zero or more times
        //                          [^\"]*  zero or more items NOT a quote
        //                          \"      then a quote
        //                          [^\"]*  zero or more items NOT a quote (again)
        //                          \"      quote again (again)
        // [^\"]*               zero or more items NOT a quote
        // $                    end of string

        string[] headers = regexCSV.Split(csvLines[0]);
        UXFDataTable table = new UXFDataTable(csvLines.Length - 1, headers);
        Debug.Log("StudyController: CSVHeaders: \n" + stringArrayToString(headers, "\n"));

        // traverse down rows
        for (int i = 1; i < csvLines.Length; i++)
        {
            string[] values = regexCSV.Split(csvLines[i]);
            Debug.Log("StudyController: CSVRow[" + i + "]: \n" + stringArrayToString(values, "\n"));

            // if last line, just 1 item in the row, and it is blank, then ignore it
            if (i == csvLines.Length - 1 && values.Length == 1 && values[0].Trim() == string.Empty) break;

            // check if number of columns is correct
            if (values.Length != headers.Length) throw new System.Exception($"CSV line {i} has {values.Length} columns, but expected {headers.Length}");

            // build across the row
            var row = new UXFDataRow();
            for (int j = 0; j < values.Length; j++)
                row.Add((headers[j], values[j].Trim('\"')));

            Debug.Log("StudyController: data row [" + i + "]: \n" + dataRowToString(row, "\n"));
            
            table.AddCompleteRow(row);
        }

        return table;
    }

    // helper function for debugging
    public static string stringArrayToString(string[] strings, string delem, char trim = '\0')
    {
        string result = "";
        for(int i = 0; i < strings.Length; i++)
		{
            result += strings[i].Trim(trim) + delem;
		}
        return result;
	}

    // helper function for debugging
    public static string dataRowToString(UXFDataRow row, string delem)
	{
        string result = "";
        for(int i = 0; i < row.Count; i++)
		{
            result += "row[\"" + row[i].Item1 + "\"]: " + row[i].Item2 + delem;
		}
        return result;
	}
}
