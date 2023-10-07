using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardSelection : MonoBehaviour
{
    public BeatBar beatBar;

    public List<GameObject> cards;
    public int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = cards.Count / 2;
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
        // TODO: PLAY CARD UNHOVER ANIMATION

        if (args.Context.ReadValue<float>() > 0)
        {
            currentIndex++;
            if (currentIndex >= cards.Count)
            {
                currentIndex = 0;
            }
        }
        else
        {
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = cards.Count - 1;
            }
        }

        //cards[currentIndex].Select();
        // TODO: PLAY CARD HOVER ANIMATION

    }

    void Use(HitEventArgs args)
    {
        //cards[currentIndex].PlayCard(args.Result);
        // TODO: 1. USE CARD 2. PLAY CARD ANIMATION 3. SWAP AT INDEX FOR EMPTY CARD
    }
    
    void Reload(HitEventArgs args)
    {
        for(int i = 0; i < cards.Count; i++)
        {
            // TODO: REROLL ALL CARDS
        }
    }
}
