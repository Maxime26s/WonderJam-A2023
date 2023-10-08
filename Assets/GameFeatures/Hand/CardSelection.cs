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
        currentIndex = 1;
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
        currentIndex = (currentIndex + value + displayedCards.Count) % (displayedCards.Count);
        currentPlayer.GetCards().MoveSelection(value == 1 ? false : true);

        displayedCards[currentIndex].BeginHover();
    }

    void Use(HitEventArgs args)
    {
        PlayerController currentPlayer = PlayerManager.Instance.PlayerManagerData.GetCurrentPlayer();

        if (currentPlayer)
        {
            currentPlayer.Animator.SetTrigger("PlayCard");
            currentPlayer.GetCards().PlayCard(args);
            displayedCards[currentIndex].SetupCardUI(blankCard);
        }
    }

    void Reload(HitEventArgs args)
    {
        PlayerController currentPlayer = PlayerManager.Instance.PlayerManagerData.GetCurrentPlayer();
        if (currentPlayer)
        {
            currentPlayer.Animator.SetTrigger("PlayCard");

            currentPlayer.DrawHand();
            foreach (CardInHandUI card in displayedCards)
            {
                int index = 0;
                card.SetupCardUI(currentPlayer.GetHand()[index++]);
            }
        }
    }

    public void ResetDiplay()
    {
        foreach(PlayerController p in PlayerManager.Instance.PlayerManagerData.PlayersList)
        {
            p.GetCards().selectedIndex = 1;
        }
        currentIndex = 1;
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
        displayedCards[1].BeginHover();
    }
}
