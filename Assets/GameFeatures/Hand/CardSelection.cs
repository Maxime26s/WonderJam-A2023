using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardSelection : Singleton<CardSelection>
{
    public BeatBar beatBar;

    public List<Card> displayedCards;
    private PlayerController currentPlayer;

    [SerializeField]
    private GameObject cardInHandTemplate;
    public int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = displayedCards.Count / 2;
        beatBar.OnHitEvent += OnHit;
    }

    private void OnDestroy()
    {
        beatBar.OnHitEvent -= OnHit;
    }

    void OnHit(object sender, HitEventArgs args)
    {
        // TODO: LOSE ACTION POINTS

        if (args.Result == HitResult.Miss)
        {
            return;
        }

        if (args.Context.action.name == "Use")
        {
            Use(args);
        }
        else if (args.Context.action.name == "Move")
        {
            Move(args);
        }
        else if (args.Context.action.name == "Reload")
        {
            Reload(args);
        }
    }

    void Move(HitEventArgs args)
    {
        //cards[currentIndex].Unselect();
        displayedCards[currentIndex].SetActive(true); // TO CHANGE FOR: 1. PLAY CARD UNHOVER ANIMATION
        
        int value = args.Context.ReadValue<float>() > 0 ? 1 : -1;
        // TODO: PLAY CARD UNHOVER ANIMATION

        currentIndex = (currentIndex + value) % (displayedCards.Count + 1);
        currentPlayer.GetCards().MoveSelection(value == 1 ? false : true);

        //cards[currentIndex].Select();
        displayedCards[currentIndex].SetActive(false); // TO CHANGE FOR: 1. PLAY CARD HOVER ANIMATION
        // TODO: PLAY CARD HOVER ANIMATION

    }

    void Use(HitEventArgs args)
    {
        currentPlayer.GetCards().PlayCard();
        Destroy(displayedCards[currentIndex]); // TO CHANGE FOR: 1. USE CARD 2. PLAY CARD ANIMATION 3. SWAP AT INDEX FOR EMPTY CARD
    }
    
    void Reload(HitEventArgs args)
    {
        for(int i = 0; i < displayedCards.Count; i++)
        {
            foreach (Card card in displayedCards[i])
                Destroy(card);

            foreach (Card card in currentPlayer.GetHand())
                displayedCards.Add();
            // TO ADD: 1. REROLL ALL CARDS
        }
    }

    public void RefreshDisplay(PlayerController player)
    {
        foreach(Card card in player.GetHand())
        {
            //displayedCards.Add(c);
        }
    }
}
