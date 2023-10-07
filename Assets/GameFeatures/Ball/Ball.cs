using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Singleton<Ball>
{

    public List<BaseEffect> effects;

    private void Start()
    {
        BeatController.Instance.OnBeatEvent += Tick;
    }

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
