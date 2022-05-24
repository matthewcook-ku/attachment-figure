using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class AgentSkin : MonoBehaviour
{
    // structure components
    [HideInInspector]
    public AgentModel Model;
    public Animator animator { get { return GetComponent<Animator>(); } }
    public GameObject gazeTarget { get { return Model.Head.GazeTarget; } }

    // action triggering
    private InputControls.AgentControlsActions controls;

    // Awake
    private void Awake()
    {
        Model = new AgentModel(this.gameObject);

        controls = AFManager.Instance.InputManager.InputActions.AgentControls;

        // install input controls
        controls.ToggleHandIK.performed += OnToggleHandIK;
        controls.ToggleLean.performed += OnToggleLean;
        controls.ToggleHeadLookAt.performed += OnToggleHeadLookAt;
        controls.ToggleEyeLookAt.performed += OnToggleEyeLookAt;
    }

    private void OnToggleHandIK(InputAction.CallbackContext obj)
    {
        Debug.Log("Toggle Hand IK");
        FixBothHandIKPositions();
        Model.ToggleBothHandConstraints();
    }
    private void OnToggleLean(InputAction.CallbackContext obj)
    {
        Debug.Log("Toggle Lean Constraint");
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

    // place one game object in the same position and rotation of another
    private void alignTransforms(GameObject src, GameObject dst)
    {
        dst.transform.SetPositionAndRotation(src.transform.position, src.transform.rotation);
    }
}
