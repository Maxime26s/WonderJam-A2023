using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickHealing : TickBasedEffect
{
    [SerializeField]
    float _healing;

    public float healing { get => _healing; set => _healing = value; }


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
        PlayerManager pm = (PlayerManager) Singleton.Instance;
        pm.PlayerManagerData.GetCurrentPlayer().ReceiveHealing(healing);

        if (tickDuration <= 0 )
        {
            isOver = true;
        }
    }
}
