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
	string id;
	 
	public TMP_InputField SessionNumberInputField;
	public Button SessionNumberIncButton;
	int sessionNumber = 1;
	 
	public TMP_InputField FilePathInputField;
	public Button FileBrowseButton;


	public Camera ModelSettingsRenderTextureCamera;
	public EventToggleGroup ModelToggleGroup;
	public EventToggleGroup SkintoneToggleGroup;

	// Start is called before the first frame update
	void Start()
	{
		// make sure the model camera is on
		ModelSettingsRenderTextureCamera.gameObject.SetActive(true);

		// set some default values
		NameInputField.text = "";
		IDInputField.text = "";
		SessionNumberInputField.text = sessionNumber.ToString();
		defaultInfoText = InfoText.text;


		// make sure all error indicators are off
		ClearAllUIElementErrors();
	}

	private void OnDisable()
	{
		// turn off the model camera
		ModelSettingsRenderTextureCamera.gameObject.SetActive(false);
	}

	void generateUniqueID()
	{
		// create a number from the current date and time
		// this way the number will always be unique
		id = System.DateTime.Now.ToString("yyMMddHHmmssfff");
		Debug.Log("StudySettings: setting ID number to generated value: " + id);
	}
	public void OnIDRandomButtonPressed()
	{
		generateUniqueID();
		IDInputField.text = id;
	}
	public bool ParseSessionNumber()
	{
		try
		{
			sessionNumber = System.Convert.ToInt32(SessionNumberInputField.text);
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
	}
	public void OnModelSelectionChanged(Toggle selected)
	{
		int modelIndex = ModelToggleGroup.getActiveIndex();
		Debug.Log("StudySettings: Selecting model: " + modelIndex);
		AFManager.Instance.agent.setActiveSkin(modelIndex);
	}
	public void OnSkintoneSelectionChanged(Toggle selected)
	{
		int skintoneIndex = SkintoneToggleGroup.getActiveIndex();
		Debug.Log("StudySettings: Selecting skintone: " + skintoneIndex);
		AFManager.Instance.agent.activeSkin.SetSkintone(skintoneIndex);
	}
	public void OnExitInfoTooltipSpace()
	{
		InfoText.text = defaultInfoText;
	}
	public void OnBeginExperimentButtonPressed()
	{
		if (!CheckFields()) 
			return; // if there is a field with an error, then stop

		// Experiment start
		Block attachmentFigureBlock = Session.instance.CreateBlock(1);
		Session.instance.Begin("Experiment 1", NameInputField.text, 1);

		StoreSettingsFromFields();

		// close the UI
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
	}
	private void StoreSettingsFromFields()
	{ 
		// Store settings to the UXF object
		Session.instance.settings.SetValue("SubjectName", NameInputField.text);
		Session.instance.settings.SetValue("ID", IDInputField.text);
		Session.instance.settings.SetValue("Session", SessionNumberInputField.text);
		Session.instance.settings.SetValue("FilePath", FilePathInputField.text);
		//Session.instance.settings.SetValue("Voice", voice_ind);
		//Session.instance.settings.SetValue("Voice_pitch", voice_pitch);
		//Session.instance.settings.SetValue("Voice_vol", voice_vol);
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
