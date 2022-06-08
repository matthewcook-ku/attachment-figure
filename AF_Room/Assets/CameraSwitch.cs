using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera camera;
    private int view = 2;
    // Start is called before the first frame update
    void Start()
    {
        // camera = transform.root.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void switchView()
    {
        if(view == 2)
        {
            camera.targetDisplay = 2;
        }
        else
        {
            camera.targetDisplay = 1;
        }
    }
}
