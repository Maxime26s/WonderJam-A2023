using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : Singleton<CardsManager>
{
    [SerializeField]
    public GameObject cardsHolder;
    PlayerController player;

    [SerializeField] private CardDatabase _cardDatabase = null;
    public CardDatabase CardDatabase => _cardDatabase;

    private void Start()
    { 
        UpdateCurrentPlayer();
    }

    private void RenderCards()
    {
        Card[] cards = player.GetHand();
        // TODO: place cards sur le jeu genre
        // Cards Placeholder, juste changer les infos avec celles de la carte
        //for (int i = 0; i < cardsHolder.transform.childCount; i++)
        //{
        //    Transform child = cardsHolder.transform.GetChild(i);
        //    cards[i].gameObject.SetActive(true);
        //    cards[i].gameObject.transform.position  = child.position;
        //}
    }

    void UpdateCurrentPlayer()
    {
        if (PlayerManager.Instance != null)
        {
            if (PlayerManager.Instance.PlayerManagerData != null)
            {
                player = PlayerManager.Instance.PlayerManagerData.GetCurrentPlayer();
            }
        }
        //RenderCards();
    }

    public void Tick()
    {
        if (player)
        {
            PlayerCards cards = player.GetCards();
            cards.PlayCard();
        }
        //RenderCards();
    }

    public void MoveSelection(bool isMovingLeft)
    {
        player.GetCards().MoveSelection(isMovingLeft);
    }

    public void PlayCard()
    {
        player.GetCards().PlayCard();
    }

    internal void DrawCard()
    {
        player.GetCards().DrawCard();
    }
}
