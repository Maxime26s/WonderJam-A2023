using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickHealing : TickBasedEffect
{
    [SerializeField] private float _healing;

    public float Healing { get => _healing; set => _healing = value; }

    public override void Tick() 
    {
        TickDuration--;
        PlayerManager.Instance.PlayerManagerData.GetCurrentPlayer().ReceiveHealing(_healing);

        if (TickDuration <= 0 )
        {
            isOver = true;
        }
    }
}
