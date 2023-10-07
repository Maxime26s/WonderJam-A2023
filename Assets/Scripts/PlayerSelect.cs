using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSelect : MonoBehaviour
{
	[SerializeField]
	public int maxPlayers = 4;
    private ControllerActions controllerActions;

	private void Awake()
	{
		controllerActions = new ControllerActions();
		controllerActions.Gameplay.Return.performed += OnReturn;
		controllerActions.Gameplay.Select.performed += OnSelect;
	}

	public void ButtonBack()
    {
        SceneLoader.Instance.LoadLevel("MainMenu");
    }

	public void OnReturn(InputAction.CallbackContext context)
	{
		ButtonBack();
	}

	public void OnSelect(InputAction.CallbackContext context)
	{
		Debug.Log("select");
		/* if(PlayerManager.Instance.PlayerManagerData.TotalNbPlayer < maxPlayers)
			PlayerManager.Instance.PlayerManagerData.TotalNbPlayer++; */
	}

	private void OnDestroy()
	{
		controllerActions.Gameplay.Return.performed -= OnReturn;
		controllerActions.Gameplay.Select.performed -= OnSelect;
	}

	private void OnEnable()
    {
        controllerActions.Enable();
    }

    private void OnDisable()
    {
        controllerActions.Disable();
    }
}
