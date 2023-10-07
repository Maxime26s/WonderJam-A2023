using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Healing,
    Damage,
    Reverse
}

public abstract class BaseCard : MonoBehaviour
{
    [SerializeField]
    string _cardName;
    [SerializeField]
    CardType _cardType;
    [SerializeField]
    string _description;

    public string cardName { get => _cardName; set => _cardName = value; }
    public CardType cardType { get => _cardType; set => _cardType = value; }
    public string description { get => _description; set => _description = value; }

    public abstract void PlayCard();
}
