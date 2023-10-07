using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatController : MonoBehaviour
{
    // Singleton instance
    public static BeatController Instance { get; private set; }

    public float startingBPM;
    public float currentBPM;
    public double beatInterval;
    public double lastBeatTime;
    public double offset;
    public double audioLeadInTime = 0.0d;
    public float initialSpeed = 1.0f;

    private AudioSource audioSource;
    private bool shouldChangeSpeed = false;
    private float newSpeed = 1.0f;

    private double nextBeatTime;

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
        startingBPM = startingBPM * initialSpeed;
        audioLeadInTime = audioLeadInTime / initialSpeed;
        InitRhythm();
        IEnumerator DelayStart()
        {
            yield return new WaitForSeconds(0.5f); // Wait for half a second
            StartPlaying();
        }
        StartCoroutine(DelayStart());
    }

    private void Update()
    {
        if (audioSource.isPlaying)
        {
            if (AudioSettings.dspTime >= nextBeatTime - beatInterval / 2)
            {
                OnBeat();

                if (shouldChangeSpeed)
                {
                    shouldChangeSpeed = false;
                    audioSource.pitch = newSpeed;
                    currentBPM = startingBPM * audioSource.pitch;
                    beatInterval = 60.0f / currentBPM;
                }

                // Calculate the next beat time.
                nextBeatTime += beatInterval;
            }
        }
    }

    private void InitRhythm()
    {
        currentBPM = startingBPM;
        audioSource.pitch = initialSpeed;
        audioSource.time = 0.0f;
        beatInterval = 60.0f / currentBPM;
    }

    private void StartPlaying()
    {
        double scheduledStartTime = AudioSettings.dspTime + audioLeadInTime;
        audioSource.PlayScheduled(scheduledStartTime);
        nextBeatTime = scheduledStartTime;
    }

    private void OnBeat()
    {
        // You can still have other logic here if needed
        lastBeatTime = AudioSettings.dspTime + offset;
        print("Beat! " + AudioSettings.dspTime);

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
        audioSource.Stop();
        InitRhythm();
        StartPlaying();
    }

    // Change audio
    public void ChangeAudio(AudioClip clip, int newStartingBPM)
    {
        // Stop the currently playing audio and the associated coroutine
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Set the new audio clip
        audioSource.clip = clip;

        startingBPM = newStartingBPM;

        // Restart the rhythm
        InitRhythm();
        StartPlaying();
    }
}
