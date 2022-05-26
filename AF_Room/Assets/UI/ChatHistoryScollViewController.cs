using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

// Controller for the Chat History UI component
//
// Turns a ScrollView into a chat history.
// - text can be appended to the end of the view easily.
// - the view can be set to always scroll to the bottom
//
// This controller should be attached to a ScrollView UI object.

public class ChatHistoryScollViewController : MonoBehaviour
{
    public Scrollbar verticalScrollbar;
    // public Scrollbar horizontalScrollbar;
    private TMP_Text historyText;


    public string text
    {
        get
        {
            return historyText.text;
        }
        set
        {
            Debug.Log("I'm about to set.");
            updateTextContent(value);
        }
    }

    private ScrollRect scrollRect;
    // should the scroll area always show the bottom as new lines are added
    public bool scrollToBottom = true;

    // Start is called before the first frame update
    void Start()
    {
        if (null == verticalScrollbar) Debug.LogError("Vertical Scrollbar not connected in Chat History Scroll View Controller!");
        // if (null == horizontalScrollbar) Debug.LogError("Hoizontal Scrollbar not connected in Chat History Scroll View Controller!");

        historyText = GetComponentInChildren<TMP_Text>();
        Debug.Log("historyText is currently" + historyText);

        scrollRect = GetComponentInChildren<ScrollRect>();
        scrollRect.GetComponentInParent<RectTransform>().pivot = new Vector2(0.0f, 0.0f); // set y pivot to 0 to scroll to bottom.
    }

    void updateTextContent(string value)
    {
        Debug.Log("Do we ever make it into updateTextContent");
        // set the text
        historyText.text = value;

        // if needed, scroll to bottom
        if(scrollToBottom)
        {
            scrollRect.verticalNormalizedPosition = 0.0f;
        }
    }
}
