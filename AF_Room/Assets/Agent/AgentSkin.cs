using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using CrazyMinnow.SALSA;

/*
 * Structure and Animation Control of a Skin
 * 
 * Apply this component to the imported prefab model of an agent skin. 
 * This class holds the state for the skin, and drives all of it's animations. 
 */

public class AgentSkin : MonoBehaviour
{
    public enum BodyAction
    {
        None = 0,
        HeadNod = 1,
        HeadShake = 2,
        HeadTiltRight = 3,
        HeadTiltNeutral = 4,
        HeadTiltLeft = 5,
        SitShift = 6,
        GlanceShift = 7,
        Blink = 9
    };
    private BodyAction currentHeadTilt;
    public static string BodyActionToString(BodyAction item)
	{
        switch(item)
		{
            case BodyAction.None:
                return "none";
            case BodyAction.HeadNod:
                return "head nod";
            case BodyAction.HeadShake:
                return "head shake";
            case BodyAction.HeadTiltRight:
                return "head tilt right";
            case BodyAction.HeadTiltNeutral:
                return "head tile neutral";
            case BodyAction.HeadTiltLeft:
                return "head tile left";
            case BodyAction.SitShift:
                return "shift sitting position";
            case BodyAction.GlanceShift:
                return "random glance";
            case BodyAction.Blink:
                return "blink";
		}
        return "unknow body action";
	}
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
    private FaceExpression currentExpression;
    public static string FaceExpressionToString(FaceExpression item)
    {
        switch (item)
        {
            case FaceExpression.None:
                return "none";
            case FaceExpression.Neutral:
                return "neutral";
            case FaceExpression.Smile:
                return "smile";
            case FaceExpression.Frown:
                return "frown";
            case FaceExpression.Concern:
                return "concern";
            case FaceExpression.Disgust:
                return "disgust";
            case FaceExpression.Anger:
                return "anger";
            case FaceExpression.Laugh:
                return "laugh";
            default:
                return "unknow expression";
        }
    }
    public enum SkinTone
    {
        Tone0 = 0,
        Tone1 = 1,
        Tone2 = 2,
        Tone3 = 3,
        Tone4 = 4,
        Tone5 = 5,
        Tone6 = 6,
        Tone7 = 7
    };

    // Set of flags to note what constraints are on or off to store state.
    // Make sure all added states are powers of 2
    // https://stackoverflow.com/questions/3261451/using-a-bitmask-in-c-sharp
    //
    // To set a flag:
    // SkinConstraint state |= SkinConstraint.Lean; // add lean
    // SkinConstraint state = SkinConstraint.Lean | SkinConstraint.HandRight; // set to lean and RH
    //
    // To unset a flag:
    // state = state & (~SkinConstraint.Lean); // unset lean
    //
    // To test a flag:
    // bool HasLean = (state & SkinConstraint.Lean) != SkinConstraint.None;
    // bool HasLean = (state & SkinConstraint.Lean) == SkinConstraint.Lean; // also works
    // bool HasLean = state.HasFlags(SkinConstraint.Lean); // also works
    [Flags] public enum SkinConstraintFlags
    {
        None = 0,
        Lean = 1,
        HandRight = 2,
        HandLeft = 4,
        NeckLookAt = 8,
        HeadLookAt = 16,
        EyeLookAtRight = 32,
        EyeLookAtLeft = 64,
    };
    private SkinConstraintFlags storedActiveConstraints;
    private bool constraintsPaused = false;
    public float ConstraintTransitionTime = 1.0f; // time in seconds to go in and out of constraint. 

    // prefix to add before mesh game object names
    [Tooltip("This prefix will be used when looking for the mesh parts like Body and LeftEye.")]
    public string SkinMeshPrefix = "";

    // structure components
    [HideInInspector]
    public AgentModel Model;
    public Animator animator { get { return GetComponent<Animator>(); } }
    public GameObject gazeTarget { get { return Model.Head.GazeTarget; } }

