using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : Singleton<Ball>
{
    public List<BaseEffect> BaseEffectsForTesting;
    public List<BaseEffect> effects;

    private void Start()
    {
        BeatController.Instance.OnBeatEvent += Tick;
        foreach (var e in BaseEffectsForTesting) 
        {
            effects.Add(Instantiate(e));
        }
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
        effects.RemoveAll(e => e.isOver);
    }
}
