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
            if (_effectType == EffectType.Cleanse)
            {
                Ball.Instance.effects.Clear();
            }
            else
            {
                for (int i = Ball.Instance.effects.Count - 1; i >= 0; i--)
                {
                    BaseEffect effect = Ball.Instance.effects[i];
                    if (effect.GetInfo().effectType == _effectType)
                    {
                        Ball.Instance.effects.RemoveAt(i);
                    }
                }
            }
            isOver = true;
        }
    }

    public override EffectInfo GetInfo()
    {
        return new EffectInfo(EffectType.Cleanse, 0, TimeEffectType.Instant, tickCountdown);
    }
}
