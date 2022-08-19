using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UXF;
using System;

public class StudySettingsUIController : MonoBehaviour
{
	public Button BeginExperimentButton;
	public TMP_Text InfoText;
	string defaultInfoText;

	public TMP_InputField ExperimenterInputField;

	public TMP_InputField SubjectNameInputField;
	public TMP_InputField IDInputField;
	public Button IDRandomButton;
	string subject_name;
	string id;
	string ppid { get { return id + "_" + SubjectNameInputField.text; } }

	string experiment_name = "Experiment 1";
	 
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

	public TMP_Dropdown TaskDropdown;
	public TMP_Dropdown TaskSetDropdown;


	// Start is called before the first frame update
	void Start()
	{
		// make sure the model camera is on
		ModelSettingsRenderTextureCamera.gameObject.SetActive(true);

		// set some default values
		ExperimenterInputField.text = "";
		subject_name = "";
		SubjectNameInputField.text = "";
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

	// We keep track of when the name or ID fields change because they are used to build the PPID
	// but this is probably not needed for most of the other data
	public void OnEndEditName()
	{
		subject_name = SubjectNameInputField.text;
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

	public void OnTaskSelectionChanged(Int32 selection)
	{
		Debug.Log("Task Selection: " + selection);
	}
	public void OnTaskSetSelectionChanged(Int32 selection)
	{
		Debug.Log("Task Selection: " + selection);
	}

	public void OnBeginExperimentButtonPressed()
	{
		if (!CheckFields()) 
			return; // if there is a field with an error, then stop

		// check if this session exists, and we might overwrite data
		bool exists = Session.instance.CheckSessionExists(
				filepath,
				experiment_name,
				ppid,
				sessionNumber
			);

		if (exists)
		{
			DialogueBoxController.Popup overwritePopup = new DialogueBoxController.Popup()
			{
				message = string.Format(
					"A session results file already exists for\n" +
					"Subject: {0} - ID:{1} \n" +
					"Session #: {2} \n" +
					"at the specified data location! Press OK to start the session anyway, data may be overwritten.",
					subject_name,
					id,
					sessionNumber
				),
				messageType = DialogueBoxController.MessageType.Warning,
				onOK = () => { BeginExperiment(); }
			};
			DialogueBoxController.Instance().DisplayPopup(overwritePopup);
			return;
		}

		// if we got this far, then all is good so go for it!
		BeginExperiment();
	}

	private void BeginExperiment()
	{
		// create a settings object to hold settings from the UI
		// record the UI data into settings
		Settings sessionSettings = new Settings();
		StoreSettingsFromFields(sessionSettings);

		// initialize the session
		// this will trigger the OnSessionBegin event
		Session.instance.Begin(experiment_name, Session.instance.ppid, Session.instance.number, null, sessionSettings);

		// close the startUI
		gameObject.SetActive(false);
	}

	private bool CheckFields()
	{
		string errorMessage;
		if (string.IsNullOrWhiteSpace(ExperimenterInputField.text))
		{
			errorMessage = "ERROR: Experimenter field cannot be blank!!";
			MarkUIElementError(ExperimenterInputField.gameObject, errorMessage);
			return false;
		}
		if (string.IsNullOrWhiteSpace(SubjectNameInputField.text))
		{
			errorMessage = "ERROR: Name field cannot be blank!!";
			MarkUIElementError(SubjectNameInputField.gameObject, errorMessage);
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
	}

	void OverwriteOK(bool userSelection)
	{
		Debug.Log("User said: " + userSelection);
	}

	// Store the settings from the UI into the given settings object
	// note this will overwrite any exisiting values in settings
	private void StoreSettingsFromFields(Settings settings)
	{
		// Store settings to the UXF object
		settings.SetValue("ExperimenterName", ExperimenterInputField.text);
		settings.SetValue("SubjectName", SubjectNameInputField.text);
		settings.SetValue("ID", IDInputField.text);
		settings.SetValue("Session", SessionNumberInputField.text);
		settings.SetValue("FilePath", FilePathInputField.text);

		AFManager.Instance.agent.Speaker.StoreSettingsFromFields(settings);

		settings.SetValue("Model", ModelToggleGroup.getActiveIndex());
		settings.SetValue("Skintone", SkintoneToggleGroup.getActiveIndex());

		settings.SetValue("Task", TaskDropdown.options[TaskDropdown.value].text);
		settings.SetValue("TaskSet", TaskSetDropdown.options[TaskSetDropdown.value].text);
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
		ClearUIElementError(ExperimenterInputField.gameObject);
		ClearUIElementError(SubjectNameInputField.gameObject);
		ClearUIElementError(IDInputField.gameObject);
		ClearUIElementError(SessionNumberInputField.gameObject);
		ClearUIElementError(FilePathInputField.gameObject);
	}
}
