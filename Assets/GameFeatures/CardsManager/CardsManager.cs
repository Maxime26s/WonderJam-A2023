using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : Singleton<CardsManager>
{
    [SerializeField]
    public GameObject cardsHolder;
    int selectedCard = -1;
    PlayerController player;


    private void Start()
    {
        BeatController.Instance.FixedOnBeatEvent += Tick;
        BeatController.Instance.OnBeatEvent += UpdateCurrentPlayer;
        UpdateCurrentPlayer();
    }

    private void RenderCards()
    {
        List<Card> cards = player.GetHand();
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
        player = PlayerManager.Instance.PlayerManagerData.GetCurrentPlayer();
        //RenderCards();
    }

    public void Tick()
    {
        if (selectedCard != -1)
        {
            List<Card> cards = player.GetHand();
            cards[selectedCard].PlayCard();
            RemoveCard(selectedCard);
        }
        selectedCard = -1;
        //RenderCards();
    }

    public void RemoveCard(int index, bool drawNewCard = true)
    {
        player.GetHand().RemoveAt(index);
        if (drawNewCard)
            DrawCard();
    }

    internal void DrawCard()
    {
        player.GetHand().Add(player.GetDeck().DrawCard());
    }
}
