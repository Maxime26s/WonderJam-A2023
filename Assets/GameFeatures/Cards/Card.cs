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
    [SerializeField] Sprite _cardImage;
    [SerializeField] string _description;

    [SerializeField] private List<BaseEffect> cardEffects;
    public CardType CardType { get => _cardType; set => _cardType = value; }
    public string CardName { get => _cardName; set => _cardName = value; }
    public Sprite CardImage { get => _cardImage; set => _cardImage = value; }
    public string Description { get => _description; set => _description = value; }

    public void PlayCard()
    {
        foreach (BaseEffect effect in cardEffects)
        {
            Ball.Instance.AddEffect(effect);
        }
    }

    public Card(Card template)
    {

    }
}
