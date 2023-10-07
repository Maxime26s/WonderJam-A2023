using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Deck : MonoBehaviour
{
    List<Card> composition;
    List<Card> currentCards;

    Deck()
    {
        composition = new List<Card>();
        currentCards = composition;
    }

    public Card DrawCard()
    {
        int index = Random.Range(0, currentCards.Count);
        Card drawnCard = currentCards[index];
        currentCards.RemoveAt(index);

        if (currentCards.Count == 0)
            ResetDeck();

        return drawnCard;
    }
    public void Shuffle()
    {
        //Shuffle :)
    }

    //Currently creates duplicates of cards currently in hand, need to fix
    public void ResetDeck()
    {
        currentCards = composition;
        Shuffle();
    }
}