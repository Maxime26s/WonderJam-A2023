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
        deckList = new List<Card>();
        deck = deckList;
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

        int index = Random.Range(0, deck.Count);
        Card drawnCard = deck[index];
        deck.RemoveAt(index);
        hand[selectedIndex] = drawnCard;
    }

    /// <summary>
    /// Sets a card to a blank card. Blank cards are not yet created.
    /// </summary>
    /// <returns>The card removed from hand.</returns>
    public Card PlayCard()
    {
        Card selectedCard = hand[selectedIndex];
        hand[selectedIndex] = new Card();
        return selectedCard;
    }

    public void Shuffle()
    {
        //Shuffle :)
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
    //Currently creates duplicates of cards currently in hand, need to fix
    public void ResetDeck()
    {
        deck = deckList;
        Shuffle();
    }
    public Card[] GetHand()
    {
        return hand;
    }
}