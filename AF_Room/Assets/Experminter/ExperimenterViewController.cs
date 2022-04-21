using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperimenterViewController : MonoBehaviour
{
    public FPSMovementController fps { get; private set; }

    private Text FPSIndicator;

    // Start is called before the first frame update
    void Start()
    {
        fps = GetComponent<FPSMovementController>();
        // disable the FPS on startup
        fps.enabled = false;

        FPSIndicator = GetComponentInChildren<Text>();
        FPSIndicator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // toggle on and off the FPS with key
        if(UnityEngine.InputSystem.Keyboard.current.equalsKey.wasPressedThisFrame || UnityEngine.InputSystem.Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            bool fpsToggledState = !fps.enabled;
            Debug.Log("FPS Controls: " + (fpsToggledState ? "on" : "off"));
            FPSIndicator.enabled = fpsToggledState;
            fps.enabled = fpsToggledState;
        }
    }
}
