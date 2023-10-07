using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    PlayerData _playerData;

    public PlayerData PlayerData { get { return _playerData; } }

    public void ReceiveEffect()
    {

    }

    public void ReceiveHealing(float healing)
    {
        if (PlayerData.IsAlive)
        {
            PlayerData.CurrentHealth += healing;

            CheckPlayerDies();
        }
        else
        {
            Debug.Log("This player is already dead :(");
        }
    }

    public void TakeDamage(float damage)
    {
        if (PlayerData.IsAlive)
        {
            PlayerData.CurrentHealth -= damage;

            CheckPlayerDies();
        }
        else
        {
            Debug.Log("This player is already dead :(");
        }
    }

    public bool CheckPlayerDies()
    {
        return PlayerData.CurrentHealth > 0;
    }

    public PlayerCards GetCards()
    {
        return PlayerData.cards;
    }

    public Card[] GetHand()
    {
        return PlayerData.cards.GetHand();
    }
}