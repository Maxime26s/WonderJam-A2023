using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    private ControllerActions controllerActions;
	public GameObject player;
	public TextMeshProUGUI text;

	private void Awake()
	{
        controllerActions = new ControllerActions();
    }

	private void Start()
	{
		BeatController.Instance.OnBeatEvent += OnBeat;

		int winnerId = GameManager.Instance.winnerPlayer;
		if(winnerId != -1)
		{
			Image image = player.GetComponent<Image>();
			switch(winnerId)
			{
				case 0:
					image.color = Color.red;
				break;

				case 1:
					image.color = Color.green;
				break;

				case 2:
					image.color = Color.blue;
				break;

				case 3:
					image.color = Color.yellow;
				break;
			}

			text.text = $"Joueur {winnerId + 1} gagne!";
		}
	}

	public void ReturnToMainMenu()
	{
		SceneLoader.Instance.LoadLevel("MainMenu");
	}

	private void OnEnable()
    {
        controllerActions.Enable();
    }

    private void OnDisable()
    {
        controllerActions.Disable();
    }

	private void OnBeat()
	{
		Animator animator = player.GetComponent<Animator>();
		
		animator.speed = (float)BeatController.Instance.track.GetBPM() / 120f;
		animator.Play("PlayerSelectDance");
	}
}
