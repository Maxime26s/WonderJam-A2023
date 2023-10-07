using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmController : MonoBehaviour
{
    // Singleton instance
    public static RythmController Instance { get; private set; }

    public float startingBPM;

    private float currentBPM;
    private float beatInterval;
    private AudioSource audioSource;
    private Coroutine beatCoroutine;

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
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InitRhythm();
    }

    private void Update()
    {
        IEnumerator enumerator()
        {
            yield return new WaitForSeconds(5);
            SetSpeed(2);
        }
        StartCoroutine(enumerator());
    }

    private void InitRhythm()
    {
        currentBPM = startingBPM;
        audioSource.pitch = 1.0f;
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
        }
    }

    private void OnBeat()
    {
        // Invoke the event
        OnBeatEvent?.Invoke();

        // You can still have other logic here if needed
        Debug.Log("Beat at BPM: " + currentBPM);
    }

    // External function to modify speed
    public void SetSpeed(float speedCoefficient)
    {
        audioSource.pitch = speedCoefficient;
        currentBPM = startingBPM * audioSource.pitch;
        beatInterval = 60.0f / currentBPM;
    }

    // Reset the coroutine
    public void ResetRhythm()
    {
        if (beatCoroutine != null)
        {
            StopCoroutine(beatCoroutine);
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
