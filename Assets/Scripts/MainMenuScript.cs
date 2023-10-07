using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ButtonStart()
    {
        SceneLoader.Instance.LoadLevel("0");
    }

    public void ButtonOptions()
    {
        SceneLoader.Instance.LoadLevel("0");
    }

    public void ButtonExit()
    {
        SceneLoader.Instance.QuitGame();
    }

    public void ToggleMute()
    {
        SceneLoader.Instance.ToggleMute();
    }
}
