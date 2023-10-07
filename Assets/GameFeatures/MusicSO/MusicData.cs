using UnityEngine;

[CreateAssetMenu(fileName = "MusicData", menuName = "ScriptableObjects/MusicData", order = 1)]
public class MusicData : ScriptableObject
{
    public int bpm;
    public double offset = 0.0d;
    public double audioLeadInTime = 0.0d;
    public AudioClip clip;

    public double Speed { get; set; } = 1.0f;

    public double GetBPM()
    {
        return bpm * Speed;
    }

    public double GetBeatInterval()
    {
        return 60.0d / GetBPM();
    }

    public double GetAudioLeadInTime()
    {
        return audioLeadInTime / Speed;
    }

    public double GetOffset()
    {
        return offset / Speed;
    }

    public void SetBPM(int newBpm)
    {
        Speed = (double)newBpm / (double)bpm;
    }
}
