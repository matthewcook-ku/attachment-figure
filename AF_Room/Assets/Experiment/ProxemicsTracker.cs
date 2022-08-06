using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using System;

// UXF Tracker for proximity data
// 
// Tracker to record proxemics data for the trial:
//  - distance between this object and a target
//  - how much this object is "looking at" the target
// Attach this tracker to an object that represents the location and direction of the player's view. Probably a camera or empty parented to the camera.
// Create a layer to represent objects that will be targets. This is used for ray casting.
//
// See: https://github.com/immersivecognition/unity-experiment-framework/wiki/Tracker-system
//
// Unity Settings:
// Layer 6 set as Gaze Target
//
// Connections:
// UXF_Rig - Data Collection Tab: add to tracked object.

public class ProxemicsTracker : Tracker
{

    // events
    public static event Action<ProxemicsTracker> OnTakeMeasurement;
    public static event Action<bool> OnTrackerRaycast;

    // collect from StudyController
    private AgentController agent = null;
    private int HMDFieldOfView;
    private LayerMask gazeTargetLayerMask; // layer #6

    // the most recent calculations for these properties. 
    public float distance { get; private set; }
    public float gaze { get; private set; }

    // running count of samples since the last average interval 
    private int intervalSampleCountDistance = 0;
    private int lastIntervalSampleCountDistance = 0;
    private int intervalSampleCountGaze = 0;
    private int lastIntervalSampleCountGaze = 0;

    // running totals for average interval
    private float runningIntervalTotalDistance = 0.0f;
    private float runningIntervalTotalGaze = 0.0f;

    // values averaged over the averageInterval
    public float averageIntervalDistance { get; private set; }
    public float averageIntervalGaze { get; private set; }

    // time value for the last time average interval was calculated
    private float prevAverageInternvalTimeStamp = 0.0f;

    // trial level stats
    private float trialAverageDistance;
    private float trialMedianDistance;
    private double trialM2Distance;
    private double trialSDDistance;

    private float trialAverageGaze;
    private float trialMedianGaze;
    private double trialM2Gaze;
    private double trialSDGaze;

    // time value for the start of the current trial
    private float trialStartTimeStamp;
    private int currentTrial;   // UXF trials my be run in random order (I think), so we'll keep track of this ourselves since we use it as an index into the count and index lists.

    // global session average
    private int globalSampleCount;

    private float globalAverageDistance;
    private float globalMedianDistance;
    private double globalM2Distance;
    private double globalSDDistance;

    private float globalAverageGaze;
    private float globalMedianGaze;
    private double globalM2Gaze;
    private double globalSDGaze;

    // data samples
    // not super happy about having to keep a second copy of these for calculation purposes. 
    private List<float> distanceSamples;
    private List<float> gazeSamples;
    private List<int> trialIndexes; // indexes[i] = index in samples[] of first value of i'th trial
                                    // note that this item in smaples[] will be out of range until first sample is recorded.
    private List<int> trialCounts;  // number of samples in the i'th trial

    public override string MeasurementDescriptor => "proxemics";
    public override IEnumerable<string> CustomHeader => new string[]
        {
            "system time",
            "distance",
            "gaze"
        };

