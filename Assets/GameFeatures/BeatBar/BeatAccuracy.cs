using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BeatAccuracy : MonoBehaviour
{
    private ControllerActions controllerActions;

    private bool canSelect = true;
    private float thresholdTriggerMove = 0.7f;
    private float thresholdResetMove = 0.3f;

    public event EventHandler<InputAction.CallbackContext> OnHitEvent;

    private void Awake()
    {
        controllerActions = new ControllerActions();

        controllerActions.Gameplay.Use.performed += OnButtonPerformed;
        controllerActions.Gameplay.Reload.performed += OnButtonPerformed;
        controllerActions.Gameplay.Move.performed += OnMovePerformed;
        controllerActions.Gameplay.Move.canceled += OnMovePerformed;
    }

    private void OnDestroy()
    {
        controllerActions.Gameplay.Use.performed -= OnButtonPerformed;
        controllerActions.Gameplay.Reload.performed -= OnButtonPerformed;
        controllerActions.Gameplay.Move.performed -= OnMovePerformed;
        controllerActions.Gameplay.Move.canceled -= OnMovePerformed;
    }

    private void OnEnable()
    {
        controllerActions.Enable();
    }

    private void OnDisable()
    {
        controllerActions.Disable();
    }

    private void OnButtonPerformed(InputAction.CallbackContext context)
    {
        if (context.control.device.deviceId == PlayerManager.Instance.PlayerManagerData.GetCurrentPlayerId())
        {
            OnHitEvent?.Invoke(this, context);
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        if (context.control.device.deviceId == PlayerManager.Instance.PlayerManagerData.GetCurrentPlayerId())
        {
            float moveValue = context.ReadValue<float>();

            if (canSelect && Mathf.Abs(moveValue) > thresholdTriggerMove)
            {
                canSelect = false;

                OnHitEvent?.Invoke(this, context);
            }

            if (!canSelect && Mathf.Abs(moveValue) < thresholdResetMove) // Reset detection when joystick is near neutral
            {
                canSelect = true;
            }
        }
        
    }
}
