using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BeatBar : MonoBehaviour
{
    public event EventHandler<HitEventArgs> OnHitEvent;

    [SerializeField]
    public int baseBeatToSkip = 6;

    public int beatToSkip;

    public Transform spawnTransform;
    public Transform centerTransform;

    public GameObject beatPrefab;
    public BeatAccuracy beatAccuracy;

    public List<GameObject> beats = new List<GameObject>();

    private float startCenterDistance;

    private SineFunction sineFunction;

    int beatsInstantiated;


    // Start is called before the first frame update
    void Start()
    {
        startCenterDistance = Mathf.Abs(centerTransform.position.x - spawnTransform.position.x);

        sineFunction = ExtractSineFunction(spawnTransform.position, centerTransform.position);

        BeatController.Instance.OnBeatEvent += OnBeat;
        beatAccuracy.OnHitEvent += OnHit;
    }

    private void OnDestroy()
    {
        BeatController.Instance.OnBeatEvent -= OnBeat;
        beatAccuracy.OnHitEvent -= OnHit;
    }

    void Update()
    {
        if(beats.Count != 0)
        {
            if (beats[0]?.transform.position.x - centerTransform.position.x >= startCenterDistance / 3.0f)
            {
                beats.RemoveAt(0);
                OnHitEvent?.Invoke(this, new HitEventArgs(new InputAction.CallbackContext(), HitResult.Miss));
            }
        }
    }

    private static SineFunction ExtractSineFunction(Vector2 start, Vector2 center)
    {
        int sign = (int)Mathf.Sign(center.y - start.y);
        float A = sign * Mathf.Abs(center.y - start.y);
        double P = 4.0d * BeatController.Instance.track.GetBeatInterval();
        double f = 1.0d / P;

        return (t) =>
        {
            Vector2 position = Vector2.zero;

            position.x = Mathf.Lerp(start.x, start.x + 2 * (center.x - start.x), (float)(t / (BeatController.Instance.track.GetBeatInterval() * 2)));
            position.y = A * Mathf.Sin(2 * Mathf.PI * (float)(f * t)) + start.y;

            return position;
        };
    }

    private void OnBeat()
    {
        if(BeatController.Instance.shouldSpawnBeat == false)
        {
            return;
        }

        if(BeatController.Instance.beatCount == 0)
        {
            beatToSkip = baseBeatToSkip;
            beatsInstantiated = 0;
        }
        if (beatToSkip > 0)
        {
            beatToSkip--;
            return;
        }

        if (beatsInstantiated >= Ball.Instance.baseActionPoints) return;

        var go = Instantiate(beatPrefab, spawnTransform.position, Quaternion.identity, transform);
        go.GetComponent<Beat>().Init(sineFunction);

        beats.Add(go);
        beatsInstantiated++;
    }

    private void OnHit(object sender, InputAction.CallbackContext context)
    {
        if (beats.Count == 0)
            return;

        float distance = Mathf.Abs(beats[0].transform.position.x - centerTransform.position.x);

        if (distance > startCenterDistance / 2.0f)
        {
            return;
        }

        float score = 1.0f - distance / startCenterDistance;

        if (score > 0.975f)
        {
            OnHitEvent?.Invoke(this, new HitEventArgs(context, HitResult.Perfect));
        }
        else if (score > 0.925f)
        {
            OnHitEvent?.Invoke(this, new HitEventArgs(context, HitResult.Good));
        }
        else if (score > 0.85f)
        {
            OnHitEvent?.Invoke(this, new HitEventArgs(context, HitResult.Bad));
        }
        else
        {
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