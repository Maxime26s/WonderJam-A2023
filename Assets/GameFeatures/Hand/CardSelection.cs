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
        if (args.Context.action.name == "Select")
        {
            Use(args);
        }
        else if (args.Context.action.name == "Move")
        {
            Move(args);
        }
    }

    void Move(HitEventArgs args)
    {
        if (args.Result == HitResult.Miss)
            return;

        //cards[currentIndex].Unselect();
        cards[currentIndex].SetActive(true); // TO CHANGE FOR: 1. PLAY CARD UNHOVER ANIMATION

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
        cards[currentIndex].SetActive(false); // TO CHANGE FOR: 1. PLAY CARD HOVER ANIMATION

    }

    void Use(HitEventArgs args)
    {
        //cards[currentIndex].PlayCard(args.Result);
        Destroy(cards[currentIndex]); // TO CHANGE FOR: 1. USE CARD 2. PLAY CARD ANIMATION 3. SWAP AT INDEX FOR EMPTY CARD
    }
}
