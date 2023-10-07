using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public bool actionPending = false;

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
        if (TryMakeAction())
            SceneManager.LoadScene(levelName);
    }

    public void QuitGame()
    {
        Debug.Log("Can't quit :)");
    }

    public void ToggleMute()
    {
        Debug.Log("This doesn't work :)");
    }

    private bool TryMakeAction()
    {
        if (!actionPending)
            actionPending = true;
        else
            return false;
        return true;
    }
}
