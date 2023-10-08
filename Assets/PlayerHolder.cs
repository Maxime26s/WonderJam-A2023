using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHolder : MonoBehaviour
{
    public bool isMoving = false;
    public Vector2 targetPosition;
    public Vector2 targetScale;
    public float duration;
    public float timeElapsed;
    public float startTime;

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            timeElapsed = Time.time - startTime;
            float percentageComplete = timeElapsed / duration;
            transform.position = Vector2.Lerp(transform.position, targetPosition, percentageComplete);
            transform.localScale = Vector2.Lerp(transform.localScale, targetScale, percentageComplete);

            if (timeElapsed >= duration)
            {
                transform.position = targetPosition;
                transform.localScale = targetScale;
                isMoving = false;
            }
        }
    }

    public void SetTarget(Vector2 position, Vector2 scale, float duration)
    {
        targetPosition = position;
        targetScale = scale;
        this.duration = duration;
    }

    public void Move()
    {
        startTime = Time.time;
        isMoving = true;
    }
}
