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

    public void TakeDamage(int damage)
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
}
