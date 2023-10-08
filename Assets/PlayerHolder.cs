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

    private void Start()
    {
        int sign;
        if (Mathf.Abs(transform.localScale.x) > 1)
            sign = -1;
        else
            sign = 1;

        transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * sign, transform.localScale.y);
    }

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

        int sign;
        if (Mathf.Abs(targetScale.x) > 1)
            sign = -1;
        else
            sign = 1;

        targetScale = new Vector2(Mathf.Abs(targetScale.x) * sign, targetScale.y);

        this.duration = duration;
    }

    public void Move()
    {
        startTime = Time.time;
        isMoving = true;
    }
}
