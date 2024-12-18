using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractCancelAction;
    public event EventHandler OnReviveAction;
    public event EventHandler OnReviveCancelAction;
    public event EventHandler OnGrabAction;
    public event EventHandler OnGrabCancelAction;
    public event EventHandler OnJumpAction;


    private PlayerInputActions playerInputActions;
    

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.started += Interact_started;

        playerInputActions.Player.Interact.canceled += Interact_canceled;

        playerInputActions.Player.Revive.performed += Revive_performed;

        playerInputActions.Player.Revive.canceled += Revive_canceled;

        playerInputActions.Player.Grab.started += Grab_started;

        playerInputActions.Player.Grab.canceled += Grab_canceled;

        playerInputActions.Player.Jump.performed += Jump_performed;
    }

    

    //Grabbing
    private void Grab_started(InputAction.CallbackContext obj)
    {
        Debug.Log("GameInputScript: G key pressed");
        OnGrabAction?.Invoke(this, EventArgs.Empty);
    }

    private void Grab_canceled(InputAction.CallbackContext obj)
    {
        Debug.Log("GameInputScript: G key released");
        OnGrabCancelAction?.Invoke(this, EventArgs.Empty);
    }

    //Jumping
    private void Jump_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("GameInputScript: Space key pressed");
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    //Revive to active ragdoll state
    private void Revive_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("GameInputScript: R key pressed");
        OnReviveAction?.Invoke(this, EventArgs.Empty);
    }

    private void Revive_canceled(InputAction.CallbackContext obj)
    {
        Debug.Log("GameInputScript: R key no longer pressed");
        OnReviveCancelAction?.Invoke(this, EventArgs.Empty);
    }

    
    private void Interact_started(InputAction.CallbackContext obj)
    {
        Debug.Log("GameInputScript: E key pressed");
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_canceled(InputAction.CallbackContext obj)
    {
        Debug.Log("GameInputScript: E key released");
        OnInteractCancelAction?.Invoke(this, EventArgs.Empty);
    }


    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 moveInputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        //moveInputVector.x = Input.GetAxis("Horizontal");
        //moveInputVector.y = Input.GetAxis("Vertical");

        moveInputVector = moveInputVector.normalized;

        return moveInputVector;


    }
}
