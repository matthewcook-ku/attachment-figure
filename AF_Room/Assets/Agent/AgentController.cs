using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Allow swapping of skins.
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
    public enum BodyAction
    {
        None = 0,
        HeadNod = 1,
        HeadShake = 2,
        HeadTiltRight = 3,
        HeadTiltNeutral = 4,
        HeadTiltLeft = 5,
        BodyLeanForward = 6,
        BodyLeanNeutral = 7,
        BodyLeanBack = 8,
        Blink = 9
    };
    public enum FaceExpression
    {
        None = 0,
        Neutral = 1,
        Smile = 2,
        Frown = 3,
        Concern = 4,
        Disgust = 5,
        Anger = 6,
        Laugh = 7
    };

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

    // control set
    AgentControls agentControls;

    private void Awake()
    {
        agentControls = new AgentControls();

        agentControls.Skin.SwapSkins.performed += context => cycleToNextSkin();

        //agentControls.Action.HeadNod.performed += context => return;

    }
    private void OnEnable()
    {
        agentControls.Enable();
    }
    private void OnDisable()
    {
        agentControls.Disable();
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

    // cycle to the next skin
    void cycleToNextSkin()
    {
        int nextSkin = (activeSkinIndex + 1) % AgentSkins.Count;
        print("swapping to new skin");
        activateSkin(nextSkin);
    }

    // set the active skin to the skin at the given 0-based index.
    // if index is not on the list, no action is taken. 
    void setActiveSkin(int index)
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
    void setActiveSkin(AgentSkin skinGameObject)
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

    public void PerformBodyAction(BodyAction action)
    {
        Animator animator = activeSkin.animator;
        switch(action)
        {
            case BodyAction.HeadNod:
                animator.SetTrigger("HeadNod");
                break;
            case BodyAction.HeadShake:
                animator.SetTrigger("HeadShake");
                break;
            case BodyAction.HeadTiltRight:
                //animator.SetTrigger("HeadTiltRight");
                break;
            case BodyAction.HeadTiltNeutral:
                //animator.SetTrigger("HeadTiltNeutral");
                break;
            case BodyAction.HeadTiltLeft:
                //animator.SetTrigger("HeadTiltLeft");
                break;
            case BodyAction.BodyLeanForward:
                //animator.SetTrigger("BodyLeanForward");
                break;
            case BodyAction.BodyLeanNeutral:
                //animator.SetTrigger("BodyLeanNeutral");
                break;
            case BodyAction.BodyLeanBack:
                //animator.SetTrigger("BodyLeanBack");
                break;
            case BodyAction.Blink:
                //animator.SetTrigger("Blink");
                break;
            default:
                Debug.Log("Unknown BodyAction: " + action);
                break;
        }

    }
    public void MakeFace(FaceExpression face)
    {
        Animator animator = activeSkin.animator;
        switch (face)
        {
            case FaceExpression.Neutral:
                animator.SetTrigger("FaceNeutral");
                break;
            case FaceExpression.Smile:
                animator.SetTrigger("FaceSmile");
                break;
            case FaceExpression.Frown:
                animator.SetTrigger("FaceFrown");
                break;
            case FaceExpression.Concern:
                animator.SetTrigger("FaceConcern");
                break;
            case FaceExpression.Disgust:
                animator.SetTrigger("FaceDisgust");
                break;
            case FaceExpression.Anger:
                animator.SetTrigger("FaceAnger");
                break;
            case FaceExpression.Laugh:
                animator.SetTrigger("FaceLaugh");
                break;
            default:
                Debug.Log("Unknown Face: " + face);
                break;
        }
    }
}
