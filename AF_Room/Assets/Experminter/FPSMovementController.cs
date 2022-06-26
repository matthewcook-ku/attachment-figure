using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// FPS Style Player Movement Control
//
// This is a simple implementation of an FPS style player controller, with optional gravity.
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
    //FPSControls controls;
    //FPSControls.GroundMovementActions groundMovement;
    // Input System values from controls
    Vector2 horizontalInput;
    Vector2 verticalInput;
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

    public bool gravityOn;

    [HideInInspector]
    public bool canMove = true;

    private void Awake()
    {
        //controls = new FPSControls();
        //groundMovement = controls.GroundMovement;
    }

    // input event functions
    void OnHorizontalInputPerformed(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<Vector2>();
    }
    void OnVerticalInputPerformed(InputAction.CallbackContext context)
    {
        verticalInput = context.ReadValue<Vector2>();
    }
    void OnJumpPerformed(InputAction.CallbackContext context)
    {
        jumpButton = context.ReadValueAsButton();
    }
    void OnRunPerformed(InputAction.CallbackContext context)
    {
        runButton = context.ReadValueAsButton();
    }
    void OnLookXPerformed(InputAction.CallbackContext context)
    {
        lookX = context.ReadValue<float>();
    }
    void OnLookYPerformed(InputAction.CallbackContext context)
    {
        lookY = context.ReadValue<float>();
    }

    private void OnEnable()
    {
        // register for input events
        InputControls.FPSControlsActions actions = AFManager.Instance.InputManager.InputActions.FPSControls;
        actions.HorizontalMovement.performed += OnHorizontalInputPerformed;
        actions.VerticalMovement.performed += OnVerticalInputPerformed;
        actions.Jump.performed += OnJumpPerformed;
        actions.Run.performed += OnRunPerformed;
        actions.LookX.performed += OnLookXPerformed;
        actions.LookY.performed += OnLookYPerformed;

        // activate controls
        InputManager.EnableActionMap(actions, true);

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnDisable()
    {
        // deregister for input events
        InputControls.FPSControlsActions actions = AFManager.Instance.InputManager.InputActions.FPSControls;
        actions.HorizontalMovement.performed -= OnHorizontalInputPerformed;
        actions.VerticalMovement.performed -= OnVerticalInputPerformed;
        actions.Jump.performed -= OnJumpPerformed;
        actions.Run.performed -= OnRunPerformed;
        actions.LookX.performed -= OnLookXPerformed;
        actions.LookY.performed -= OnLookYPerformed;

        // deactivate controls
        InputManager.EnableActionMap(actions, false);

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
        // Recalculate move direction based on axes
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 up = transform.TransformDirection(Vector3.up);
        Vector3 forward = gravityOn ? transform.TransformDirection(Vector3.forward) : playerCamera.transform.TransformDirection(Vector3.forward);
        

        // reset the downward motion when we land. 
        if (gravityOn && characterController.isGrounded && moveDirection.y < 0)
        {
            moveDirection.y = 0;
        }

        // read input and translate to vectors forward, up/down, and side to side
        float curSpeedX = canMove ? (runButton ? runningSpeed : walkingSpeed) * horizontalInput.x : 0;
        float curSpeedY = canMove ? (runButton ? runningSpeed : walkingSpeed) * horizontalInput.y : 0;
        float curSpeedZ = canMove ? (runButton ? runningSpeed : walkingSpeed) * verticalInput.y : 0;

        // stash previous vertical movement (if we were jumping or falling)
        float prevMovementDirectionY = moveDirection.y;

        // find new movement vector
        // might need to normalize to avoid moving faster diagonally, but not now
        moveDirection = (forward * curSpeedY) + (right * curSpeedX);

        if (gravityOn)
        {

            if (jumpButton && canMove && characterController.isGrounded)
            {
                moveDirection.y = jumpSpeed;
            }
            else
            {
                moveDirection.y = prevMovementDirectionY;
            }
        }
        else
        {
            moveDirection += (up * curSpeedZ);
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (gravityOn && !characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            // to look up and down, we tilt the camera. this keeps player level.
            rotationX += -lookY * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            // to look left and right, we turn the whole player object. this updates forward direction.
            transform.rotation *= Quaternion.Euler(0, lookX * lookSpeed, 0);
        }
    }
}
