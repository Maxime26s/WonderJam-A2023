using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectsInfoUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI number;
    [SerializeField] public Image effectIcon;
    [SerializeField] public TextMeshProUGUI duration;

    [SerializeField] public List<Sprite> icons;
    [SerializeField] public List<Sprite> timeIcons;

    public void SetInfo(EffectInfo info)
    {
        switch (info.effectType) 
        {
            case EffectType.Healing:
                effectIcon.sprite = icons[0];
                break;
            case EffectType.Mana:
                effectIcon.sprite = icons[1];
                break;
            case EffectType.Damage:
                effectIcon.sprite = icons[2];
                break;
            case EffectType.Multiplier:
                effectIcon.sprite = icons[3];
                break;
        }

        switch (info.timeEffectType)
        {
            case TimeEffectType.Instant:
                duration.text = $"dans {info.duration}";
                break;
            case TimeEffectType.Tick:
                duration.text = $"pendant {info.duration}";
                break;
        }

        if (info.effectType == EffectType.Multiplier)
        {
            number.text = $"x{info.mainNumber}";
        }
        else
        {
            number.text = info.mainNumber.ToString();
        }
    }
}
