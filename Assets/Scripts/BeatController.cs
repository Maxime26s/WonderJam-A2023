using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatController : MonoBehaviour
{
    // Singleton instance
    public static BeatController Instance { get; private set; }

    public double lastBeatTime;

    public MusicData track;
    public double newSpeed = 1.0d;

    public bool shouldStartOnAwake = false;

    private AudioSource audioSource;
    private bool shouldChangeSpeed = false;

    private double nextBeatTime;

    // Event to subscribe to
    public delegate void BeatAction();
    public event BeatAction OnBeatEvent;

    // Event to subscribe to
    public event BeatAction EarlyOnBeatEvent;

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

        track.Speed = newSpeed;

        audioSource = GetComponent<AudioSource>();
        SetupAudioSource();
    }

    private void Start()
    {
        if (!shouldStartOnAwake)
            return;

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
            if (AudioSettings.dspTime >= nextBeatTime - track.GetBeatInterval() / 2)
            {
                OnBeat();

                if (shouldChangeSpeed)
                {
                    shouldChangeSpeed = false;
                    track.Speed = newSpeed;
                    audioSource.pitch = (float)track.Speed;
                }

                // Calculate the next beat time.
                nextBeatTime += track.GetBeatInterval();
            }
        }
    }

    public void SetupAudioSource()
    {
        audioSource.clip = track.clip;
        audioSource.pitch = (float)track.Speed;
        audioSource.time = 0.0f;
    }

    public void StartPlaying()
    {
        double scheduledStartTime = AudioSettings.dspTime + track.GetAudioLeadInTime();
        audioSource.PlayScheduled(scheduledStartTime);
        nextBeatTime = scheduledStartTime;
    }

    public void StopPlaying()
    {
        audioSource.Stop();
    }

    private void OnBeat()
    {
        // You can still have other logic here if needed
        lastBeatTime = AudioSettings.dspTime + track.GetOffset();
        print("Beat! " + AudioSettings.dspTime);

        // Invoke the event
        EarlyOnBeatEvent?.Invoke();
        // Invoke the event
        OnBeatEvent?.Invoke();

    }

    // External function to modify speed
    public void SetSpeed(double newSpeed)
    {
        this.newSpeed = newSpeed;
        shouldChangeSpeed = true;
    }

    // Reset the coroutine
    public void ResetAudioSource()
    {
        audioSource.Stop();
        SetupAudioSource();
    }

    // Change audio
    public void ChangeTrack(MusicData newTrack)
    {
        // Stop the currently playing audio and the associated coroutine
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Set the new audio clip
        track = newTrack;

        // Restart the rhythm
        ResetAudioSource();
    }
}
