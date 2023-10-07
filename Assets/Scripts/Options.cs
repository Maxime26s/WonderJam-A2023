using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField]
    private Image muteButton;
    [SerializeField]
    private Sprite mutedSprite;
    [SerializeField]
    private Sprite unmutedSprite;
    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ButtonBack()
    {
        SceneLoader.Instance.LoadLevel("MainMenu");
    }

    public void ToggleMute()
    {
        if (SceneLoader.Instance.ToggleMute())
            muteButton.sprite = mutedSprite;
        else
            muteButton.sprite = unmutedSprite;
    }
}
