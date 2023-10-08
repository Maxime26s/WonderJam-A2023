using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cleanse Effect", menuName = "Effect/Cleanse Effect")]
public class CleanseEffect : InstantEffect
{
    [SerializeField] private EffectType _effectType;
    public EffectType EffectType { get => _effectType; set => _effectType = value; }

    public override void Tick()
    {
        tickCountdown--;

        if (tickCountdown <= 0)
        {
            Ball.PendingCleanse pendingCleanse = new Ball.PendingCleanse();
            pendingCleanse.effectToCleanse = _effectType;
            Ball.Instance.PendingCleansesList.Add(pendingCleanse);
            isOver = true;
        }
    }

    public override EffectInfo GetInfo()
    {
        return new EffectInfo(EffectType.Cleanse, 0, TimeEffectType.Instant, tickCountdown);
    }
}
