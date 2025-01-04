using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }


    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractCancelAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnReviveAction;
    public event EventHandler OnReviveCancelAction;
    public event EventHandler OnGrabAction;
    public event EventHandler OnGrabCancelAction;
    public event EventHandler OnJumpAction;
    public event EventHandler OnPauseAction;
    
    public enum Binding
    {
        MoveUp,
        MoveDown,
        MoveRight,
        MoveLeft,
        Jump,
        Interact,
        AltInteract,
        Grab,
        Revive,
        Fire,
        Pause
    }

    private PlayerInputActions playerInputActions;
    

    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.started += Interact_started;

        playerInputActions.Player.Interact.canceled += Interact_canceled;

        playerInputActions.Player.Revive.performed += Revive_performed;

        playerInputActions.Player.Revive.canceled += Revive_canceled;

        playerInputActions.Player.Grab.started += Grab_started;

        playerInputActions.Player.Grab.canceled += Grab_canceled;

        playerInputActions.Player.Jump.performed += Jump_performed;

        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;

        playerInputActions.Player.Pause.performed += Pause_performed;

        
    }

    private void OnDestroy()
    {//this unsubscribes from existing Input Actions on pause, preventing logic from the previous game preventing future use of player input actions
        playerInputActions.Player.Interact.started -= Interact_started;

        playerInputActions.Player.Interact.canceled -= Interact_canceled;

        playerInputActions.Player.Revive.performed -= Revive_performed;

        playerInputActions.Player.Revive.canceled -= Revive_canceled;

        playerInputActions.Player.Grab.started -= Grab_started;

        playerInputActions.Player.Grab.canceled -= Grab_canceled;

        playerInputActions.Player.Jump.performed -= Jump_performed;

        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;

        playerInputActions.Player.Pause.performed -= Pause_performed;

        playerInputActions.Dispose();
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }


    //AlternateInteract
    private void InteractAlternate_performed(InputAction.CallbackContext context)
    {
        Debug.Log("GameInput:F pressed");
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    //Grabbing
    private void Grab_started(InputAction.CallbackContext obj)
    {
        //Debug.Log("GameInputScript: G key pressed");
        OnGrabAction?.Invoke(this, EventArgs.Empty);
    }

    private void Grab_canceled(InputAction.CallbackContext obj)
    {
        //Debug.Log("GameInputScript: G key released");
        OnGrabCancelAction?.Invoke(this, EventArgs.Empty);
    }

    //Jumping
    private void Jump_performed(InputAction.CallbackContext obj)
    {
        //Debug.Log("GameInputScript: Space key pressed");
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    //Revive to active ragdoll state
    private void Revive_performed(InputAction.CallbackContext obj)
    {
        //Debug.Log("GameInputScript: R key pressed");
        OnReviveAction?.Invoke(this, EventArgs.Empty);
    }

    private void Revive_canceled(InputAction.CallbackContext obj)
    {
        //Debug.Log("GameInputScript: R key no longer pressed");
        OnReviveCancelAction?.Invoke(this, EventArgs.Empty);
    }

    
    private void Interact_started(InputAction.CallbackContext obj)
    {
        //Debug.Log("GameInputScript: E key pressed");
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_canceled(InputAction.CallbackContext obj)
    {
        //Debug.Log("GameInputScript: E key released");
        OnInteractCancelAction?.Invoke(this, EventArgs.Empty);
    }


    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 moveInputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        moveInputVector = moveInputVector.normalized;

        return moveInputVector;
    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.MoveUp:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();
                //returns key binding nested within Move binding
            case Binding.MoveDown:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.MoveLeft:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.MoveRight:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.AltInteract:
                return playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Grab:
                return playerInputActions.Player.Grab.bindings[0].ToDisplayString();
            case Binding.Revive:
                return playerInputActions.Player.Revive.bindings[0].ToDisplayString();
            case Binding.Jump:
                return playerInputActions.Player.Jump.bindings[0].ToDisplayString().ToUpper();
            case Binding.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString().ToUpper();
            case Binding.Fire:
                return playerInputActions.Player.Fire.bindings[0].ToDisplayString();
        }
    }
}
