using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickHealing : TickBasedEffect
{
    [SerializeField]
    float _healing;

    public float healing { get => _healing; set => _healing = value; }

    public override void Tick() 
    {
        tickDuration--;
        PlayerManager.Instance.PlayerManagerData.GetCurrentPlayer().ReceiveHealing(healing);

        if (tickDuration <= 0 )
        {
            isOver = true;
        }
    }
}
