using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UXF;

public class StudySettingsUIController : MonoBehaviour
{
	public Button BeginExperimentButton;
	public TMP_Text InfoText;
	string defaultInfoText;
	public TMP_InputField NameInputField;
	public TMP_InputField IDInputField;
	public Button IDRandomButton;
	string subject_name;
	string id;
	string expName = "Experiment 1";
	string ppid { get { return id + "_" + NameInputField.text; } }
	 
	public TMP_InputField SessionNumberInputField;
	public Button SessionNumberIncButton;
	int sessionNumber;
	 
	public TMP_InputField FilePathInputField;
	public Button FileBrowseButton;
	public LocalFileDataHander FileDataHandler;
	string filepath;

	public Camera ModelSettingsRenderTextureCamera;
	public EventToggleGroup ModelToggleGroup;
	public EventToggleGroup SkintoneToggleGroup;

	public OverwriteWarningPanelController OverwriteWarning;

	// Start is called before the first frame update
	void Start()
	{
		// make sure the model camera is on
		ModelSettingsRenderTextureCamera.gameObject.SetActive(true);

		// set some default values
		subject_name = "";
		NameInputField.text = "";
		id = "";
		IDInputField.text = "";
		sessionNumber = 1;
		SessionNumberInputField.text = sessionNumber.ToString();
		filepath = FilePathInputField.text;	// filled in from user prefs, so grab it
		defaultInfoText = InfoText.text;

		// update UXF data
		Session.instance.name = subject_name;
		Session.instance.ppid = ppid;
		Session.instance.number = sessionNumber;
		FileDataHandler.storagePath = FilePathInputField.text; // filled in from user prefs, so grab it

		// make sure all error indicators are off
		ClearAllUIElementErrors();
	}

	private void OnDisable()
	{
		// turn off the model camera
		// if the program quits, it might be that the camera is destroyed before this object, hence the check. 
		if(ModelSettingsRenderTextureCamera != null)
			ModelSettingsRenderTextureCamera.gameObject.SetActive(false);
	}

	public void OnEndEditName()
	{
		subject_name = NameInputField.text;
		Session.instance.name = subject_name;
		Session.instance.ppid = ppid; // uses name
	}
	public void OnEndEditID()
	{
		id = IDInputField.text;
		Session.instance.ppid = ppid; // uses ID
	}
	void generateUniqueID()
	{
		// create a number from the current date and time
		// this way the number will always be unique
		id = System.DateTime.Now.ToString("yyMMddHHmmss");   // yyMMddHHmmssfff
		IDInputField.text = id;
		Session.instance.ppid = ppid; // uses ID
		Debug.Log("StudySettings: setting ID number to generated value: " + id);
	}
	public void OnIDRandomButtonPressed()
	{
		generateUniqueID();
	}
	public bool ParseSessionNumber()
	{
		try
		{
			sessionNumber = System.Convert.ToInt32(SessionNumberInputField.text);
			Session.instance.number = sessionNumber;
			Debug.Log("StudySettings: setting session number to: " + sessionNumber);
		}
		catch (System.FormatException)
		{
			System.Console.WriteLine("Input string is not a sequence of digits.");
			return false;
		}
		return true;
	}
	public void OnEndEditSessionNumber()
	{
		// if we couldnt parse the input as an int, set it back to previous value
		if(!ParseSessionNumber())
		{
			Debug.Log("StudySettings: Session number input not valid, setting back to previous value.");
			SessionNumberInputField.text = sessionNumber.ToString();
		}
	}
	public void OnSessionNumberIncButtonPressed()
	{
		Debug.Log("StudySettings: incrementing session number");
		sessionNumber++;
		SessionNumberInputField.text = sessionNumber.ToString();
		Session.instance.number = sessionNumber;
	}
	private void SetModelFromCurrentSelection()
	{
		int index = ModelToggleGroup.getActiveIndex();
		Debug.Log("StudySettings: Selecting model: " + index);
		AFManager.Instance.agent.setActiveSkin(index);

		// also need to set the skintone of the new model to the current selection
		SetSkintoneFromCurrentSelection();
	}
	public void OnModelSelectionChanged(Toggle selected)
	{
		SetModelFromCurrentSelection();
	}
	private void SetSkintoneFromCurrentSelection()
	{
		int index = SkintoneToggleGroup.getActiveIndex();
		Debug.Log("StudySettings: Selecting skintone: " + index);
		AFManager.Instance.agent.activeSkin.SetSkintone(index);
	}
	public void OnSkintoneSelectionChanged(Toggle selected)
	{
		SetSkintoneFromCurrentSelection();
	}
	public void OnExitInfoTooltipSpace()
	{
		InfoText.text = defaultInfoText;
	}
	public void OnBeginExperimentButtonPressed()
	{
		if (!CheckFields()) 
			return; // if there is a field with an error, then stop

		// Create the session
		// this requires the file path to be set before calling.
		Block attachmentFigureBlock = Session.instance.CreateBlock(1);
		Session.instance.Begin(expName, Session.instance.ppid, Session.instance.number);
		// that will trigger the OnSessionBegin event from UXF

		StoreSettingsFromFields();

		// close the startUI
		gameObject.SetActive(false);
	}

