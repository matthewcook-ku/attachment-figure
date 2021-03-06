using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHeightPanelController : MonoBehaviour
{
    public Canvas PopUpUICanvas;
    
    public SubjectController Subject;
    public SubscriberCamera SubjectPOVRenderTextureCamera;
    public SubscriberCamera DesktopPOVRenderTextureCamera;

    Slider HeightSlider;
    TMP_InputField HeightInputField;

    [Tooltip("Height of the floor in local space of Subject. Usually 0.0f")]
    public float FloorHeight;
    [Tooltip("Max view height for the Subject above the floor height in unity units. Usually 1 == 1m")]
    public float MaxPlayerHeight;

    void OnDisable()
    {
        // remove self from cameras
        if (SubjectPOVRenderTextureCamera != null) SubjectPOVRenderTextureCamera.Unsubscribe(this);
        if (DesktopPOVRenderTextureCamera != null) DesktopPOVRenderTextureCamera.Unsubscribe(this);
    }

    private void Start()
    {
        // find references to the UI controls we need
        HeightSlider = gameObject.GetComponentInChildren<Slider>();
        HeightInputField = gameObject.GetComponentInChildren<TMP_InputField>();

        // subscribe to the needed cameras, this will turn them on if needed
        if (SubjectPOVRenderTextureCamera != null) SubjectPOVRenderTextureCamera.Subscribe(this);
        if (DesktopPOVRenderTextureCamera != null) DesktopPOVRenderTextureCamera.Subscribe(this);

        // set the slider to match the current height value
        // need to convert the value into the slider range of [0,1].
        HeightSlider.value = Subject.SubjectHeight / (MaxPlayerHeight - FloorHeight);
        HeightInputField.text = Subject.SubjectHeight.ToString("F2"); // 2 sig digits decimal 
    }

    public void OpenPanel()
    {
        // open the popup canvas, and then turn on this panel.
        PopUpUICanvas.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }
    public void ClosePanel()
    {
        // make sure to close the panel AND canvas, as canvas will block other conavas's input.
        gameObject.SetActive(false);
        PopUpUICanvas.gameObject.SetActive(false);
    }

    public void OnSliderValueChanged(float value)
    {
        // convert the [0,1] slider value to a distance in range
        float newHeight = Mathf.Lerp(FloorHeight, MaxPlayerHeight, value);

        //Debug.Log("Updating Height: " + value + " -> " + newHeight);
        HeightInputField.text = newHeight.ToString("F2"); // 2 sig digits decimal 
        Subject.SubjectHeight = newHeight;
    }
    public void OnResetPositionButtonPressed()
    {
        Subject.ResetSubjectPosition();
    }
}
