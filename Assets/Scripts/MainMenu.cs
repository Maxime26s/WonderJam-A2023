using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ButtonStart()
    {
        SceneLoader.Instance.LoadLevel("PlayerSelect");
    }

	public void ButtonInstructions()
    {
        SceneLoader.Instance.LoadLevel("Instructions");
    }

	public void ButtonDeck()
    {
        SceneLoader.Instance.LoadLevel("DeckChecker");
    }

    public void ButtonOptions()
    {
        SceneLoader.Instance.LoadLevel("Options");
    }

    public void ButtonExit()
    {
        SceneLoader.Instance.QuitGame();
    }
}
