using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickBasedEffectController : BaseEffect
{
    public int tickDuration;

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

    public override void Tick()
    {

    }
}
