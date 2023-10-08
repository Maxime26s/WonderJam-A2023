using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCards
{
    //4 cards in hand. When a card is played, a card is drawn. If a card would be drawn, all cards except the 3 in hand are placed back in the deck.
    //5 cards in hand. One is selected. Playing a card gives a blank card instead of it. 

    [SerializeField]
    private List<Card> deckList;
    private List<Card> deck;
    private Card[] hand = new Card[3];
    public int selectedIndex = 1;

    public PlayerCards()
    {
        deckList = new List<Card>();
        foreach (Card card in CardsManager.Instance.CardDatabase.Cards)
        {
            if (card.CardType != CardType.Blank)
            {
                deckList.Add(card);
                deckList.Add(card);
                deckList.Add(card);
            }
        }
        deck = new List<Card>(deckList);
        Shuffle();
        DrawHand();
    }

    /// <summary>
    /// Replaces the currently selected by a new random card from the deck.
    /// </summary>
    public void DrawCard()
    {
        if (deck.Count == 0)
            ResetDeck();

        //int index = Random.Range(0, deck.Count);
        int index = 0;
        Card drawnCard = deck[index];
        deck.RemoveAt(index);
        hand[selectedIndex] = drawnCard;
    }

    /// <summary>
    /// Sets a card to a blank card.
    /// </summary>
    /// <returns>The card removed from hand.</returns>
    public Card PlayCard(HitEventArgs? h = null)
    {
        Card selectedCard = hand[selectedIndex];
        hand[selectedIndex] = CardsManager.Instance.CardDatabase.Cards[0];
        selectedCard.PlayCard(h);
        return selectedCard;
    }

    public void Shuffle()
    {
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n - 1);
            Card value = deck[k];
            deck[k] = deck[n];
            deck[n] = value;
        }

    }

    public void MoveSelectionLeft()
    {
        selectedIndex = (selectedIndex - 1 + 3) % 3;
    }
    public void MoveSelectionRight()
    {
        selectedIndex = (selectedIndex + 1) % 3;
    }
    public void MoveSelection(bool isMovingLeft)
    {
        selectedIndex = (selectedIndex + (isMovingLeft ? -1 : 1) + 3) % 3;
    }

    public void ResetDeck()
    {
        bool foundInHand = false;
        deck = new List<Card>();
        foreach (Card deckListCard in deckList)
        {
            foreach (Card handCard in hand)
            {
                if (deckListCard == handCard)
                {
                    foundInHand = true;
                }
            }
            if (!foundInHand)
                deck.Add(deckListCard);
            foundInHand = false;
        }

        Shuffle();
    }

    public void DrawHand()
    {
        int indexBackup = selectedIndex;
        for (int index = 0; index < 3; index++)
        {
            selectedIndex = index;
            DrawCard();
        }
        selectedIndex = indexBackup;
    }

    public Card[] GetHand()
    {
        return hand;
    }
}