    // initialize variables
    void Start()
    {
        // collect a local reference to the agent from the AFManager
        agent = AFManager.Instance.agent;
        if (null == agent) Debug.LogError("agent null in ProxemicsTracker!");
        HMDFieldOfView = AFManager.Instance.studyController.HMDFieldOfView;
        gazeTargetLayerMask = AFManager.Instance.studyController.gazeTargetLayerMask;

        // init the data lists
        distanceSamples = new List<float>();
        gazeSamples = new List<float>();
        trialIndexes = new List<int>();
        trialCounts = new List<int>();

        // zero the counts and values
        // data
        distance = 0.0f;
        gaze = 0.0f;
        
        // interval
        intervalSampleCountDistance = 0;
        lastIntervalSampleCountDistance = 0;
        intervalSampleCountGaze = 0;
        lastIntervalSampleCountGaze = 0;
        runningIntervalTotalDistance = 0.0f;
        runningIntervalTotalGaze = 0.0f;
        averageIntervalDistance = 0.0f;
        averageIntervalGaze = 0.0f;
        prevAverageInternvalTimeStamp = 0.0f;

        // trial
        currentTrial = -1; // this allows us to inc on init to zero
        // we will initialize the trial just before a new trial start

        // global
        globalSampleCount = 0;
        globalAverageDistance = 0.0f;
        globalMedianDistance = 0.0f;
        globalM2Distance = 0.0;
        globalSDDistance = 0.0;
        globalAverageGaze = 0.0f;
        globalMedianGaze = 0.0f;
        globalM2Gaze = 0.0;
        globalSDGaze = 0.0;
    }

    public void initNextTrial(Trial trial)
	{
        // set the current trial index
        currentTrial++;
        
        // collect the time stamp
        trialStartTimeStamp = Time.realtimeSinceStartup;

        // store the index and zero the count of the start of this trial data
        trialIndexes.Add(distanceSamples.Count); 
        trialCounts.Add(0); // zero the counter

        Debug.Log("Tracker: Trial[" + currentTrial + "]: start index " + trialIndexes[currentTrial]);

        // zero out the trial stats
        trialAverageDistance = 0.0f;
        trialMedianDistance = 0.0f;
        trialM2Distance = 0.0;
        trialSDDistance = 0.0;
        trialAverageGaze = 0.0f;
        trialMedianGaze = 0.0f;
        trialM2Gaze = 0.0;
        trialSDGaze = 0.0;
    }

    // Compute stats for the current trial that has just ended.
    // write the stats to the UXF trial results
    public void closeCurrentTrial(Trial trial)
	{
        Debug.Log("Tracker: Trial[" + currentTrial + "]: " + trialCounts[currentTrial] + " samples starting at index " + trialIndexes[currentTrial]);

        // print all data for debugging
        //string dataString = "";
        //for(int i = 0; i < distanceSamples.Count; i++)
		//{
        //    dataString += "\n" + "dist: " + distanceSamples[i] + " gaze: " + gazeSamples[i]; 
		//}
        //Debug.Log("Trial[" + currentTrial + "] data: " + dataString);
        
        // calculate the median for the trial data
        trialMedianDistance = median(distanceSamples, trialIndexes[currentTrial], trialCounts[currentTrial], false);
        trialMedianGaze = median(gazeSamples, trialIndexes[currentTrial], trialCounts[currentTrial], false);

        // print the stats for debugging
        string statsString = "";
        statsString += "\n\t" + "trial average distance = " + trialAverageDistance;
        statsString += "\n\t" + "trial median distance = " + trialMedianDistance;
        statsString += "\n\t" + "trial standard deviation distance = " + trialSDDistance;
        statsString += "\n\t" + "trial average gaze = " + trialAverageGaze;
        statsString += "\n\t" + "trial median gaze = " + trialMedianGaze;
        statsString += "\n\t" + "trial standard deviation gaze = " + trialSDGaze;
        Debug.Log("Tracker: Trial[" + currentTrial + "] stats: " + statsString);
        
        // write out trial stats
        trial.result["trial average distance"] = trialAverageDistance;
        trial.result["trial median distance"] = trialMedianDistance;
        trial.result["trial standard deviation distance"] = trialSDDistance;
        
        trial.result["trial average gaze"] = trialAverageGaze;
        trial.result["trial median gaze"] = trialMedianGaze;
        trial.result["trial standard deviation gaze"] = trialSDGaze;
    }

    // sort of extension method to find the number of trials in a session over all blocks
    public static int TrialsInSession(Session session)
	{
        int count = 0;
        List<Block> blocks = session.blocks;
        foreach (Block block in blocks)
		{
            count += block.trials.Count;
		}
        return count;
	}

