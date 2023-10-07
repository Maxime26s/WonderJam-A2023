using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public bool actionPending = false;
    public bool isMuted = false;

    private static SceneLoader instance;

    public static SceneLoader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SceneLoader>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("SceneLoader");
                    instance = singletonObject.AddComponent<SceneLoader>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    public void LoadLevel(string levelName)
    {
        //if (TryMakeAction())
        SceneManager.LoadScene(levelName);
    }

    public void QuitGame()
    {
        Debug.Log("Can't quit :)");
    }

    public bool ToggleMute()
    {
        isMuted = !isMuted;
        return isMuted;
    }

    private bool TryMakeAction()
    {
        if (actionPending)
            return false;

        actionPending = true;
        return true;
    }
}
