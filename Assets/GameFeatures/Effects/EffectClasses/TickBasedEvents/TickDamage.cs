using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickDamage : TickBasedEffect
{
    [SerializeField]
    float _damage;

    public float damage { get => _damage; set => _damage = value; }

    public override void Tick()
    {
        TickDuration--;
        PlayerManager.Instance.PlayerManagerData.GetCurrentPlayer().TakeDamage(damage);

        if (TickDuration <= 0)
        {
            isOver = true;
        }
    }
}
