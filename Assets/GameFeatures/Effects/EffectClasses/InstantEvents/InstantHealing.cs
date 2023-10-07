using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantHealing : InstantEffect
{
    [SerializeField]
    float _healing;

    public float healing { get => _healing; set => _healing = value; }

    public override void Tick()
    {
        tickCountdown--;
        
        if (tickCountdown <= 0)
        {
            PlayerManager.Instance.PlayerManagerData.GetCurrentPlayer().ReceiveHealing(healing);
            isOver = true;
        }
    }
}
