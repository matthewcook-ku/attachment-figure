using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;


// Observer Pattern - delegates, events, actions, and funcs
// https://www.youtube.com/watch?v=UWMmib1RYFE
// Unity Input System
// https://www.youtube.com/watch?v=YHC-6I_LSos
// Changing Action Maps
// https://onewheelstudio.com/blog/2021/6/27/changing-action-maps-with-unitys-new-input-system
//
// Remember to add any mapping you add to the InputControls to the OnEnable list of mappings to enable.

public class InputManager : MonoBehaviour
{
    // input action set for others to access
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
        //ToggleActionMap(InputActions.TestingActions);
        //ToggleActionMap(InputActions.AgentControls);
        ToggleActionMap(InputActions.ExperimenterControls);
    }

    private void OnDisable()
    {
        // Disable all actions in all mappings 
        InputActions.Disable();
    }



    // toggle the given action map on or off
    // raises an event to signal the change to all registered listeners
    public static void ToggleActionMap(InputActionMap actionMap)
    {
        ActionMapChange?.Invoke(actionMap); // raise an event to alert others of change
        Debug.Log("ActionMap: " + actionMap + " => " + !actionMap.enabled);
        if (actionMap.enabled)
        {
            actionMap.Disable();
        }
        else
        {
            actionMap.Enable();
        }
    }
    // turn the mapping on or off
    // raises an event to signal the change to all registered listeners
    // if state is already set, no action is taken and no event is sent
    public static void EnableActionMap(InputActionMap actionMap, bool enable)
    {
        // ignore if already set
        if (actionMap.enabled == enable)
            return;

        ActionMapChange?.Invoke(actionMap); // raise an event to alert others of change
        Debug.Log("ActionMap: " + actionMap + " => " + enable);
        if (enable)
            actionMap.Enable();
        else
            actionMap.Disable();
    }
}