	private bool CheckFields()
	{
		string errorMessage;
		if (string.IsNullOrWhiteSpace(NameInputField.text))
		{
			errorMessage = "ERROR: Name field cannot be blank!!";
			MarkUIElementError(NameInputField.gameObject, errorMessage);
			return false;
		}
		if (string.IsNullOrWhiteSpace(IDInputField.text))
		{
			errorMessage = "ERROR: ID field cannot be blank!!";
			MarkUIElementError(IDInputField.gameObject, errorMessage);
			return false;
		}
		if (string.IsNullOrWhiteSpace(SessionNumberInputField.text))
		{
			errorMessage = "ERROR: Session number field cannot be blank!!";
			MarkUIElementError(SessionNumberInputField.gameObject, errorMessage);
			return false;
		}
		if (string.IsNullOrWhiteSpace(FilePathInputField.text))
		{
			errorMessage = "ERROR: File path field cannot be blank!!";
			MarkUIElementError(FilePathInputField.gameObject, errorMessage);
			return false;
		}

		return true;

		// check if this session exists, and we might overwrite data
		// borrowed from example code and needs to be rewritten
		/*bool exists = session.CheckSessionExists(
				localFilePath,
				newExperimentName,
				newPpid,
				sessionNum
			);

		if (exists)
		{
			Popup newPopup = new Popup()
			{
				message = string.Format(
					"{0} - {1} - Session #{2} already exists! Press OK to start the session anyway, data may be overwritten.",
					newExperimentName,
					newPpid,
					sessionNum
				),
				messageType = MessageType.Warning,
				onOK = () => {
					gameObject.SetActive(false);
					// BEGIN!
					session.Begin(
						newExperimentName,
						newPpid,
						sessionNum,
						newParticipantDetails,
						newSettings
					);
				}
			};
			popupController.DisplayPopup(newPopup);
		}*/

		/* // working on mine
		// check if there is already data at this save location
		bool exists = Session.instance.CheckSessionExists(
				filepath,
				expName,
				ppid,
				sessionNumber
			);

		if (exists)
		{
			OverwriteWarning.OpenPanel();
			OverwriteWarning.OnUserSelection += OverwriteOK;
			return false;
		}*/
	}

	void OverwriteOK(bool userSelection)
	{
		Debug.Log("User said: " + userSelection);
	}

	private void StoreSettingsFromFields()
	{ 
		// Store settings to the UXF object
		Session.instance.settings.SetValue("SubjectName", NameInputField.text);
		Session.instance.settings.SetValue("ID", IDInputField.text);
		Session.instance.settings.SetValue("Session", SessionNumberInputField.text);
		Session.instance.settings.SetValue("FilePath", FilePathInputField.text);

		AFManager.Instance.agent.Speaker.StoreSettingsFromFields();

		Session.instance.settings.SetValue("Model", ModelToggleGroup.getActiveIndex());
		Session.instance.settings.SetValue("Skintone", SkintoneToggleGroup.getActiveIndex());
	}
	void MarkUIElementError(GameObject uiElement, string errorMessage)
	{
		Outline errorOutline = uiElement.GetComponent<Outline>();
		errorOutline.enabled = true;

		InfoText.text = "<color=\"red\">" + errorMessage + "</color>";
	}
	void ClearUIElementError(GameObject uiElement)
	{
		Outline errorOutline = uiElement.GetComponent<Outline>();
		errorOutline.enabled = false;

		InfoText.text = defaultInfoText;
	}
	void ClearAllUIElementErrors()
	{
		ClearUIElementError(NameInputField.gameObject);
		ClearUIElementError(IDInputField.gameObject);
		ClearUIElementError(SessionNumberInputField.gameObject);
		ClearUIElementError(FilePathInputField.gameObject);
	}
}
