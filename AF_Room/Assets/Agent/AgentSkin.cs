using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using DG.Tweening;

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

    // structure components
    [HideInInspector]
    public AgentModel Model;
    public Animator animator { get { return GetComponent<Animator>(); } }
    public GameObject gazeTarget { get { return Model.Head.GazeTarget; } }

    // pose settings
    // blinking
    [Tooltip("Time in sec between blinks.")]
    public float blinkRate = 5.0f;
    [Tooltip("Blink variance random +/- this many sec.")]
    public float blinkVariance = 2.8f;
    private Coroutine BlinkCoroutine;


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

    // input controls
    private InputControls.AgentControlsActions controls;

    // Awake
    private void Awake()
    {
        // collect reference to objects we need
        Model = new AgentModel(this.gameObject);
        controls = AFManager.Instance.InputManager.InputActions.AgentControls;

        // install input controls
        controls.ToggleHandIK.performed += OnToggleHandIK;
        controls.ToggleLean.performed += OnToggleLean;
        controls.ToggleHeadLookAt.performed += OnToggleHeadLookAt;
        controls.ToggleEyeLookAt.performed += OnToggleEyeLookAt;
    }

    private void OnEnable()
    {
        // begin the blink coroutine
        Debug.Log("Starting Blink Coroutine");
        BlinkCoroutine = StartCoroutine(RandomBlinkCoroutine());
    }
    private void OnDisable()
    {
        // stop random blinking
        Debug.Log("Stopping Blink Coroutine");
        StopCoroutine(BlinkCoroutine);
    }

    private IEnumerator RandomBlinkCoroutine()
    {
        float randomWait;
        while (true)
        {
            randomWait = blinkRate + UnityEngine.Random.Range(-blinkVariance, blinkVariance);
            animator.SetTrigger("Blink");
            Debug.Log("BLINK! - next blink in " + randomWait + " sec");
            yield return new WaitForSeconds(randomWait);
        }
    }

    /*
    // TODO: Have the eyes point in a random direction when blinking.
    // needs to play nice with the eye constraints.
    //
    // this code tries to effect the bones, but prob better to move the eye constraint itself

    // eye darts
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
        Model.ToggleBothHandConstraints();
    }
    private void OnToggleLean(InputAction.CallbackContext obj)
    {
        Debug.Log("Toggle Lean Constraint");
        CenterLeanTarget();
        Model.Spine.ToggleConstraint();
    }
    private void OnToggleHeadLookAt(InputAction.CallbackContext obj)
    {
        Debug.Log("Toggle Head Look At");
        Model.Head.ToggleConstraint();
    }
    private void OnToggleEyeLookAt(InputAction.CallbackContext obj)
    {
        Debug.Log("Toggle Eye Look At");
        Model.ToggleBothEyeConstraints();
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
                animator.SetTrigger("HeadTiltRight");
                break;
            case BodyAction.HeadTiltNeutral:
                animator.SetTrigger("HeadTiltNeutral");
                break;
            case BodyAction.HeadTiltLeft:
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
        alignTransforms(arm.TipBone, hand.PlaceTarget);
        alignTransforms(arm.MidBone, hand.Hint);
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

    // place one game object in the same position and rotation of another
    private void alignTransforms(GameObject src, GameObject dst)
    {
        alignTransforms(src.transform, dst.transform);
    }
    private void alignTransforms(Transform src, Transform dst)
    {
        dst.SetPositionAndRotation(src.position, src.rotation);
    }

    void UpdateLookTargetToSubject()
    {
        UpdateLookTarget(AFManager.Instance.subject.GazeTarget);
    }
    void UpdateLookTarget(Transform target)
    {
        Model.LookTarget.transform.position = target.position;
    }
    void ClearLookTarget()
    {
        Model.ResetLookTargetPosition();
    }
}
