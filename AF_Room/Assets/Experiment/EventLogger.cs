using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;


// Use the UXF system to log any important events, along with their timestamps.
// this file will end up in the /other data folder

public class EventLogger : MonoBehaviour
{
    string[] headers; 
    UXFDataTable log;

    public enum EventType
	{
        Expression,
        Gesture,
        Attention,
        StockSpeech,
        CustomSpeech
	}

    // connected objects for logging
    public AnimationPanelController animationPanel;
    public SpeechPanelController speechPanel;

    
    // Start is called before the first frame update
    void Start()
    {
        headers = new string[] { "time", "system time", "event type", "message" };
        log = new UXFDataTable(headers);
    }

	private void OnEnable()
	{
        Debug.Log("animationPanel = " + animationPanel);
        Debug.Log("speechPanel = " + speechPanel);

        animationPanel.OnExpressionButtonClick += OnExpressionEvent;
        animationPanel.OnGestureButtonClick += OnGestureEvent;
        animationPanel.OnAttentionChange += OnAttentionEvent;
        speechPanel.OnPhraseButtonClick += OnStockSpeechEvent;
        speechPanel.OnSendChat += OnCustomSpeechEvent;
    }

	private void OnDisable()
	{
        // un-register for events
        animationPanel.OnExpressionButtonClick -= OnExpressionEvent;
        animationPanel.OnGestureButtonClick -= OnGestureEvent;
        animationPanel.OnAttentionChange -= OnAttentionEvent;
        speechPanel.OnPhraseButtonClick -= OnStockSpeechEvent;
        speechPanel.OnSendChat -= OnCustomSpeechEvent;
    }

	public void OutputLogFile()
	{
        Debug.Log("Writing output log file...");
        
        // don't let someone call this function before elements are initilzied.
        if (headers == null) return;
        if (log == null) return;
        if (UXF.Session.instance == null) return;

        UXF.Session.instance.SaveDataTable(log, "EventLog");

        Debug.Log("Log written successfully.");
    }

    string NameForEventType(EventType type)
	{
        switch(type)
		{
            case EventType.Expression:
                return "expression";
            case EventType.Gesture:
                return "gesture";
            case EventType.Attention:
                return "attention";
            case EventType.StockSpeech:
                return "stock speech";
            case EventType.CustomSpeech:
                return "custom speech";
        }
        return "unknown";
	}

    public void Log(string message, EventType type)
	{
        var values = new UXFDataRow()
        {
            ("time", Time.time.ToString()),
            ("system time", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff")),
            ("event type", NameForEventType(type)),
            ("message", message)
        };

        //Debug.Log("Logging Event: " + message);

        log.AddCompleteRow(values);
    }


    void OnStockSpeechEvent(string message)
	{
        Log(message, EventType.StockSpeech);
    }
    void OnCustomSpeechEvent(string message)
    {
        Log(message, EventType.CustomSpeech);
    }
    void OnExpressionEvent(int item)
    {
        Debug.Log("Expression Event Handler.");
        Log(AgentSkin.FaceExpressionToString((AgentSkin.FaceExpression)item), EventType.Expression);
    }
    void OnGestureEvent(int item)
    {
        Log(AgentSkin.BodyActionToString((AgentSkin.BodyAction)item), EventType.Gesture);
    }
    void OnAttentionEvent(bool looking)
    {
        Log("looking: " + looking.ToString(), EventType.Gesture);
    }
}
