using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatBar : MonoBehaviour
{
    public event EventHandler<HitEventArgs> OnHitEvent;

    public int beatToSkip = 3;

    public Transform spawnTransform;
    public Transform centerTransform;

    public GameObject beatPrefab;
    public BeatAccuracy beatAccuracy;

    public List<GameObject> beats = new List<GameObject>();

    private Vector2 startPos;
    private Vector2 centerPos;

    private SineFunction sineFunction;


    // Start is called before the first frame update
    void Start()
    {
        startPos = spawnTransform.position;
        centerPos = centerTransform.position;
        sineFunction = ExtractSineFunction(startPos, centerPos);

        BeatController.Instance.OnBeatEvent += OnBeat;
        beatAccuracy.OnHitEvent += OnHit;
    }

    private static SineFunction ExtractSineFunction(Vector2 start, Vector2 center)
    {
        float A = Mathf.Abs(center.y - start.y);
        double P = 4 * BeatController.Instance.beatInterval;
        double f = 1 / P;

        return (t) =>
        {
            Vector2 position = Vector2.zero;

            position.x = Mathf.Lerp(start.x, start.x + 2 * (center.x - start.x), (float)(t / (BeatController.Instance.beatInterval * 2)));
            position.y = -A * Mathf.Sin(2 * Mathf.PI * (float)(f * t));

            return position;
        };
    }

    private void OnBeat()
    {
        if(beatToSkip > 0)
        {
            beatToSkip--;
            return;
        }

        var go = Instantiate(beatPrefab, spawnTransform.position, Quaternion.identity, null);
        go.GetComponent<Beat>().Init(sineFunction);

        beats.Add(go);
    }

    private void OnHit()
    {
        float startCenterDistance = Mathf.Abs(centerPos.x - startPos.x);

        if (beats.Count > 1)
        {
            while (beats.Count != 0 && (beats[0] == null || beats[0].transform.position.x - centerPos.x >= startCenterDistance / 3.0f))
            {
                beats.RemoveAt(0);
            }
        }

        if (beats.Count == 0)
            return;

        float distance = Mathf.Abs(beats[0].transform.position.x - centerPos.x);

        if (distance > startCenterDistance / 2.0f)
        {
            return;
        }

        float score = 1.0f - distance / startCenterDistance;

        if (score > 0.975f)
        {
            print("Perfect!");
            OnHitEvent?.Invoke(this, new HitEventArgs(HitResult.Perfect));
        }
        else if (score > 0.925f)
        {
            print("Good!");
            OnHitEvent?.Invoke(this, new HitEventArgs(HitResult.Good));
        }
        else if (score > 0.85f)
        {
            print("Bad!");
            OnHitEvent?.Invoke(this, new HitEventArgs(HitResult.Bad));
        }
        else
        {
            print("Miss!");
            OnHitEvent?.Invoke(this, new HitEventArgs(HitResult.Miss));
        }

        Destroy(beats[0]);
        beats.RemoveAt(0);
    }
}

public delegate Vector2 SineFunction(double x);

public enum HitResult
{
    Miss,
    Bad,
    Good,
    Perfect,
}

public class HitEventArgs : EventArgs
{
    public HitResult Result { get; }

    public HitEventArgs(HitResult result)
    {
        Result = result;
    }
}