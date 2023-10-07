using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{
	public int maxPlayers = 4;
	public string nextSceneName;
    private ControllerActions controllerActions;
	[SerializeField]
	private List<int> playersToSpawn;
	[SerializeField]
	private List<GameObject> playerSprites;

	private void Awake()
	{
		controllerActions = new ControllerActions();
		controllerActions.Gameplay.Return.performed += OnReturn;
		controllerActions.Gameplay.Use.performed += OnSelect;
		BeatController.Instance.OnBeatEvent += OnBeat;

		playersToSpawn = new List<int>(4);

		for(int i = 0; i < 4; i++)
			playersToSpawn.Add(-1);
	}

	public void ButtonBack()
    {
        SceneLoader.Instance.LoadLevel("MainMenu");
    }

	public void ButtonContinue()
    {
        SceneLoader.Instance.LoadLevel(nextSceneName);
    }

	public void OnSelect(InputAction.CallbackContext context)
	{
		InputDevice device = context.control.device;

		if(!playersToSpawn.Contains(device.deviceId))
		{
			int index = playersToSpawn.FindIndex(x => x == -1);
			playersToSpawn[index] = device.deviceId;

			Image image = playerSprites[index].GetComponent<Image>();
			image.enabled = true;
		}
		else
			ButtonContinue();

		PlayerManager.Instance.PlayerManagerData.PlayersToSpawn = playersToSpawn;
	}

	public void OnReturn(InputAction.CallbackContext context)
	{
		InputDevice device = context.control.device;

		if(playersToSpawn.Contains(device.deviceId))
		{
			int index = playersToSpawn.FindIndex(x => x == device.deviceId);
			playersToSpawn[index] = -1;

			for(int i = 0; i < playerSprites.Count; i++)
			{
				Image image = playerSprites[i].GetComponent<Image>();

				if(playersToSpawn[i] == -1)
					image.enabled = false;
			}
		}
		else
			ButtonBack();
	}

	private void OnBeat()
	{
		
		for(int i = 0; i < playerSprites.Count; i++)
		{
			Animator animator = playerSprites[i].GetComponent<Animator>();

			//animator.speed = BeatController.Instance.currentBPM / 60;
			animator.Play("PlayerSelectDance");
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
