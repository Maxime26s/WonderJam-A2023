using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TickBasedEffect : BaseEffect
{
    [SerializeField] int _tickDuration;

    public int TickDuration { get => _tickDuration; set => _tickDuration = value; }
}
