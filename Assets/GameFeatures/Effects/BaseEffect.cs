using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    Healing,
    Mana,
    Damage,
    Multiplier,
    Blank,
    Cleanse
}

public enum TimeEffectType
{
    Tick,
    Instant
}

public enum MultiplierType
{
    Damage,
    Healing
}

public class EffectInfo
{
    public EffectType effectType = EffectType.Healing;
    public float mainNumber = 0;
    public TimeEffectType timeEffectType = TimeEffectType.Tick;
    public int duration = 0;

    public MultiplierType multiplierType = MultiplierType.Damage;

    public EffectInfo(EffectType _effectType, float _mainNumber, TimeEffectType _timeEffectType, int _duration) 
    { 
        effectType = _effectType; mainNumber = _mainNumber; timeEffectType = _timeEffectType; duration = _duration;
    }

    public EffectInfo(EffectType _effectType, float _mainNumber, TimeEffectType _timeEffectType, int _duration, MultiplierType _multiplierType)
    {
        effectType = _effectType; mainNumber = _mainNumber; timeEffectType = _timeEffectType; duration = _duration; multiplierType = _multiplierType;
    }
}

public abstract class BaseEffect : ScriptableObject
{
    public bool isOver = false;
    
    public abstract void Tick();
    public abstract EffectInfo GetInfo();
}