    private Eyes LookController;
    public bool currentlyLooking
	{
		get
		{
            return (LookController.headEnabled && LookController.eyeEnabled);
		}
        set
		{
            Debug.Log(gameObject.name + ": setting currentlyLooking: " + value);
            if (value)
            {
                Debug.Log(gameObject.name + ": setting currentlyLooking: true");
                //LookController.lookTarget = Model.LookTarget.transform;
                Model.LookTargetConstraint.weight = 1.0f;


                LookController.EnableHead(true);
                LookController.EnableEye(true);
            }
            else
            {
                Debug.Log(gameObject.name + ": setting currentlyLooking: false");
                //LookController.lookTarget = null;
                Model.LookTargetConstraint.weight = 0.0f;
                CenterLookTarget();


				try
				{
					LookController.EnableHead(false);
					LookController.EnableEye(false);
				}
				catch (NullReferenceException)
				{
                    // SALSA EYES throws this exception on the first init for some reason, just ignore it
                    Debug.Log("Caught SALSA Exception");
				}
            }
        }
	}
    private void CenterLookTarget()
    {
        Model.ResetLookTargetPosition();
    }

    // texture selectors for swapping textures
    public TextureSelector HeadTextureSelector;
    public TextureSelector BodyTextureSelector;

    // pose settings

    // blinking
    // Now using SALSA's EYE for blinks and head movement
    //public bool RandomBlinks;
    //[Tooltip("Time in sec between blinks.")]
    //public float blinkRate = 5.0f;
    //[Tooltip("Blink variance random +/- this many sec.")]
    //public float blinkVariance = 2.8f;
    //private Coroutine BlinkCoroutine;


    // leaning
    public float LeanLimit = 1.0f; // how far to move target forward or back
    [SerializeField]
    private float _LeanDegree = 0.0f;
    public float LeanDegree
    {
        get { return _LeanDegree; }
        set
        {
            _LeanDegree = value;
            SetLean(_LeanDegree);
        }
    }
    private float leanScaleFactor = 0.1f; // multiplied by LeanLimit to make LeanLimit a human scale value


    // skin voice settings
    public string DefaultVoiceName;
    [Range(0.0f, 1.0f)]
    public float DefaultVoiceVolume = 1.0f;
    [Range(0.0f, 2.0f)]
    public float DefaultVoicePitch = 1.0f;
    [Range(0.01f, 3.0f)]
    public float DefaultVoiceSpeed = 1.0f;

    // Awake
    private void Awake()
    {
        // collect reference to objects we need
        Model = new AgentModel(this.gameObject, SkinMeshPrefix);

        LookController = GetComponent<Eyes>();
    }

	void AddRemoveInputControls(bool add)
    {
        InputControls.AgentControlsActions controls;

        if (AFManager.Instance == null)
            return;
        controls = AFManager.Instance.inputManager.InputActions.AgentControls;

        // install input controls
        if (add)
        {
            controls.ToggleHandIK.performed += OnToggleHandIK;
            controls.ToggleLean.performed += OnToggleLean;
            controls.ToggleHeadLookAt.performed += OnToggleHeadLookAt;
            controls.ToggleEyeLookAt.performed += OnToggleEyeLookAt;

            controls.NextSkinTone.performed += OnNextSkinTone;
            controls.PrevSkinTone.performed += OnPrevSkinTone;
        }
        else // remove
        {
            controls.ToggleHandIK.performed -= OnToggleHandIK;
            controls.ToggleLean.performed -= OnToggleLean;
            controls.ToggleHeadLookAt.performed -= OnToggleHeadLookAt;
            controls.ToggleEyeLookAt.performed -= OnToggleEyeLookAt;

            controls.NextSkinTone.performed -= OnNextSkinTone;
            controls.PrevSkinTone.performed -= OnPrevSkinTone;
        }
        
    }

