using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSelect : MonoBehaviour
{
	public int maxPlayers = 4;
	public string nextSceneName;
    private ControllerActions controllerActions;
	[SerializeField]
	private List<int> playersToSpawn;

	private void Awake()
	{
		controllerActions = new ControllerActions();
		controllerActions.Gameplay.Return.performed += OnReturn;
		controllerActions.Gameplay.Use.performed += OnSelect;
	}

	public void ButtonBack()
    {
        SceneLoader.Instance.LoadLevel("MainMenu");
    }

	public void ButtonContinue()
    {
        SceneLoader.Instance.LoadLevel(nextSceneName);
    }

	public void OnReturn(InputAction.CallbackContext context)
	{
		InputDevice device = context.control.device;

		if(playersToSpawn.Contains(device.deviceId))
			playersToSpawn.Remove(device.deviceId);
		else
			ButtonBack();
	}

	public void OnSelect(InputAction.CallbackContext context)
	{
		InputDevice device = context.control.device;

		if(playersToSpawn.Count < maxPlayers)
		{
			if(!playersToSpawn.Contains(device.deviceId))
				playersToSpawn.Add(device.deviceId);
			else
				ButtonContinue();

			PlayerManager.Instance.PlayerManagerData.PlayersToSpawn = playersToSpawn;
		}
	}

	private void OnDestroy()
	{
		controllerActions.Gameplay.Return.performed -= OnReturn;
		controllerActions.Gameplay.Use.performed -= OnSelect;
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
