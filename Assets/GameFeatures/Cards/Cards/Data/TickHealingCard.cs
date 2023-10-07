using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickHealingCard : BaseCard
{
    [SerializeField]
    float healingValue;

    [SerializeField]
    int tickDuration;

    public TickHealing prefab;

    public override void PlayCard()
    {
        TickHealing tickHealing = Instantiate(prefab);
        tickHealing.healing = healingValue;
        tickHealing.tickDuration = tickDuration;
        BallController.Instance.AddEffect(tickHealing);
    }
}
