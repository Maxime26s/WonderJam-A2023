using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private float startCenterDistance;

    private SineFunction sineFunction;


    // Start is called before the first frame update
    void Start()
    {
        startPos = spawnTransform.position;
        centerPos = centerTransform.position;

        startCenterDistance = Mathf.Abs(centerPos.x - startPos.x);

        sineFunction = ExtractSineFunction(startPos, centerPos);

        BeatController.Instance.OnBeatEvent += OnBeat;
        beatAccuracy.OnHitEvent += OnHit;
    }

    private void OnDestroy()
    {
        while (beats.Count != 0)
        {
            if (beats[0] != null)
                Destroy(beats[0]);
            beats.RemoveAt(0);
        }

        BeatController.Instance.OnBeatEvent -= OnBeat;
        beatAccuracy.OnHitEvent -= OnHit;
    }

    void Update()
    {
        if (beats[0]?.transform.position.x - centerPos.x >= startCenterDistance / 3.0f)
        {
            beats.RemoveAt(0);
            OnHitEvent?.Invoke(this, new HitEventArgs(new InputAction.CallbackContext(), HitResult.Miss));
        }
    }

    private static SineFunction ExtractSineFunction(Vector2 start, Vector2 center)
    {
        int sign = (int)Mathf.Sign(center.y - start.y);
        float A = sign * Mathf.Abs(center.y - start.y);
        double P = 4.0d * BeatController.Instance.beatInterval;
        double f = 1.0d / P;

        print(BeatController.Instance.beatInterval);
        print(P);
        print(f);

        return (t) =>
        {
            Vector2 position = Vector2.zero;

            position.x = Mathf.Lerp(start.x, start.x + 2 * (center.x - start.x), (float)(t / (BeatController.Instance.beatInterval * 2)));
            position.y = A * Mathf.Sin(2 * Mathf.PI * (float)(f * t)) + start.y;

            return position;
        };
    }

    private void OnBeat()
    {
        if (beatToSkip > 0)
        {
            beatToSkip--;
            return;
        }

        var go = Instantiate(beatPrefab, spawnTransform.position, Quaternion.identity, null);
        go.GetComponent<Beat>().Init(sineFunction);

        beats.Add(go);
    }

    private void OnHit(object sender, InputAction.CallbackContext context)
    {
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
            OnHitEvent?.Invoke(this, new HitEventArgs(context, HitResult.Perfect));
        }
        else if (score > 0.925f)
        {
            print("Good!");
            OnHitEvent?.Invoke(this, new HitEventArgs(context, HitResult.Good));
        }
        else if (score > 0.85f)
        {
            print("Bad!");
            OnHitEvent?.Invoke(this, new HitEventArgs(context, HitResult.Bad));
        }
        else
        {
            print("Miss!");
            OnHitEvent?.Invoke(this, new HitEventArgs(context, HitResult.Miss));
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
    public InputAction.CallbackContext Context { get; }

    public HitEventArgs(InputAction.CallbackContext context, HitResult result)
    {
        Context = context;
        Result = result;
    }
}