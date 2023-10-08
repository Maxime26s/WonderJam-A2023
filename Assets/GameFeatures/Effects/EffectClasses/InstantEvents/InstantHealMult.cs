using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu(fileName = "Instant Healing Mult", menuName = "Effect/Instant Healing Mult")]
public class InstantHealMult : MultiplierEffect
{
    [SerializeField] float _healingMult;
    public float healingMult { get => _healingMult; set => _healingMult = value; }

    bool hasTickedOnce = false;
    float addedValue = 0;


    public override void Tick()
    {
        if (!hasTickedOnce)
        {
            addedValue = Ball.Instance.healingMultiplier * _healingMult - Ball.Instance.healingMultiplier;
            Ball.Instance.healingMultiplier += addedValue;
            hasTickedOnce = true;
        }
        tickCountdown--;
        
        if (tickCountdown <= 0)
        {
            Ball.Instance.healingMultiplier -= addedValue;
            isOver = true;
        }
    }

    public override EffectInfo GetInfo()
    {
        return new EffectInfo(EffectType.Multiplier, healingMult, TimeEffectType.Tick, tickCountdown);
    }
}
