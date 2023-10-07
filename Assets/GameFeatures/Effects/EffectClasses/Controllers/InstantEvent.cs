using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InstantEvent : BaseEffect
{
    public int tickCountdown;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        base.Update();
    }
}
