using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Beat : MonoBehaviour
{
    public float startTime;
    public SineFunction positionFunction;

    private float centerX;
    public float diffX;

    public void Init(SineFunction positionFunction)
    {
        this.positionFunction = positionFunction;
        startTime = Time.time;
        centerX = positionFunction(BeatController.Instance.beatInterval).x;
        diffX = Mathf.Abs(positionFunction(0).x - centerX);
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = positionFunction(Time.time - startTime);
        transform.position = position;
        transform.localScale = Vector3.one * Mathf.Clamp01(1.5f - Mathf.Abs(position.x - centerX)/diffX);

        if(position.x > 10)
        {
            Destroy(gameObject);
        }
    }
}
