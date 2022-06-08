using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;


// Observer Pattern
// https://www.youtube.com/watch?v=UWMmib1RYFE
// Unity Input System
// https://www.youtube.com/watch?v=YHC-6I_LSos
// Changing Action Maps
// https://onewheelstudio.com/blog/2021/6/27/changing-action-maps-with-unitys-new-input-system
//
// Remember to add any mapping you add to the InputControls to the OnEnable list of mappings to enable.

public class InputManager : MonoBehaviour
{
    // Static input controls for others to access
    public InputControls InputActions;

    // Delegate for map changes
    // subscribe with actionMapChange += MyFunc in OnEnable, and -= in OnDisable
    public static event Action<InputActionMap> ActionMapChange;


    private void Awake()
    {
        InputActions = new InputControls();
    }

    private void OnEnable()
    {
        ToggleActionMap(InputActions.TestingActions);
        ToggleActionMap(InputActions.AgentControls);
        ToggleActionMap(InputActions.ExperimenterControls);
    }

    private void OnDisable()
    {
        // Disable all actions in all mappings 
        InputActions.Disable();
    }



    // toggle the given action map on or off
    public static void ToggleActionMap(InputActionMap actionMap)
    {
        ActionMapChange?.Invoke(actionMap); // raise an event to alert others of change
        if (actionMap.enabled)
        {
            actionMap.Disable();
        }
        else
        {
            actionMap.Enable();
        }
    }
}
