using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MovementValue { get; private set; }

    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action SprintEvent;
    public event Action TargetEvent;
    public event Action InteractEvent;
    public event Action ToggleCharacterTabEvent;
    public event Action ToggleInventoryTabEvent;
    
    public bool IsAttacking { get; private set; }
    public bool IsBlocking { get; private set; }
    public bool CanInteract { get; private set; }

    private Controls controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void Start()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
       
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(!context.performed) { return; }

        JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if(!context.performed) { return; }

        DodgeEvent?.Invoke();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            SprintEvent?.Invoke();
        }
        else if(context.canceled)
        {
            SprintEvent?.Invoke();
        }

        
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if(!context.performed) { return; }
        TargetEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            IsAttacking = true;
        }
        else if(context.canceled)
        {
            IsAttacking = false;
        }
        
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            IsBlocking = true;
        }
        else if(context.canceled)
        {
            IsBlocking = false;
        }
        
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(!context.performed) { return; }

        InteractEvent?.Invoke();
    }

    public void OnToggleCharacterTab(InputAction.CallbackContext context)
    {
        if(!context.performed) { return; }

        ToggleCharacterTabEvent?.Invoke();
    }

    public void OnToggleInventoryTab(InputAction.CallbackContext context)
    {
        if(!context.performed) { return; }

        ToggleInventoryTabEvent?.Invoke();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

}
