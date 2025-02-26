using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    public static GameInput Instance { get; private set; }


    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractCancelAction;
    public event EventHandler OnReviveAction;
    public event EventHandler OnReviveCancelAction;
    public event EventHandler OnGrabAction;
    public event EventHandler OnGrabCancelAction;
    public event EventHandler OnJumpAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnTurboAction;

    public event EventHandler OnBindingRebind;
    
    public enum Binding
    {
        MoveUp,
        MoveDown,
        MoveRight,
        MoveLeft,
        Jump,
        Interact,
        Turbo,
        Revive,
        Pause,
    }

    private PlayerInputActions playerInputActions;
    

    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();

        //use stored bindings previously set by player
        //if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        //{
        //    playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        //}


        playerInputActions.Player.Enable();

        //playerInputActions.Player.Interact.started += Interact_started;

        playerInputActions.Player.Interact.performed += Interact_performed;

        playerInputActions.Player.Interact.canceled += Interact_canceled;

        playerInputActions.Player.Revive.performed += Revive_performed;

        playerInputActions.Player.Revive.canceled += Revive_canceled;

        playerInputActions.Player.Grab.started += Grab_started;

        playerInputActions.Player.Grab.canceled += Grab_canceled;

        playerInputActions.Player.Jump.performed += Jump_performed;

        playerInputActions.Player.Pause.performed += Pause_performed;

        playerInputActions.Player.Turbo.performed += Turbo_performed;


    }

    

    private void OnDestroy()
    {//this unsubscribes from existing Input Actions on pause, preventing logic from the previous game preventing future use of player input actions
        //playerInputActions.Player.Interact.started -= Interact_started;

        playerInputActions.Player.Interact.performed -= Interact_performed;

        playerInputActions.Player.Interact.canceled -= Interact_canceled;

        playerInputActions.Player.Revive.performed -= Revive_performed;

        playerInputActions.Player.Revive.canceled -= Revive_canceled;

        playerInputActions.Player.Grab.started -= Grab_started;

        playerInputActions.Player.Grab.canceled -= Grab_canceled;

        playerInputActions.Player.Jump.performed -= Jump_performed;

        playerInputActions.Player.Pause.performed -= Pause_performed;

        playerInputActions.Player.Turbo.performed -= Turbo_performed;

        playerInputActions.Dispose();
    }

    //T key pressed
    private void Turbo_performed(InputAction.CallbackContext obj)
    {
        OnTurboAction?.Invoke(this, EventArgs.Empty);
    }

    //Esc key pressed
    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
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

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        //Debug.Log("GameInputScript: E key pressed");
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }


    //private void Interact_started(InputAction.CallbackContext obj)
    //{
    //    //Debug.Log("GameInputScript: E key pressed");
    //    OnInteractAction?.Invoke(this, EventArgs.Empty);
    //}

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
            case Binding.Turbo:
                return playerInputActions.Player.Turbo.bindings[0].ToDisplayString();
            case Binding.Revive:
                return playerInputActions.Player.Revive.bindings[0].ToDisplayString();
            case Binding.Jump:
                return playerInputActions.Player.Jump.bindings[0].ToDisplayString().ToUpper();
            case Binding.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString().ToUpper().Substring(0,3);
        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        { 
            default:
            case Binding.MoveUp:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.MoveDown:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.MoveLeft:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.MoveRight:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.Jump:
                inputAction = playerInputActions.Player.Jump;
                bindingIndex = 0;
                break;
            case Binding.Turbo:
                inputAction = playerInputActions.Player.Turbo;
                bindingIndex = 0;
                break;
            case Binding.Revive:
                inputAction = playerInputActions.Player.Revive;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete((callback) =>
        {
            //Debug.Log(callback.action.bindings[1].path);
            //Debug.Log(callback.action.bindings[1].overridePath);
            callback.Dispose();
            playerInputActions.Player.Enable();
            onActionRebound();

            
            PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();

            OnBindingRebind?.Invoke(this, EventArgs.Empty);

        })
        .Start();
    }


}
