using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Management and Swapping of Avatar Skins.
 * 
 * Apply this controller to a base GameObject, with each skin as a child. Use this base to apply any global transforms to all skins. Any root animations will be applied to skins individually.
 * Add each child skin's root GameObject to the AgentSkins list in the editor.
 * 
 * AgentSkins - list of skins (See below)
 * SpawnLocation - new skins will spawn at the position and rotation of this game object.
 * 
 * Skins should be of the form:
 *  root
 *      mesh component - visual components.
 *      mesh component
 *      mesh component
 *      ...
 *      armature - main Humanioid compatablr armature
 *          hips
 *          ...
 *      additinal rigs (AnimationRigging package, optional)
 */

public class AgentController : MonoBehaviour
{
    // skins
    private int activeSkinIndex = 0;
    public List<AgentSkin> AgentSkins;
    public AgentSkin activeSkin
    {
        get
        {
            return AgentSkins[activeSkinIndex];
        }
    }

    // gaze target
    public GameObject gazeTarget 
    { 
        get 
        {
            return activeSkin.gazeTarget;
        } 
    }

    // spawn point
    public GameObject spawnLocation = null;
    public bool updateSpawnLocation = false;
    private Vector3 spawnPosition = Vector3.zero;
    private Quaternion spawnRotation = Quaternion.identity;

    private void OnEnable()
    {
        InputControls.AgentControlsActions controls = AFManager.Instance.InputManager.InputActions.AgentControls;
        controls.SwapSkins.performed += OnSwapSkins;
    }

    private void OnDisable()
    {
        InputControls.AgentControlsActions controls = AFManager.Instance.InputManager.InputActions.AgentControls;
        controls.SwapSkins.performed -= OnSwapSkins;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(AgentSkins.Count == 0)
        {
            throw new System.Exception("No skins specified for agent.");
        }

        // save the spawn position
        if (spawnLocation != null)
        {
            spawnPosition = spawnLocation.transform.position;
            spawnRotation = spawnLocation.transform.rotation;
        }

        // turn off all skins, activate current skin
        foreach (AgentSkin skin in AgentSkins)
        {
            skin.gameObject.SetActive(false);
        }
        AgentSkins[activeSkinIndex].gameObject.SetActive(true);
    }

    // input event callback
    private void OnSwapSkins(InputAction.CallbackContext obj)
    {
        cycleToNextSkin();
    }

    // cycle to the next skin
    void cycleToNextSkin()
    {
        int nextSkin = (activeSkinIndex + 1) % AgentSkins.Count;
        print("swapping to new skin");
        activateSkin(nextSkin);
    }

    // set the active skin to the skin at the given 0-based index.
    // if index is not on the list, no action is taken. 
    public void setActiveSkin(int index)
    {
        // check that index is on the list.
        if (index >= 0 && index < AgentSkins.Count)
        {
            activateSkin(index);
        }
    }

    // set the active skin to the given skinGameObject. skinGameObject must already be on the list.
    // if null, this call is ignored.
    // if this is already the active skin, no action is taken. 
    public void setActiveSkin(AgentSkin skinGameObject)
    {
        if (skinGameObject == null) return;
        if (AgentSkins[activeSkinIndex] == skinGameObject) return;

        int index = AgentSkins.IndexOf(skinGameObject);
        if(index >= 0)
        {
            activateSkin(index);
        }
    }

    // Set the active skin to the given index. The current skin will be deactivated.
    // Note that this will run any associated animation controller from it's start state.
    // if the current index is already active, no action is taken. 
    private void activateSkin(int index)
    {
        // check if we should bother
        if (activeSkinIndex == index) return;

        // update the spawn location?
        if(updateSpawnLocation)
        {
            spawnPosition = spawnLocation.transform.position;
            spawnRotation = spawnLocation.transform.rotation;
        }

        // swap!
        AgentSkins[activeSkinIndex].gameObject.SetActive(false);
        activeSkinIndex = index;
        AgentSkins[activeSkinIndex].transform.SetPositionAndRotation(spawnPosition, spawnRotation);
        AgentSkins[activeSkinIndex].gameObject.SetActive(true);
    }

    // pass any animation calls on to the skin.
    public void PerformBodyAction(AgentSkin.BodyAction action)
    {
        activeSkin.PerformBodyAction(action);
    }
    public void MakeFace(AgentSkin.FaceExpression face)
    {
        activeSkin.MakeFace(face);
    }
}
