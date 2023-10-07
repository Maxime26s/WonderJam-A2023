using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardSelection : Singleton<CardSelection>
{
    public BeatBar beatBar;

    public List<CardInHandUI> displayedCards;
    private PlayerController currentPlayer;

    [SerializeField]
    private Card blankCard;

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
        displayedCards[currentIndex].gameObject.SetActive(true); // TO CHANGE FOR: 1. PLAY CARD UNHOVER ANIMATION

        int value = args.Context.ReadValue<float>() > 0 ? 1 : -1;
        // TODO: PLAY CARD UNHOVER ANIMATION

        currentIndex = (currentIndex + value) % (displayedCards.Count + 1);
        currentPlayer.GetCards().MoveSelection(value == 1 ? false : true);

        //cards[currentIndex].Select();
        displayedCards[currentIndex].gameObject.SetActive(false); // TO CHANGE FOR: 1. PLAY CARD HOVER ANIMATION
        // TODO: PLAY CARD HOVER ANIMATION

    }

    void Use(HitEventArgs args)
    {
        currentPlayer.GetCards().PlayCard();
        displayedCards[currentIndex].SetupCardUI(blankCard);

        // TO CHANGE FOR: PLAY CARD ANIMATION
    }

    void Reload(HitEventArgs args)
    {
        currentPlayer.Mulligan();
        foreach (CardInHandUI card in displayedCards)
        {
            int index = 0;
            card.SetupCardUI(currentPlayer.GetHand()[index++]);
        }
    }
}
