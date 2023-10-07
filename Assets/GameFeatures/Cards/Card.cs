using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Healing,
    Damage,
    Reverse
}

[CreateAssetMenu(fileName = "Card", menuName = "Card", order = 1)]

public class Card : ScriptableObject
{
    [SerializeField] CardType _cardType;

    [SerializeField] string _cardName;
    [SerializeField] string _description;

    [SerializeField] private List<BaseEffect> cardEffects;
    public string cardName { get => _cardName; set => _cardName = value; }
    public CardType cardType { get => _cardType; set => _cardType = value; }
    public string description { get => _description; set => _description = value; }

    public void PlayCard()
    {
        foreach (BaseEffect effect in cardEffects)
        {
            Ball.Instance.AddEffect(effect);
        }
    }
}
