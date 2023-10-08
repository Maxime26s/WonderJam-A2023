using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Instant Damage", menuName = "Effect/Instant Damage")]
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
            Ball.Instance.pendingDamage += damage;
            isOver = true;
        }
    }

    public override EffectInfo GetInfo()
    {
        return new EffectInfo(EffectType.Damage, damage, TimeEffectType.Instant, tickCountdown);
    }
}
