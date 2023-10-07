using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public List<BaseEffect> effects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
