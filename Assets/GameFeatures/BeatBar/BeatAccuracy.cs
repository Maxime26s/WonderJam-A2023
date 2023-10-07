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

    public delegate void HitAction();
    public event HitAction OnHitEvent;

    private void Awake()
    {
        controllerActions = new ControllerActions();

        controllerActions.Gameplay.Select.performed += OnSelectPerformed;
        controllerActions.Gameplay.Move.performed += OnMovePerformed;
        controllerActions.Gameplay.Move.canceled += OnMovePerformed;
    }

    private void OnDestroy()
    {
        controllerActions.Gameplay.Select.performed -= OnSelectPerformed;
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

    private void OnSelectPerformed(InputAction.CallbackContext context)
    {
        OnHitEvent?.Invoke();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        float moveValue = context.ReadValue<float>();

        if (canSelect && Mathf.Abs(moveValue) > thresholdTriggerMove)
        {
            canSelect = false;

            OnHitEvent?.Invoke();
        }

        if (!canSelect && Mathf.Abs(moveValue) < thresholdResetMove) // Reset detection when joystick is near neutral
        {
            canSelect = true;
        }
    }
}
