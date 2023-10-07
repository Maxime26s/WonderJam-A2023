using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI _cardName = null;
    [SerializeField] public Image _cardImage = null;
    [SerializeField] public TextMeshProUGUI _cardDescription = null;

    public void SetupCardUI(Card card)
    {
        _cardName.text = card.CardName;
        _cardImage.sprite = card.CardImage;
        _cardDescription.text = card.Description;
    }
}
