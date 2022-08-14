using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class DialogueBoxController : MonoBehaviour
{
	public Canvas PopUpUICanvas;
	public TMP_Text Title;
	public TMP_Text Message;
	public Image AttentionIcon;
	public Image WarningIcon;
	public Image ErrorIcon;
	public Button CancelButton;
	public Button OKButton;

	Action OKAction;

	private static DialogueBoxController DialogueBox; 
	public static DialogueBoxController Instance()
	{
		if(null == DialogueBox)
		{
			DialogueBox = FindObjectOfType<DialogueBoxController>(true);
			if(null == DialogueBox)
				Debug.LogError("There needs to be one active messageBox script on a GameObject in your scene.");
		}
		return DialogueBox;
	}

	public void DisplayPopup(Popup popup)
	{
		Title.text = popup.messageType.ToString(); // ToString on an enum will use the enum label
		Message.text = popup.message;
		OKAction = popup.onOK;

		AttentionIcon.gameObject.SetActive((popup.messageType == MessageType.Attention) ? true : false);
		WarningIcon.gameObject.SetActive((popup.messageType == MessageType.Warning) ? true : false);
		ErrorIcon.gameObject.SetActive((popup.messageType == MessageType.Error) ? true : false);

		OpenPanel();
	}
	public void OpenPanel()
	{
		// open the popup canvas, and then turn on this panel.
		PopUpUICanvas.gameObject.SetActive(true);
		gameObject.SetActive(true);
		transform.SetAsLastSibling();
	}
	public void ClosePanel()
	{
		// make sure to close the panel AND canvas, as canvas will block other canvas's input.
		gameObject.SetActive(false);
		PopUpUICanvas.gameObject.SetActive(false);
	}

	public void OnClickOK()
	{
		ClosePanel();
		OKAction.Invoke();
	}

	public void OnClickCancel()
	{
		ClosePanel();
		gameObject.SetActive(false);
	}

	[ContextMenu("Test popup")]
	public void PopupTest()
	{
		Popup popup = new Popup();
		popup.messageType = MessageType.Attention;
		popup.message = "Testing popup!";
		popup.onOK = new Action(() => { });
		DisplayPopup(popup);
	}

	public struct Popup
    {
        public MessageType messageType;
        public string message;
        public Action onOK;
    }

    public enum MessageType
    {
        Attention, Warning, Error
    }
}
