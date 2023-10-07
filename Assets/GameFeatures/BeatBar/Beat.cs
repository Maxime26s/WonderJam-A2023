using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Beat : MonoBehaviour
{
    public double startTime;
    public SineFunction positionFunction;

    private float centerX;
    public float diffX;

    public void Init(SineFunction positionFunction)
    {
        this.positionFunction = positionFunction;
        startTime = BeatController.Instance.lastBeatTime;
        centerX = positionFunction(BeatController.Instance.beatInterval/2).x;
        diffX = Mathf.Abs(centerX - positionFunction(0).x);
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        double timeDiff = AudioSettings.dspTime - startTime;

        Vector2 position = positionFunction(timeDiff);
        transform.position = position;
        transform.localScale = Vector3.one * Mathf.Clamp01(1.5f - Mathf.Clamp01(Mathf.Abs(centerX - position.x) / diffX));

        if (position.x >= centerX + diffX)
        {
            Destroy(gameObject);
        }
    }
}
