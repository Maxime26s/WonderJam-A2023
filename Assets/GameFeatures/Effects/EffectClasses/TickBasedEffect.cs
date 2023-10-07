using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TickBasedEffect : BaseEffect
{
    [SerializeField]
    int _tickDuration;

    public int tickDuration { get => _tickDuration; set => _tickDuration = value; }
}
