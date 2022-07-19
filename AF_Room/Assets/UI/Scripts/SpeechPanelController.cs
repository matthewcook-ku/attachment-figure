using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

// Controller for the Speech UI
//
// This is the pannel where all speech activity is controlled by the experimenter. This includes both stock phrases, and also the free chat inout area and history.
//
// Connections:
// ExperimenterUI - SpeechPanel
//
// InputSystem:
// 'ENTER' - sends chat data when focus is on chat field.

public class SpeechPanelController : MonoBehaviour
{   
    // collection of stock phrase buttons
    // using this array means buttons can be added easily in the inspector.
    // the system will use the button's text component as the phrase to say.
    public Button[] phraseButtons;
    public event Action<string> OnPhraseButtonClick;

    public TMP_InputField chatInputField;
    public ChatHistoryScollViewController chatHistory;
    public event Action<string> OnSendChat;

    public TextSpeaker Speaker;

    private void Start()
    {
        if (null == chatInputField) Debug.LogError("Chat Input Field missing on Speech Panel Controller!");
        if (null == chatHistory) Debug.LogError("Chat History missing on Speech Panel Controller!");

        foreach (Button b in phraseButtons)
        {
            b.onClick.AddListener(delegate { phraseButtonPressed(b); });
        }

    }

    public void phraseButtonPressed(Button sender)
    {
        string message = sender.GetComponentInChildren<TMP_Text>().text;
        
        speak(sender.GetComponentInChildren<TMP_Text>().text);
        OnPhraseButtonClick?.Invoke(message);
    }

    // called when the text in the chat field changes. 
    public void chatTextFieldUpdated()
    {
        //Debug.Log("Text Updated: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    // called when the user presses ENTER, or when the send button is pressed.
    // this callback also fires when focus is lost, so check for that.
    public void chatTextFieldEndEdit()
    {
        //Debug.Log("Text Update Ended: " + System.Reflection.MethodBase.GetCurrentMethod().Name);

        // Detect if the user pressed enter, or if the field just lost focus.
        if (UnityEngine.InputSystem.Keyboard.current.enterKey.wasPressedThisFrame || UnityEngine.InputSystem.Keyboard.current.numpadEnterKey.wasPressedThisFrame)
        {
            if(chatInputField.text != string.Empty)
            {
                sendChatText();
            }
        }
    }

    public void chatSendButtonPressed()
    {
        Debug.Log("Button Pressed: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        sendChatText();
    }

    void sendChatText()
    {
        // Debug.Log("Now we are in sendChatText(), and " + chatInputField.text);
        // Debug.Log(chatInputField);
        string chatText = chatInputField.text;

        // clear the chat box
        chatInputField.text = "";

        // add the text to the history box
        chatHistory.text += "\n" + System.DateTime.Now.ToString("[hh:mm:ss]: ") + chatText;

        // on Enter, the fieled will be deactivated. so turn it back on
        chatInputField.ActivateInputField();

        // send the text to the TTS system
        speak(chatText);

        OnSendChat?.Invoke(chatText);
    }

    // send the given text to the TTS system
    void speak(string text)
    {
        Debug.Log("SPEAK: " + text);

        // readspeaker
        Speaker.Say(text);
    }
}