    void ActivateDefulatVoiceForSkin()
    {
        // do...while(false) will run once, and allow me to break at any point
        // this syntax avoids a lot of nested if's to check if things are null
        do
        {
            if (AFManager.Instance == null)
                break;
            if (AFManager.Instance.agent == null)
                break;
            if (AFManager.Instance.agent.Speaker == null)
                break;

            TextSpeaker Speaker = AFManager.Instance.agent.Speaker;
            if (Speaker.Ready)
            {
                Speaker.VoiceName = DefaultVoiceName;
                Speaker.VoiceVolume = DefaultVoiceVolume;
                Speaker.VoicePitch = DefaultVoicePitch;
                Speaker.VoiceSpeed = DefaultVoiceSpeed;

                // in case we had to use the callback
                TextSpeaker.OnInit -= ActivateDefulatVoiceForSkin;

                return;
            }

        } while (false);

        // if anything above failed, then we'll be dumped here
        Debug.Log("AgentSkin: Unable to activate default voice, speaker not ready. Registering for callback.");
        TextSpeaker.OnInit += ActivateDefulatVoiceForSkin;
    }

    // called each time this skin is activated
    private void OnEnable()
    {
        Debug.Log("<color=olive>Activating Agent Skin: " + name + "</color>");

        AddRemoveInputControls(true);

        // begin the blink coroutine
        //if (RandomBlinks) StartRandomBlinks();

        // activate the associated voice for this skin
        // note that this call may fail if this OnEnable is happening before other components have loaded.
        ActivateDefulatVoiceForSkin();

        // when model starts up, make sure look constraint is off
        currentlyLooking = false;

        // head tilt always starts neutral
        currentHeadTilt = BodyAction.HeadTiltNeutral;
        // expression always starts neutral
        currentExpression = FaceExpression.Neutral;
    }
    // called each time this skin is deactivated
    private void OnDisable()
    {
        // remove input controls
        AddRemoveInputControls(false);

        // stop random blinking
        //if (RandomBlinks) StopRandomBlinks();

        // in case we were still waiting to activate voice
        TextSpeaker.OnInit -= ActivateDefulatVoiceForSkin;

        Debug.Log("<color=olive>Deactivating Agent Skin: " + name + "</color>");
    }

	//private void StartRandomBlinks()
	//{
	//    Debug.Log("starting random blink coroutine");
	//    BlinkCoroutine = StartCoroutine(RandomBlinkCoroutine());
	//}
	//private void StopRandomBlinks()
	//{
	//    StopCoroutine(BlinkCoroutine);
	//}
	//private IEnumerator RandomBlinkCoroutine()
	//{
	//    float randomWait;
	//    while (true)
	//    {
	//        randomWait = blinkRate + UnityEngine.Random.Range(-blinkVariance, blinkVariance);
	//        animator.SetTrigger("Blink");
	//        Debug.Log("BLINK! - next blink in " + randomWait + " sec");
	//        yield return new WaitForSeconds(randomWait);
	//    }
	//}

