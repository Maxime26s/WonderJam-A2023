using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TickBasedEffect : BaseEffect
{
    [SerializeField]
    int _tickDuration;

    public int tickDuration { get => _tickDuration; set => _tickDuration = value; }

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
