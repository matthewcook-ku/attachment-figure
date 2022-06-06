using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FPS Style Player Movement Control
//
// This is a simple implementation of an FPS style player controller.
// Note that this uses the new unity InputSystem controls.
//
// This controller should be placed with a CharacterController component. Place this on an empty, and then place the player camera as a child. This allows the camera to look left/right, but the whole object to tilt up/down.
//
// Inputs:
// See the FPSControls object for input actions and bindings.
// See also : https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/QuickStartGuide.html 

[RequireComponent(typeof(CharacterController))]

public class FPSMovementController : MonoBehaviour
{
    // Input System controller
    InputControls.FPSControlsActions controls;
    // Input System values from controls
    Vector2 horizontalInput;
    bool jumpButton;
    bool runButton;
    float lookX;
    float lookY;

    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 80.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    private void Awake()
    {
        controls = AFManager.Instance.InputManager.InputActions.FPSControls;

        // connect variables to the input controls
        controls.HorizontalMovement.performed += context => horizontalInput = context.ReadValue<Vector2>();
        controls.Jump.performed += context => jumpButton = context.ReadValueAsButton();
        controls.Run.performed += context => runButton = context.ReadValueAsButton();
        controls.LookX.performed += context => lookX = context.ReadValue<float>();
        controls.LookY.performed += context => lookY = context.ReadValue<float>();
    }

    private void OnEnable()
    {
        controls.Enable();
        
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnDisable()
    {
        controls.Disable();

        // unlock the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // reset the downward motion when we land. 
        if (characterController.isGrounded && moveDirection.y < 0)
        {
            moveDirection.y = 0;
        }

        // read input and translate to vectors forward and side to side
        float curSpeedX = canMove ? (runButton ? runningSpeed : walkingSpeed) * horizontalInput.x : 0;
        float curSpeedY = canMove ? (runButton ? runningSpeed : walkingSpeed) * horizontalInput.y : 0;

        // load previous vertical movement (if we were jumping or falling)
        float movementDirectionY = moveDirection.y;

        // find new movement vector
        // might need to normalize to avoid moving faster diagonally, but not now
        moveDirection = (forward * curSpeedY) + (right * curSpeedX);

        if (jumpButton && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            // to look left and right, we turn the camera
            rotationX += -lookY * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            // to look up and down, we tilt the whole player object.
            transform.rotation *= Quaternion.Euler(0, lookX * lookSpeed, 0);
        }
    }
}
