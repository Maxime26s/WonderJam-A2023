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

    public AudioSource beatAudioSource;
    public AudioSource melodyAudioSource;
    private bool shouldChangeSpeed = false;

    private double nextBeatTime;
    public int beatCount = 0;
    public bool shouldSpawnBeat = false;

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

        SetupAudioSource();
    }

    private void Start()
    {
        if (!shouldStartOnAwake)
            return;

        IEnumerator DelayStart()
        {
            yield return new WaitForSeconds(0.5f); // Wait for half a second
            StartPlaying(true);
        }
        StartCoroutine(DelayStart());
    }

    private void Update()
    {
        if (beatAudioSource.isPlaying)
        {
            if (AudioSettings.dspTime >= nextBeatTime - track.GetBeatInterval() / 2)
            {
                OnBeat();

                if (shouldChangeSpeed)
                {
                    shouldChangeSpeed = false;
                    track.Speed = newSpeed;
                    beatAudioSource.pitch = (float)track.Speed;
                    melodyAudioSource.pitch = (float)track.Speed;
                }

                // Calculate the next beat time.
                nextBeatTime += track.GetBeatInterval();
            }
        }
    }

    public void SetupAudioSource()
    {
        beatAudioSource.clip = track.beatClip;
        beatAudioSource.pitch = (float)track.Speed;
        beatAudioSource.time = 0.0f;

        melodyAudioSource.clip = track.melodyClip;
        melodyAudioSource.pitch = (float)track.Speed;
        melodyAudioSource.time = 0.0f;
    }

    public void StartPlaying(bool shouldEnableBeat = false)
    {
        print("start playing");
        if (shouldEnableBeat)
        {
            EnableBeatSpawn();
        }
        else
        {
            DisableBeatSpawn();
        }

        beatCount = 0;
        double scheduledStartTime = AudioSettings.dspTime + track.GetAudioLeadInTime();
        beatAudioSource.PlayScheduled(scheduledStartTime);
        melodyAudioSource.PlayScheduled(scheduledStartTime);
        nextBeatTime = scheduledStartTime;
    }

    public void StopPlaying()
    {
        beatAudioSource.Stop();
        melodyAudioSource.Stop();
    }

    private void OnBeat()
    {
        if(shouldSpawnBeat == false)
        {
            return;
        }

        // You can still have other logic here if needed
        lastBeatTime = AudioSettings.dspTime + track.GetOffset();

        // Invoke the event
        EarlyOnBeatEvent?.Invoke();
        // Invoke the event
        OnBeatEvent?.Invoke();
        
        beatCount++;
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
        beatAudioSource.Stop();
        melodyAudioSource.Stop();
        SetupAudioSource();
    }

    // Change audio
    public void ChangeTrack(MusicData newTrack)
    {
        // Stop the currently playing audio and the associated coroutine
        if (beatAudioSource.isPlaying)
        {
            beatAudioSource.Stop();
            melodyAudioSource.Stop();
        }

        // Set the new audio clip
        track = newTrack;

        // Restart the rhythm
        ResetAudioSource();
    }

    public void FadeOutMelody(float end, float duration)
    {
        IEnumerator FadeOutCoroutine()
        {
            DisableBeatSpawn();

            float startTime = Time.time;
            float currentVolume = melodyAudioSource.volume;

            while (Time.time - startTime < duration)
            {
                melodyAudioSource.volume = Mathf.Lerp(currentVolume, end, (Time.time - startTime) / duration);
                yield return null;
            }

            melodyAudioSource.volume = 0;
        }
        StartCoroutine(FadeOutCoroutine());
    }

    public void FadeInMelody(float end, float duration)
    {
        IEnumerator FadeInCoroutine()
        {
            EnableBeatSpawn();

            float startTime = Time.time;
            float currentVolume = melodyAudioSource.volume;

            while (Time.time - startTime < duration)
            {
                melodyAudioSource.volume = Mathf.Lerp(currentVolume, end, (Time.time - startTime) / duration);
                yield return null;
            }

            melodyAudioSource.volume = 1;
        }
        StartCoroutine(FadeInCoroutine());
    }

    public void EnableBeatSpawn()
    {
        shouldSpawnBeat = true;
    }

    public void DisableBeatSpawn()
    {
        shouldSpawnBeat = false;
        beatCount = 0;
    }
}
