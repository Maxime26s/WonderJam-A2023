using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InstantEffect : BaseEffect
{
    [SerializeField]
    int _tickCountdown;

    public int tickCountdown { get => _tickCountdown; set => _tickCountdown = value; }
}
