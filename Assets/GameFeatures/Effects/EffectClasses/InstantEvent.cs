using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InstantEvent : BaseEffect
{
    [SerializeField]
    int _tickCountdown;

    public int tickCountdown { get => _tickCountdown; set => _tickCountdown = value; }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
