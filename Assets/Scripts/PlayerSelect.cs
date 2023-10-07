using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSelect : MonoBehaviour
{
    private ControllerActions controllerActions;

	private void Awake()
	{
		controllerActions = new ControllerActions();
		controllerActions.Gameplay.Return.performed += OnReturn;
	}

	public void ButtonBack()
    {
        SceneLoader.Instance.LoadLevel("MainMenu");
    }

	private void OnDestroy()
	{
		controllerActions.Gameplay.Return.performed -= OnReturn;
	}

	private void OnEnable()
    {
        controllerActions.Enable();
    }

    private void OnDisable()
    {
        controllerActions.Disable();
    }

	public void OnReturn(InputAction.CallbackContext context)
	{
		ButtonBack();
	}
}
