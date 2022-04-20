using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechPanelController : MonoBehaviour
{
    public Button[] phraseButtons;

    public InputField chatInputField;
    public ChatHistoryScollViewController chatHistory;

    public GameObject TTSProvider;

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
        speak(sender.GetComponentInChildren<Text>().text);
    }

    public void chatTextFieldUpdated()
    {
        Debug.Log("Text Updated: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    public void chatTextFieldEndEdit()
    {
        Debug.Log("Text Update Ended: " + System.Reflection.MethodBase.GetCurrentMethod().Name);

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
        string chatText = chatInputField.text;

        // clear the chat box
        chatInputField.text = "";

        // add the text to the history box
        chatHistory.text += "\n" + System.DateTime.Now.ToString("[hh:mm:ss]: ") + chatText;

        // on Enter, the filed will be deactivated. so turn it back on
        chatInputField.ActivateInputField();

        // send the text to the TTS system
        speak(chatText);
    }

    // send the given text to the TTS system
    void speak(string text)
    {
        Debug.Log("SPEAK: " + text);

        // readspeaker
        TextSpeaker tts = TTSProvider.GetComponent<TextSpeaker>();
        tts.Say(text);
    }
}
