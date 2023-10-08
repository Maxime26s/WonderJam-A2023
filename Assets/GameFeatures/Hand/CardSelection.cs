using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardSelection : Singleton<CardSelection>
{
    public BeatBar beatBar;

    public List<CardInHandUI> displayedCards;

    [SerializeField]
    private Card blankCard;

    [SerializeField]
    private GameObject cardInHandTemplate;
    public int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 2;
        beatBar.OnHitEvent += OnHit;
        RefreshDisplay();
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

        else if (GameManager.Instance.GameState == GameState.Playing)
        {
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
        
        RefreshDisplay();
    }

    void Move(HitEventArgs args)
    {
        PlayerController currentPlayer = PlayerManager.Instance.PlayerManagerData.GetCurrentPlayer();
        displayedCards[currentIndex].StopHover();

        int value = args.Context.ReadValue<float>() > 0 ? 1 : -1;
        currentIndex = (currentIndex + value + 5) % (displayedCards.Count);
        currentPlayer.GetCards().MoveSelection(value == 1 ? false : true);

        displayedCards[currentIndex].BeginHover();
    }

    void Use(HitEventArgs args)
    {
        PlayerController currentPlayer = PlayerManager.Instance.PlayerManagerData.GetCurrentPlayer();
        if (currentPlayer)
        {
            currentPlayer.GetCards().PlayCard();
            displayedCards[currentIndex].SetupCardUI(blankCard);
        }
        // TO CHANGE FOR: PLAY CARD ANIMATION
    }

    void Reload(HitEventArgs args)
    {
        PlayerController currentPlayer = PlayerManager.Instance.PlayerManagerData.GetCurrentPlayer();
        if (currentPlayer)
        {
            currentPlayer.Mulligan();
            foreach (CardInHandUI card in displayedCards)
            {
                int index = 0;
                card.SetupCardUI(currentPlayer.GetHand()[index++]);
            }
        }
    }

    public void ResetDiplay()
    {
        currentIndex = 2;
        displayedCards.ForEach(c => c.StopHover());
        displayedCards[currentIndex].BeginHover();
        RefreshDisplay();
    }

    void RefreshDisplay()
    {
        PlayerController currentPlayer = PlayerManager.Instance.PlayerManagerData.GetCurrentPlayer();
        int index = 0;
        if (currentPlayer)
        {
            foreach (CardInHandUI card in displayedCards)
            {
                card.SetupCardUI(currentPlayer.GetHand()[index++]);
            }
        }
        
    }

    void ResetSelf()
    {
        foreach (CardInHandUI card in displayedCards)
        {
            card.StopHover();
        }
        displayedCards[2].BeginHover();
    }
}
