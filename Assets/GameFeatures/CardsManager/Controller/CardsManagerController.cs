using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManagerController : Singleton
{
    [SerializeField]
    CardsManagerData _cardsManagerData;
    int selectedCard = -1;

    public CardsManagerData cardsManagerData { get { return _cardsManagerData; } }

    private void RenderCards()
    {
        List<BaseCard> cards = cardsManagerData.player.GetHand();
        // TODO: place cards sur le jeu genre
        // Cards Placeholder, juste changer les infos avec celles de la carte
    }

    public void UpdatePlayer()
    {
        cardsManagerData.UpdateCurrentPlayer();
        RenderCards();
    }

    public void Tick()
    {
        if (selectedCard != -1)
        {
            List<BaseCard> cards = cardsManagerData.player.GetHand();
            cards[selectedCard].PlayCard();
            cardsManagerData.RemoveCard(selectedCard);
        }
        selectedCard = -1;
    }
}
