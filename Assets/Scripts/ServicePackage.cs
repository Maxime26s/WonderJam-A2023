using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServicePackage : MonoBehaviour
{
    public static ServicePackage Instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}
