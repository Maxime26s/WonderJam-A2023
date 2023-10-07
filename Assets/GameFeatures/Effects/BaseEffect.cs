using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEffect
{
    [SerializeField]
    bool _isOver = false;

    public bool isOver { get => _isOver; set => _isOver = value; }

    public abstract void Tick();
}
