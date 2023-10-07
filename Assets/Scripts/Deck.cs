using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Deck : MonoBehaviour
{
    List<BaseCard> composition;
    List<BaseCard> currentCards;

    Deck()
    {
        composition = new List<BaseCard>();
        currentCards = composition;
    }

    public BaseCard DrawCard()
    {
        int index = Random.Range(0, currentCards.Count);
        BaseCard drawnCard = currentCards[index];
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