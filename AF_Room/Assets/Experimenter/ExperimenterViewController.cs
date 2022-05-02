using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Overall controller for items in the experimenter's desktop view.
//
// This is the place to coordinate anything that would be displayed to the experimenter before, while, or after the session.
// Note this class uses the new unity InputSystem
//
// Major Parts:
// - a simple FPS style controller to allow movement around the room for viewing.
// - a small display text to let you know FPS is on.
//
// InputSystem:
// '=' - toggle FPS on or off.

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