    // Compute global stats
    // write the stats to the UXF session results
    // *** WARNING: this will sort the data arrays, so make sure you are done with them.
    public void closeSession(Session session)
	{
        Debug.Log("Tracker: Session: " + globalSampleCount + " total samples in " + TrialsInSession(session) + " trials");

        // calculate the global medians.
        // *** WARNING ***
        // Note that this will sort the data arrays, so make sure you are done with them.
        globalMedianDistance = median(distanceSamples, 0, distanceSamples.Count, true);
        globalMedianGaze = median(gazeSamples, 0, gazeSamples.Count, true);

        // print the stats for debugging
        string statsString = "";
        statsString += "\n\t" + "global average distance = " + globalAverageDistance;
        statsString += "\n\t" + "global median distance = " + globalMedianDistance;
        statsString += "\n\t" + "global standard deviation distance = " + globalSDDistance;
        statsString += "\n\t" + "global average gaze = " + globalAverageGaze;
        statsString += "\n\t" + "global median gaze = " + globalMedianGaze;
        statsString += "\n\t" + "global standard deviation gaze = " + globalSDGaze;
        Debug.Log("Session stats: " + statsString);

        // write out the global stats
        // we need to add these stats to all trials 
        foreach (Trial trial in session.Trials)
        {
            trial.result["global average distance"] = globalAverageDistance;
            trial.result["global median distance"] = globalMedianDistance;
            trial.result["global standard deviation distance"] = globalSDDistance;

            trial.result["global average gaze"] = globalAverageGaze;
            trial.result["global median gaze"] = globalMedianGaze;
            trial.result["global standard deviation gaze"] = globalSDGaze;
        }
    }

    // Calculate the average of recorded data since the last average was calcualted.
    // This function will reset the average counters and running totals.
    private void CalculateAverageValues()
    {
        float elapsedTime = Time.realtimeSinceStartup - prevAverageInternvalTimeStamp;
        // update the time stamp at the end of the calculations...

        if (intervalSampleCountDistance != 0)
        {
            averageIntervalDistance = runningIntervalTotalDistance / (float)intervalSampleCountDistance;

            string logstring = "Average Distance: " + averageIntervalDistance + " with " + intervalSampleCountDistance + " samples in " + elapsedTime + " sec.";
            Debug.Log(ColorString.Colorize(logstring, "#55d99f"));
            
            runningIntervalTotalDistance = 0.0f;
            lastIntervalSampleCountDistance = intervalSampleCountDistance;
            intervalSampleCountDistance = 0;
        }
        if (intervalSampleCountGaze != 0)
        {
            averageIntervalGaze = runningIntervalTotalGaze / (float)intervalSampleCountGaze;

            string logstring = "Average Gaze: " + averageIntervalGaze + " with " + intervalSampleCountGaze + " samples in " + elapsedTime + " sec.";
            Debug.Log(ColorString.Colorize(logstring, "#55d99f"));

            runningIntervalTotalGaze = 0.0f;
            lastIntervalSampleCountGaze = intervalSampleCountGaze;
            intervalSampleCountGaze = 0;
        }

        // now update...
        prevAverageInternvalTimeStamp = Time.realtimeSinceStartup;
    }

