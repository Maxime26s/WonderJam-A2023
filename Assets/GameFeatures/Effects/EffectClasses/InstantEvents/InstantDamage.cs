using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDamage : InstantEffect
{
    [SerializeField]
    float _damage;

    public float damage{ get => _damage; set => _damage = value; }

    public override void Tick()
    {
        tickCountdown--;

        if (tickCountdown <= 0)
        {
            PlayerManager.Instance.PlayerManagerData.GetCurrentPlayer().TakeDamage(damage);
            isOver = true;
        }
    }
}
