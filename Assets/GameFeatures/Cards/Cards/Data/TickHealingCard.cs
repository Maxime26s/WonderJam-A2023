using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickHealingCard : Card
{
    [SerializeField]
    float healingValue;

    [SerializeField]
    int tickDuration;

    public TickHealing prefab;

    public void PlayCard()
    {
        /*
        TickHealing tickHealing = Instantiate(prefab);
        tickHealing.healing = healingValue;
        tickHealing.tickDuration = tickDuration;
        Ball.Instance.AddEffect(tickHealing);
        */
    }
}
