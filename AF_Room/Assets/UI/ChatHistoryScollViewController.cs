using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChatHistoryScollViewController : MonoBehaviour
{
    public Scrollbar verticalScrollbar;
    public Scrollbar horizontalScrollbar;

    private Text historyText;
    public string text
    {
        get
        {
            return historyText.text;
        }
        set
        {
            updateTextContent(value);
        }
    }

    private ScrollRect scrollRect;

    public bool scrollToBottom = true;

    // Start is called before the first frame update
    void Start()
    {
        if (null == verticalScrollbar) Debug.LogError("Vertical Scrollbar not connected in Chat History Scroll View Controller!");
        if (null == horizontalScrollbar) Debug.LogError("Hoizontal Scrollbar not connected in Chat History Scroll View Controller!");

        historyText = GetComponentInChildren<Text>();
        if (null == historyText) Debug.LogError("Text object missing in Chat History Scroll View Controller!");

        scrollRect = GetComponentInChildren<ScrollRect>();
        if (null == historyText) Debug.LogError("ScrollRect missing from Chat History Scroll View Controller!");
        scrollRect.GetComponentInParent<RectTransform>().pivot = new Vector2(0.0f, 0.0f); // set y pivot to 0 to scroll to bottom.
    }

    void updateTextContent(string value)
    {
        // set the text
        historyText.text = value;

        // if needed, scroll to bottom
        if(scrollToBottom)
        {
            scrollRect.verticalNormalizedPosition = 0.0f;
        }
    }
}
