using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectPOVPanelController : MonoBehaviour
{
    public SubscriberCamera SubjectPOVRenderTextureCamera;
    public GameObject OpenPanelGroup;

    private void Start()
    {
        if(OpenPanelGroup.activeInHierarchy) OpenPanel();
    }

    public void OpenPanel()
    {
        OpenPanelGroup.SetActive(true);
        
        // subscribe to the needed cameras
        if (SubjectPOVRenderTextureCamera != null) SubjectPOVRenderTextureCamera.Subscribe(this);

    }
    public void ClosePanel()
    {
        OpenPanelGroup.SetActive(false);

        // remove self from cameras
        if (SubjectPOVRenderTextureCamera != null) SubjectPOVRenderTextureCamera.Unsubscribe(this);
    }
}
