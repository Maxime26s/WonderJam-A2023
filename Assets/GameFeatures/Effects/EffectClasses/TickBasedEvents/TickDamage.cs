using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickDamage : TickBasedEffect
{
    [SerializeField]
    float _damage;

    public float damage { get => _damage; set => _damage = value; }

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

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    public override void Tick()
    {
        tickDuration--;
        PlayerManager.Instance.PlayerManagerData.GetCurrentPlayer().TakeDamage(damage);

        if (tickDuration <= 0)
        {
            isOver = true;
        }
    }
}