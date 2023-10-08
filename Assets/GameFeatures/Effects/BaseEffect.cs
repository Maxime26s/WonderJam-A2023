using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    Healing,
    Mana,
    Damage,
    Multiplier
}

public enum TimeEffectType
{
    Tick,
    Instant
}

public class EffectInfo
{
    public EffectType effectType = EffectType.Healing;
    public float mainNumber = 0;
    public TimeEffectType timeEffectType = TimeEffectType.Tick;
    public int duration = 0;

    public EffectInfo(EffectType _effectType, float _mainNumber, TimeEffectType _timeEffectType, int _duration) 
    { 
        effectType = _effectType; mainNumber = _mainNumber; timeEffectType = _timeEffectType; duration = _duration;
    }
}

public abstract class BaseEffect : ScriptableObject
{
    [SerializeField] public bool isOver = false;
    
    public abstract void Tick();
    public abstract EffectInfo GetInfo();
}
