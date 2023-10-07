using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantHealing : TickBasedEffectController
{
    public float healing;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void Tick() 
    {
        tickDuration--;
        // PlayerManager.GetInstance().Heal(healing);

        if (tickDuration <= 0 )
        {
            isOver = true;
        }
    }
}
