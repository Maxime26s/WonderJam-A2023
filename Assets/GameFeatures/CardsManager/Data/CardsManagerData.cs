using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManagerData : MonoBehaviour
{
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCurrentPlayer();
    }

    public void UpdateCurrentPlayer()
    {
        PlayerManager pm = (PlayerManager)Singleton.Instance;
        player = pm.PlayerManagerData.GetCurrentPlayer();
    }

    public void RemoveCard(int index, bool drawNewCard=true)
    {
        player.GetHand().RemoveAt(index);
        if (drawNewCard)
        {
            // TODO:
            //player.DrawCard();
        }
    }
}
