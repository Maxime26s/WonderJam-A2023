using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : Singleton<BallController>
{

    public List<BaseEffect> effects;

    public void AddEffect(BaseEffect e)
    {
        effects.Add(e);
    }

    void Tick()
    { 
        foreach(BaseEffect e in effects)
        {
            e.Tick();
        }

        // Delete all effects that are over
        effects.RemoveAll(e =>
        {
            if (e.isOver)
            {
                Destroy(e);
                return true;
            }
            return false;
        });
    }
}