	private void PrintFlags(SkinConstraintFlags flags, string prompt = "")
    {
        string result = "";
        result += (flags.HasFlag(SkinConstraintFlags.Lean) ? "[Ln]" : "[ ]");
        result += (flags.HasFlag(SkinConstraintFlags.HandRight) ? "[HR]" : "[ ]");
        result += (flags.HasFlag(SkinConstraintFlags.HandLeft) ? "[HL]" : "[ ]");
        result += (flags.HasFlag(SkinConstraintFlags.NeckLookAt) ? "[Nk]" : "[ ]");
        result += (flags.HasFlag(SkinConstraintFlags.HeadLookAt) ? "[Hd]" : "[ ]");
        result += (flags.HasFlag(SkinConstraintFlags.EyeLookAtRight) ? "[ER]" : "[ ]");
        result += (flags.HasFlag(SkinConstraintFlags.EyeLookAtLeft) ? "[EL]" : "[ ]");
        Debug.Log(prompt + result);
    }
    private SkinConstraintFlags GetCurrentActiveConstraints()
    {
        SkinConstraintFlags active = SkinConstraintFlags.None;
        if (Model.Spine.ConstraintActive) active |= SkinConstraintFlags.Lean;
        if (Model.HandRight.ConstraintActive) active |= SkinConstraintFlags.HandRight;
        if (Model.HandLeft.ConstraintActive) active |= SkinConstraintFlags.HandLeft;
        if (Model.Neck.ConstraintActive) active |= SkinConstraintFlags.NeckLookAt;
        if (Model.Head.ConstraintActive) active |= SkinConstraintFlags.HeadLookAt;
        if (Model.EyeRight.ConstraintActive) active |= SkinConstraintFlags.EyeLookAtRight;
        if (Model.EyeLeft.ConstraintActive) active |= SkinConstraintFlags.EyeLookAtLeft;
        return active;
    }
    private void ActivateConstraintsFromFlags(SkinConstraintFlags flags)
    {
        if (flags.HasFlag(SkinConstraintFlags.Lean))
		{
			Model.Spine.TweenOnConstraint(ConstraintTransitionTime);
		}
		if (flags.HasFlag(SkinConstraintFlags.HandRight))
		{
            FixHandIKPosition(AgentModel.Side.right);
            Model.HandRight.TweenOnConstraint(ConstraintTransitionTime);
		}
		if (flags.HasFlag(SkinConstraintFlags.HandLeft))
		{
            FixHandIKPosition(AgentModel.Side.left);
            Model.HandLeft.TweenOnConstraint(ConstraintTransitionTime);
		}
		if (flags.HasFlag(SkinConstraintFlags.NeckLookAt))
		{
			Model.Neck.TweenOnConstraint(ConstraintTransitionTime);
		}
		if (flags.HasFlag(SkinConstraintFlags.HeadLookAt))
		{
			Model.Head.TweenOnConstraint(ConstraintTransitionTime);
		}
		if (flags.HasFlag(SkinConstraintFlags.EyeLookAtRight))
		{
			Model.EyeRight.TweenOnConstraint(ConstraintTransitionTime);
		}
		if (flags.HasFlag(SkinConstraintFlags.EyeLookAtLeft))
		{
			Model.EyeLeft.TweenOnConstraint(ConstraintTransitionTime);
		}
    }
    private void DeactivateConstraints()
    {
            Model.Spine.TweenOffConstraint(ConstraintTransitionTime);
            Model.HandRight.TweenOffConstraint(ConstraintTransitionTime);
            Model.HandLeft.TweenOffConstraint(ConstraintTransitionTime);
            Model.Neck.TweenOffConstraint(ConstraintTransitionTime);
            Model.Head.TweenOffConstraint(ConstraintTransitionTime);
            Model.EyeRight.TweenOffConstraint(ConstraintTransitionTime);
            Model.EyeLeft.TweenOffConstraint(ConstraintTransitionTime);
    }

    public void PauseConstraintsForAnimation()
    {
        if (constraintsPaused) return;
        constraintsPaused = true;

        Debug.Log("Pausing constraints to animate");
        storedActiveConstraints = GetCurrentActiveConstraints();
        PrintFlags(storedActiveConstraints, "Turning OFF Constraints: ");
        DeactivateConstraints();
    }
    public void ResumeConstraintsForAnimation()
    {
        if (!constraintsPaused) return;
        constraintsPaused = false;

        Debug.Log("Resuming constraints");
        PrintFlags(storedActiveConstraints, "Turning ON Constraints: ");
        ActivateConstraintsFromFlags(storedActiveConstraints);
    }

