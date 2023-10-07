using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardViewer : MonoBehaviour
{
    [SerializeField] private CardUI _cardUIPrefab = null;
    [SerializeField] private Transform _cardUIParent = null;

    private List<CardUI> cardUIs = new List<CardUI>();

    void Start()
    {
        foreach (Card card in CardsManager.Instance.CardDatabase.Cards)
        {
            CardUI cardUI = Instantiate(_cardUIPrefab, _cardUIParent);
            cardUI.SetupCardUI(card);
            cardUIs.Add(cardUI);
        }
    }

    private void DestroyAndClearCardUIList()
    {
        foreach(CardUI card in cardUIs)
        {
            Destroy(card.gameObject);
        }
        cardUIs.Clear();
    }
}
