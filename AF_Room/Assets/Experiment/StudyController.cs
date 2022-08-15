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

        // Build the set of trials
        Debug.Log("StudyController: Building trails...");
        
        // tell the system where to look for the settings file
        session.settings.SetValue("trial_specification_name", "question set.csv");

        // read and process the settings file to create blocks and trials
        BuildExperimentFromCSV(session, "trial_specification_name");

        // start experimenter UI
        Debug.Log("Load experimenter UI.");
        experimenterUI.gameObject.SetActive(true);

        // begin the first trial - this will trigger the OnTrialStart event to prep the first trial
        // "begin" is a slight misnomer, this will do the setup in the trackers to be ready to record data for the next trial
        // and then we'll start the trackers next. 
        session.BeginNextTrial();

        // start the proxemics trackers
        Debug.Log("Starting proxemics trackers.");
        StartCoroutine(ProxemicsTrackingManualRecord());
        StartCoroutine(ProxemicsTrackingManualRecordAverage());
    }

    // This method should be called by the OnTrialBegin event in the UXF rig.
    public void TrialStart(Trial trial)
    {
        Debug.Log("StudyController: Starting Trial...");
        Debug.Log("Trial Prompt[" + trial.settings.GetString("prompt id") + "]: " + trial.settings.GetString("prompt"));

        // initilize the proxemics tracker for the new trial
        subject.proxemicsTracker.initNextTrial(trial);
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
        Debug.Log("Ending Trial[" + (trial.number - 1) + "]...");

        // write out the stats for this trial
        subject.proxemicsTracker.closeCurrentTrial(trial);

        // if this is the last trial, or if we are ending, save the global data as well.
        // if we wait to do this until PreSessionEnd the results file will already have been writen.
        if (trial.session.isEnding || trial == trial.session.LastTrial)
        {
            subject.proxemicsTracker.closeSession(trial.session);
        }

        Debug.Log("Ending Trial...done");
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
        else
		{
            Debug.Log($"Reading file from settings key: \"{csvFileKey}\".");
        }

        // get the csv file name
        string csvName = session.settings.GetString(csvFileKey);

        // check if the file exists
        string csvPath = Path.GetFullPath(Path.Combine(Application.streamingAssetsPath, csvName));
        if (!File.Exists(csvPath))
        {
            throw new Exception($"CSV file at \"{csvPath}\" does not exist!");
        }
        else
        {
            Debug.Log($"Reading CSV file: \"{csvName}\"\npath=\"{csvPath}\".");
        }

        // read the csv file
        string[] csvLines = File.ReadAllLines(csvPath);

        // parse as table
        Debug.Log("Parsing file...");
        var table = ParseCSV(csvLines);

        // build the experiment.
        // this adds a new trial to the session for each row in the table
        // the trial will be created with the settings from the values from the table
        // if "block_num" is specified in the table, the trial will be added to the block with that number
        session.BuildFromTable(table, true);

        // report what was done
        string settingsString = "";
        settingsString += "Trial settings: ";
        for (int i = 0; i < session.blocks.Count; i++)
        {
            settingsString += "\n" + "Block[" + i + "]: " + session.blocks[i].trials.Count + " trials";
            for(int j = 0; j < session.blocks[i].trials.Count; j++)
			{
                settingsString += "\n\t" + "trial[" + j + "]: ";
                settingsString += "(" + session.blocks[i].trials[j].settings.GetString("prompt id") + ") ";
                settingsString += session.blocks[i].trials[j].settings.GetString("prompt");
            }
        }
        Debug.Log(settingsString);
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
        //Debug.Log("headers: \n" + stringArrayToString(headers, "\t", "\n"));

        // traverse down rows
        for (int i = 1; i < csvLines.Length; i++)
        {
            string[] values = regexCSV.Split(csvLines[i]);

            // if last line, just 1 item in the row, and it is blank, then ignore it
            if (i == csvLines.Length - 1 && values.Length == 1 && values[0].Trim() == string.Empty) break;

            // check if number of columns is correct
            if (values.Length != headers.Length) throw new System.Exception($"CSV line {i} has {values.Length} columns, but expected {headers.Length}");

            // build across the row
            var row = new UXFDataRow();
            for (int j = 0; j < values.Length; j++)
                row.Add((headers[j], values[j].Trim('\"')));

            //Debug.Log("row[" + i + "]: \n" + dataRowToString(row, "\t", "\n"));
            
            table.AddCompleteRow(row);
        }

        return table;
    }

    // helper function for debugging
    public static string stringArrayToString(string[] strings, string before, string after)
    {
        string result = "";
        for(int i = 0; i < strings.Length; i++)
		{
            result += before + strings[i] + after;
		}
        return result;
	}

    // helper function for debugging
    public static string dataRowToString(UXFDataRow row, string before, string after)
	{
        string result = "";
        for(int i = 0; i < row.Count; i++)
		{
            result += before + "row[\"" + row[i].Item1 + "\"]: " + row[i].Item2 + after;
		}
        return result;
	}
}
