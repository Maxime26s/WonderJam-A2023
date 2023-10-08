using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu(fileName = "Instant Draw", menuName = "Effect/Instant Draw")]
public class InstantDraw5 : InstantEffect
{
    public override void Tick()
    {
        PlayerManager.Instance.PlayerManagerData.GetCurrentPlayer().DrawHand();
        isOver = true;
    }

    public override EffectInfo GetInfo()
    {
        return new EffectInfo(EffectType.Blank, 0, TimeEffectType.Tick, tickCountdown);
    }
}
