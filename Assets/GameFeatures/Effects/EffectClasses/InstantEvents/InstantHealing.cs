using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu(fileName = "Instant Healing", menuName = "Effect/Instant Healing")]
public class InstantHealing : InstantEffect
{
    [SerializeField] float _healing;
    public float healing { get => _healing; set => _healing = value; }

    public override void Tick()
    {
        tickCountdown--;
        
        if (tickCountdown <= 0)
        {
            Ball.Instance.pendingHealing += healing;
            isOver = true;
        }
    }

    public override EffectInfo GetInfo()
    {
        return new EffectInfo(EffectType.Healing, healing, TimeEffectType.Instant, tickCountdown);
    }
}