    /*
    // Now Replaced with SALSA EYE system
    //
    // TODO: Have the eyes point in a random direction when blinking.
    // needs to play nice with the eye constraints.
    //
    // this code tries to effect the bones, but prob better to move the eye constraint itself
    // eye darts
    //
    public bool DartEyes = true;
    [Tooltip("Deflection in degrees.")]
    public float EyeDartVerticalDeflection = 15.0f;
    [Tooltip("Deflection in degrees.")]
    public float EyeDartHorizontalDeflection = 5.0f;
    [Tooltip("What % chance a blink will be an eye dart.")]
    public float EyeDartPercent = 0.2f;
    [Tooltip("Time to look away before darting back.")]
    public float EyeDartDelay = 1.0f;
    private void ShiftEyes()
    {
        float vertical = UnityEngine.Random.Range(-EyeDartVerticalDeflection, EyeDartVerticalDeflection);
        float horizontal = UnityEngine.Random.Range(-EyeDartHorizontalDeflection, EyeDartHorizontalDeflection);

        vertical = 100.0f;
        horizontal = 100.0f;

        Quaternion rotation = Quaternion.Euler(vertical, 0, horizontal);
        Debug.Log("shifting eyes by: " + vertical + " , " + horizontal + " : " + rotation.eulerAngles);

        Model.EyeLeft.Bone.transform.localRotation = rotation;
        Model.EyeRight.Bone.transform.localRotation = rotation;
    }
    */

    private void OnToggleHandIK(InputAction.CallbackContext obj)
    {
        Debug.Log("Toggle Hand IK");

        FixBothHandIKPositions();
        Model.HandRight.TweenToggleConstraint(ConstraintTransitionTime);
        Model.HandLeft.TweenToggleConstraint(ConstraintTransitionTime);

        PrintFlags(GetCurrentActiveConstraints(), "Currently Active Constraints: ");
    }
    private void OnToggleLean(InputAction.CallbackContext obj)
    {
        Debug.Log("Toggle Lean Constraint");
        CenterLeanTarget();
        Model.Spine.ToggleConstraint();

        PrintFlags(GetCurrentActiveConstraints(), "Currently Active Constraints: ");
    }
    private void OnToggleHeadLookAt(InputAction.CallbackContext obj)
    {
        Debug.Log("Toggle Head Look At");
        Model.Head.ToggleConstraint();

        PrintFlags(GetCurrentActiveConstraints(), "Currently Active Constraints: ");
    }
    private void OnToggleEyeLookAt(InputAction.CallbackContext obj)
    {
        Debug.Log("Toggle Eye Look At");
        Model.ToggleBothEyeConstraints();

        PrintFlags(GetCurrentActiveConstraints(), "Currently Active Constraints: ");
    }

    public void PerformBodyAction(BodyAction action)
    {
        switch (action)
        {
            case BodyAction.HeadNod:
                animator.SetTrigger("HeadNod");
                break;
            case BodyAction.HeadShake:
                animator.SetTrigger("HeadShake");
                break;
            case BodyAction.HeadTiltRight:
                if (currentHeadTilt == BodyAction.HeadTiltRight) break;
                currentHeadTilt = BodyAction.HeadTiltRight;
                animator.SetTrigger("HeadTiltRight");
                break;
            case BodyAction.HeadTiltNeutral:
                if (currentHeadTilt == BodyAction.HeadTiltNeutral) break;
                currentHeadTilt = BodyAction.HeadTiltNeutral;
                animator.SetTrigger("HeadTiltNeutral");
                break;
            case BodyAction.HeadTiltLeft:
                if (currentHeadTilt == BodyAction.HeadTiltLeft) break;
                currentHeadTilt = BodyAction.HeadTiltLeft;
                animator.SetTrigger("HeadTiltLeft");
                break;
            case BodyAction.SitShift:
                animator.SetTrigger("SitShift");
                break;
            case BodyAction.GlanceShift:
                animator.SetTrigger("GlanceShift");
                break;
            case BodyAction.Blink:
                animator.SetTrigger("Blink");
                break;
            default:
                Debug.Log("Unknown BodyAction: " + action);
                break;
        }
    }

