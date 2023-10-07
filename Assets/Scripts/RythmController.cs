using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmController : MonoBehaviour
{
    // Singleton instance
    public static RhythmController Instance { get; private set; }

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
        InitRhythm();
    }

    private void Start()
    {

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
            OnBeat();
            if (shouldChangeSpeed)
            {
                shouldChangeSpeed = false;
                audioSource.pitch = newSpeed;
                currentBPM = startingBPM * audioSource.pitch;
                beatInterval = 60.0f / currentBPM;
            }
        }
    }

    private void OnBeat()
    {
        // Invoke the event
        OnBeatEvent?.Invoke();

        // You can still have other logic here if needed
        Debug.Log("Beat at BPM: " + currentBPM);
        lastBeatTime = Time.time;
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
