using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using SubjectNerd.Utilities;
using UXF;
using UXF.UI;

public class UIStartController : MonoBehaviour
{
    private Canvas canvas;
    private Session session;
    public FormElement localFilePathElement;

    public bool RequiresFilePathElement
    {
        get
        {
            if (session == null) return false;
            return session.ActiveDataHandlers
                .Where((dh) => dh is LocalFileDataHander)
                .Any(dh => ((LocalFileDataHander)dh).dataSaveLocation == DataSaveLocation.AcquireFromUI);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnValidate()
    {
        UpdateLocalFileElementState();
    }



    void UpdateLocalFileElementState()
    {
        if (localFilePathElement != null) localFilePathElement.gameObject.SetActive(RequiresFilePathElement);
    }
}