    public void MakeFace(FaceExpression face)
    {
        if (currentExpression == face) return;
        currentExpression = face;

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


    // Align hand IK targets with the arm's current position.
    private void FixHandIKPosition(AgentModel.Side side)
    {
        switch (side)
        {
            case AgentModel.Side.left:
                TwoBoneIKTargetsToBones(Model.HandLeft, Model.ArmLeft);
                break;
            case AgentModel.Side.right:
                TwoBoneIKTargetsToBones(Model.HandRight, Model.ArmRight);
                break;
            default:
                Debug.LogError("Unknown IKHand Side");
                break;
        }
    }
    private void FixBothHandIKPositions()
    {
        FixHandIKPosition(AgentModel.Side.left);
        FixHandIKPosition(AgentModel.Side.right);
    }

    // Move IK targets to bone position so that the targets will result in the same bone position.
    // Use this to move the targets in place before activating the IK so that the limb does not move.
    private void TwoBoneIKTargetsToBones(AgentModel.HandModel hand, AgentModel.ArmModel arm)
    {
        AFUtilities.alignTransforms(arm.TipBone, hand.PlaceTarget);
        AFUtilities.alignTransforms(arm.MidBone, hand.Hint);
    }


    // place the lean target centered over the skin to a neutral position.
    private void CenterLeanTarget()
    {
        Debug.Log("Centering Lean Target");

        Transform leanTarget = Model.Spine.LeanTarget.transform;
        Transform skinRoot = Model.SkinRootBone.transform;

        leanTarget.position = skinRoot.position;
        leanTarget.Translate(0.0f, 1.0f, 0.0f); // move up above head
        leanTarget.up = Vector3.up; // keep it level
        leanTarget.Rotate(Vector3.up, skinRoot.eulerAngles.y); // rotate to match
    }

    // set how much lean to apply to the skin
    // degree should be in the range
    //      -1.0f   fully back
    //      0.0f    no lean
    //      1.0f    fully forward
    private void SetLean(float degree)
    {
        Mathf.Clamp(degree, -1.0f, 1.0f);

        Transform leanTarget = Model.Spine.LeanTarget.transform;
        Transform skinRoot = Model.SkinRootBone.transform;
        Vector3 leanPosition = Vector3.zero;
        Quaternion leanRotation = Quaternion.identity;

        // find target center
        leanPosition = skinRoot.position;
        leanPosition += new Vector3(0.0f, 1.0f, 0.0f); // move up 1 unit for some distance from root
        leanRotation *= Quaternion.AngleAxis(skinRoot.eulerAngles.y, Vector3.up); // rotate to face same dir

        // move center forward or back by degree
        leanPosition += (leanRotation * Vector3.forward) * ((LeanLimit * leanScaleFactor) * degree);

        leanTarget.SetPositionAndRotation(leanPosition, leanRotation);
    }


    // switch skintones
    public void SetSkintone(AgentSkin.SkinTone tone)
    {
        SetSkintone((int)tone);
    }
    public void SetSkintone(int toneIndex)
    {
        // we're assuming that these 2 have matching textures for each index.
        if (HeadTextureSelector != null) HeadTextureSelector.SetTexture(toneIndex);
        if (BodyTextureSelector != null) BodyTextureSelector.SetTexture(toneIndex);
    }

    private void OnNextSkinTone(InputAction.CallbackContext obj)
    {
        Debug.Log("Cycle Next Skintone...");
        if (HeadTextureSelector != null) HeadTextureSelector.CycleNextTexture();
        if (BodyTextureSelector != null) BodyTextureSelector.CycleNextTexture();
    }
    private void OnPrevSkinTone(InputAction.CallbackContext obj)
    {
        Debug.Log("Cycle Prev Skintone...");
        if (HeadTextureSelector != null) HeadTextureSelector.CyclePrevTexture();
        if (BodyTextureSelector != null) BodyTextureSelector.CyclePrevTexture();
    }
}