    // calculate the average data and record it to the trial object
    public UXFDataRow GetAverageValues()
    {
        CalculateAverageValues();

        var values = new UXFDataRow()
        {
            ("system time", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff")),
            ("average distance", averageIntervalDistance),
            ("distance samples", lastIntervalSampleCountDistance),
            ("average gaze", averageIntervalGaze),
            ("gaze samples", lastIntervalSampleCountGaze)
        };
        return values;
    }

    // return one row of values to be written to file by the UXF tracking system.
    protected override UXFDataRow GetCurrentValues()
    {
        // update data values
        distance = AgentSubjectDistance();
        distanceSamples.Add(distance);
        gaze = GazeScore();
        gazeSamples.Add(gaze);

        //Debug.Log("Data: " + distance + " " + gaze);

        // update intervals
        intervalSampleCountDistance++;
        runningIntervalTotalDistance += distance;
        intervalSampleCountGaze++;
        runningIntervalTotalGaze += gaze;

        // update trial
        updateTrialStats(distance, gaze);

        // update global
        updateGlobalStats(distance, gaze);

        // store data row

        //Debug.Log("Writing data row...\n" +
        //   "distance: " + distance + " " + "gaze: " + gaze
        //  );

        var values = new UXFDataRow()
        {
            ("system time", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff")),
            ("distance", distance),
            ("gaze", gaze)
        };

        // notify that a measurement was taken
        OnTakeMeasurement?.Invoke(this);

        return values;
    }

    /*
    // update the global average
    // using Scott's algo from https://stackoverflow.com/questions/28820904/how-to-efficiently-compute-average-on-the-fly-moving-average
    private void updateGlobalAverage(float distance, float gaze)
	{
        globalSampleCount++;
        float a = 1.0f / globalSampleCount;
        float b = 1.0f - a;

        // distance
        globalAverageDistance = (a * distance) + (b * globalAverageDistance);

        // gaze
        globalAverageGaze = (a * gaze) + (b * globalAverageGaze);
    }
    */

    private void updateTrialStats(float distance, float gaze)
	{
        // updateStats needs to use the old count value before the new data is included,
        // so we'll inc the count at the end.
        
        updateStats(
            distance, 
            trialCounts[currentTrial], 
            ref trialAverageDistance, 
            ref trialM2Distance, 
            ref trialSDDistance);
        //Debug.Log("Trial Distance Update: " + "av: " + trialAverageDistance + "m2: " + trialM2Distance + "sd: " + trialSDDistance);
        updateStats(
            gaze, 
            trialCounts[currentTrial], 
            ref trialAverageGaze, 
            ref trialM2Gaze, 
            ref trialSDGaze);
        //Debug.Log("Trial Gaze Update: " + "av: " + trialAverageGaze + "m2: " + trialM2Gaze + "sd: " + trialSDGaze);

        // count the new sample
        trialCounts[currentTrial]++;
    }
    private void updateGlobalStats(float distance, float gaze)
	{
        // updateStats need to use the old count value before the new data is included,
        // so we'll inc the count at the end.

        updateStats(
            distance,
            globalSampleCount, 
            ref globalAverageDistance,
            ref globalM2Distance, 
            ref globalSDDistance);
        //Debug.Log("Global Distance Update: " + "av: " + globalAverageDistance + "m2: " + globalM2Distance + "sd: " + globalSDDistance);
        updateStats(
            gaze,
            globalSampleCount, 
            ref globalAverageGaze, 
            ref globalM2Gaze, 
            ref globalSDGaze);
        //Debug.Log("Global Gaze Update: " + "av: " + globalAverageGaze + "m2: " + globalM2Gaze + "sd: " + globalSDGaze);

        // count the new sample
        globalSampleCount++;
    }

    // update the given stat values
    private static void updateStats(float new_data, int sample_count, ref float average, ref double m2, ref double sd)
	{
        double new_average = updateAverage(new_data, average, sample_count);
        double new_m2 = updateM2(new_data, m2, average, new_average);

        sample_count++;
        average = (float)new_average;
        m2 = new_m2;
        sd = currentSDfromM2(m2, sample_count);
    }


    // new_sample : new data point to add to the average
    // prev_average : the average of all previous data points
    // total_sampels : number of samples in prev_average (i.e. does not include the new data point)
    private static double updateAverage(double new_sample, double prev_average, int total_samples)
	{
        total_samples++;
        double a = 1.0f / total_samples;
        double b = 1.0f - a;
        return  (a * new_sample) + (b * prev_average);
    }

    // update the sum of squares of differences from the current mean (M2)
    // see: Welford's online algorithm for variance
    private static double updateM2(double new_sample, double prev_m2, double prev_average, double new_average)
	{
        // find new SSDM
        double prev_delta = new_sample - prev_average;
        double new_delta = new_sample - new_average;
        return prev_m2 + (prev_delta * new_delta);
	}

    // calculate variance and sd from the sum of squares of differences from the current mean (M2)
    private static double currentSDfromM2(double m2, int total_samples)
	{
        double variance = m2 / total_samples;
        double sd = Math.Sqrt(variance);
        return sd;
    }

    // Find the median of a list of float data. 
    // if in_place == false
    //      the data will be shallow copied to a new list, sorted, and median value returned. 
    //      the original list is not changed, but note this will incur a O(n) copy, O(nlogn) sort
    // if in_place == true
    //      the data will be sorted in place, changing the order of the original list.
    //      this will avoid the memory and processing cost of the copy.
    private float median(List<float> dataset, int start_index, int count, bool in_place = false)
	{
        if (count <= 0) throw new IndexOutOfRangeException("count is <= 0");
        if (start_index < 0) throw new IndexOutOfRangeException("start_index is < 0");

        int median_index = count / 2; // note int division here : a list of 7 items will return (7 / 2) = 3, index of the 4th item

        // should we make a copy?
        List<float> data;
        if (in_place) 
            data = dataset;
        else
            data = dataset.GetRange(start_index, count);

        // now sort it!! Median value will be the index in the middle (or average if even)
        data.Sort();
        if((count % 2) == 0)   // count is even
		{
            return (data[median_index] + data[median_index - 1]) * 0.5f; // (8 / 2) = 4, the index of 5th item, so average 4th and 5th
        }
        else
		{
            return data[median_index];
        }
	}

    // calcualte the distance between agent and subject
    public float AgentSubjectDistance()
    {
        Vector3 agentLocation = agent.gazeTarget.transform.position;
        Vector3 subjectLocation = transform.position;

        return Vector3.Distance(agentLocation, subjectLocation);
    }

    // The degree to which the agent falls within the subject's field of view.
    // 1.0 means looking right at the agent.
    // 0.0 means out of the field of view.
    public float GazeScore()
    {
        // find the vector from subject to agent
        Vector3 agentLocation = agent.gazeTarget.transform.position;
        Vector3 subjectLocation = transform.position;
        Vector3 directionVector = agentLocation - subjectLocation;
        directionVector.Normalize();

        //Debug.DrawLine(agentLocation, subjectLocation, Color.red, 0.0f, false);

        // find the forward sight line vector of the subject
        // assuming this is a camera, in unity this will be the subject's positive Z vector.
        Vector3 subjectForwardVector = transform.forward;

        //Debug.DrawRay(subjectLocation, subjectForwardVector * 10.0f, Color.blue, 0.0f, false);

        raycastGaze();

        // find the angle (degrees) between these 2 vectors
        float angleBetween = Vector3.Angle(subjectForwardVector, directionVector);

        // find proportion within field of view.
        float halfFOV = (float)HMDFieldOfView / 2.0f;
        if (angleBetween >= (halfFOV))
            return 0.0f;
        else
            return (1.0f - (angleBetween / halfFOV));
    }

    // Draw a ray indicating the gaze raycast
    private void raycastGaze()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, gazeTargetLayerMask, QueryTriggerInteraction.Collide))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow, 2.0f, true);
            OnTrackerRaycast?.Invoke(true);
            //Debug.Log("Raycast Hit ***** !!");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 10.0f, Color.white, 2.0f, true);
            OnTrackerRaycast?.Invoke(false);
            //Debug.Log("Raycast Miss");
        }
    }
}
