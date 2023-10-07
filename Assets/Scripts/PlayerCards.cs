using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCards
{
    //4 cards in hand. When a card is played, a card is drawn. If a card would be drawn, all cards except the 3 in hand are placed back in the deck.
    //5 cards in hand. One is selected. Playing a card gives a blank card instead of it. 
    private List<Card> deckList;
    private List<Card> deck;
    private Card[] hand = new Card[5];
    public int selectedIndex = 2;

    public PlayerCards()
    {
        deckList = new List<Card>(CardsManager.Instance.CardDatabase.Cards);
        deck = new List<Card>(deckList);
    }

    /// <summary>
    /// Replaces the currently selected by a new random card from the deck.
    /// </summary>
    public void DrawCard()
    {
        //if (hand.Count >= 4)
        //return;

        if (deck.Count == 0)
            ResetDeck();

        //int index = Random.Range(0, deck.Count);
        int index = 0;
        Card drawnCard = deck[index];
        deck.RemoveAt(index);
        hand[selectedIndex] = drawnCard;
    }

    /// <summary>
    /// Sets a card to a blank card. Blank cards are not yet created, so it just sets it to null for now.
    /// </summary>
    /// <returns>The card removed from hand.</returns>
    public Card PlayCard()
    {
        Card selectedCard = hand[selectedIndex];
        hand[selectedIndex] = null;
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
        selectedIndex = (selectedIndex - 1) % 5;
    }
    public void MoveSelectionRight()
    {
        selectedIndex = (selectedIndex + 1) % 5;
    }
    public void MoveSelection(bool isMovingLeft)
    {
        selectedIndex = (selectedIndex + (isMovingLeft ? -1 : 1)) % 5;
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

    public void Mulligan()
    {
        int indexBackup = selectedIndex;
        for (int index = 0; index < 5; index++)
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