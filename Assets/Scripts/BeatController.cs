using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatController : MonoBehaviour
{
    // Singleton instance
    public static BeatController Instance { get; private set; }

    public float startingBPM;
    public float currentBPM;
    public float beatInterval;
    public float lastBeatTime;

    private AudioSource audioSource;
    private Coroutine beatCoroutine;
    private bool shouldChangeSpeed = false;
    private float newSpeed = 1.0f;

    // Event to subscribe to
    public delegate void BeatAction();
    public event BeatAction OnBeatEvent;

    // Event to subscribe to
    public event BeatAction FixedOnBeatEvent;

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        InitRhythm();
    }

    private void InitRhythm()
    {
        currentBPM = startingBPM;
        audioSource.pitch = 1.0f;
        audioSource.time = 0.0f;
        audioSource.Play();
        beatInterval = 60.0f / currentBPM;
        beatCoroutine = StartCoroutine(BeatTick());
    }

    private IEnumerator BeatTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(beatInterval);
            if (shouldChangeSpeed)
            {
                shouldChangeSpeed = false;
                audioSource.pitch = newSpeed;
                currentBPM = startingBPM * audioSource.pitch;
                beatInterval = 60.0f / currentBPM;
            }

            OnBeat();
        }
    }

    private void OnBeat()
    {
        // You can still have other logic here if needed
        //Debug.Log("Beat at BPM: " + currentBPM);
        lastBeatTime = Time.time;

        // Invoke the event
        FixedOnBeatEvent?.Invoke();
        // Invoke the event
        OnBeatEvent?.Invoke();

    }

    // External function to modify speed
    public void SetSpeed(float speedCoefficient)
    {
        newSpeed = speedCoefficient;
        shouldChangeSpeed = true;
    }

    // Reset the coroutine
    public void Restart()
    {
        if (beatCoroutine != null)
        {
            StopCoroutine(beatCoroutine);
            audioSource.Stop();
            InitRhythm();
        }
    }

    // Change audio
    public void ChangeAudio(AudioClip clip, int newStartingBPM)
    {
        // Stop the currently playing audio and the associated coroutine
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        if (beatCoroutine != null)
        {
            StopCoroutine(beatCoroutine);
        }

        // Set the new audio clip
        audioSource.clip = clip;

        startingBPM = newStartingBPM;

        // Restart the rhythm
        InitRhythm();
    }
}
