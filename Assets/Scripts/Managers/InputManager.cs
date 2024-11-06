using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class InputManager : MonoBehaviour
{
    // Script References
    [SerializeField] private PlayerLocomotionHandler playerLocomotionHandler;
    [SerializeField] private CameraManager cameraManager; // Reference to CameraManager


    [Header("Movement Inputs")]
    public float verticalInput;
    public float horizontalInput;
    public bool jumpInput;
    public Vector2 movementInput;
    public float moveAmount;
    public bool sprintInput;

    public PlayerInputActions playerControls;

    private InputAction move;
    private InputAction look;
    private InputAction jump;
    private InputAction sprint;
    private InputAction fire;

    public InteractionManager interactionManager;

    [Header("Camera Inputs")]
    public float scrollInput; // Scroll input for camera zoom
    public Vector2 cameraInput; // Mouse input for the camera

    public bool isPauseKeyPressed = false;

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpInput();
        HandleCameraInput();
        HandlePauseKeyInput();
        HandleInteractionInput();
    }

    private void HandleCameraInput()
    {
        //NEW
        cameraInput = look.ReadValue<Vector2>();
        
        //OLD
        // Get mouse input for the camera
        //cameraInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
        // Get scroll input for camera zoom
        scrollInput = Input.GetAxis("Mouse ScrollWheel");
        // Send inputs to CameraManager
        cameraManager.zoomInput = scrollInput;
        cameraManager.cameraInput = cameraInput;        
    }

    private void HandleMovementInput()
    {
        //NEW
        movementInput = move.ReadValue<Vector2>();

        //OLD
        //movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        horizontalInput = movementInput.x;
        verticalInput = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
    }

    private void HandlePauseKeyInput()
    {
        isPauseKeyPressed = Input.GetKeyDown(KeyCode.Escape); // Detect the escape key press
    }

    private void HandleSprintingInput()
    {
        sprintInput = sprint.IsPressed();

        if (sprintInput) //&& moveAmount > 0.5f)
        {
            playerLocomotionHandler.isSprinting = true;
        }
        else
        {
            playerLocomotionHandler.isSprinting = false;
        }
    }

    private void HandleJumpInput()
    {
        //NEW
        jumpInput = jump.IsPressed();

        //OLD
        //jumpInput = Input.GetKeyDown(KeyCode.Space); // Detect jump input (spacebar)
        if (jumpInput)
        {
            playerLocomotionHandler.HandleJump(); // Trigger jump in locomotion handler
        }
    }

    private void HandleInteractionInput()
    {
        if (fire.IsPressed() && interactionManager.interactionPossible)
        {
            interactionManager.Interact();
        }
    }

    private void OnEnable()
    {
        playerControls = new PlayerInputActions();
        interactionManager = FindObjectOfType<InteractionManager>();
        move = playerControls.Player.Move;
        look = playerControls.Player.Look;
        jump = playerControls.Player.Jump;
        sprint = playerControls.Player.Sprint;
        fire = playerControls.Player.Fire; 
        move.Enable();
        look.Enable();
        jump.Enable();
        sprint.Enable();
        fire.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        look.Disable();
        jump.Disable();
        sprint.Disable();
        fire.Disable();
    }

}
