using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEffect : MonoBehaviour
{
    public bool isOver = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        isOver = false;
        // Subscribe to tick machin truc
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public abstract void Tick();
}
