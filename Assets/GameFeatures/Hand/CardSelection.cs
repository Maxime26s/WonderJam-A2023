using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelection : MonoBehaviour
{
    public BeatBar beatBar;

    public List<Card> cards;
    public int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        beatBar.OnHitEvent += OnHit;
    }

    private void OnDestroy()
    {
        beatBar.OnHitEvent -= OnHit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnHit(object sender, HitEventArgs args)
    {
        if(args.Context.action.name == "Select")
        {
            Use(args);
        }
        else if(args.Context.action.name == "Move")
        {
            Move(args);
        }
    }

    void Move(HitEventArgs args)
    {
        if (args.Result == HitResult.Miss)
            return;

        //cards[currentIndex].Unselect();

        if(args.Context.ReadValue<float>() > 0)
        {
            currentIndex++;
            if(currentIndex >= cards.Count)
            {
                currentIndex = 0;
            }
        }
        else
        {
            currentIndex--;
            if(currentIndex < 0)
            {
                currentIndex = cards.Count - 1;
            }
        }

        //cards[currentIndex].Select();
    }

    void Use(HitEventArgs args)
    {
        //cards[currentIndex].PlayCard(args.Result);
    }
}
