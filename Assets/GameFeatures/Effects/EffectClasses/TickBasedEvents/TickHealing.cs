using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tick Healing", menuName = "Effect/Tick Healing")]
public class TickHealing : TickBasedEffect
{
    [SerializeField] private float _healing;

    public float Healing { get => _healing; set => _healing = value; }

    public override void Tick()
    {
        TickDuration--;
        Ball.Instance.pendingHealing += Healing;

        if (TickDuration <= 0 )
        {
            isOver = true;
        }
    }
}
