using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu(fileName = "Instant Damage Mult", menuName = "Effect/Instant Damage Mult")]
public class InstantDamageMult : InstantEffect
{
    [SerializeField] float _damageMult;
    public float damageMult { get => _damageMult; set => _damageMult = value; }

    bool hasTickedOnce = false;
    float addedValue = 0;


    public override void Tick()
    {
        if (!hasTickedOnce)
        {
            addedValue = Ball.Instance.damageMultiplier * _damageMult - Ball.Instance.damageMultiplier;
            Ball.Instance.damageMultiplier += addedValue;
            hasTickedOnce = true;
        }
        tickCountdown--;

        if (tickCountdown <= 0)
        {
            Ball.Instance.damageMultiplier -= addedValue;
            isOver = true;
        }
    }

    public override EffectInfo GetInfo()
    {
        return new EffectInfo(EffectType.Multiplier, damageMult, TimeEffectType.Tick, tickCountdown);
    }
}
