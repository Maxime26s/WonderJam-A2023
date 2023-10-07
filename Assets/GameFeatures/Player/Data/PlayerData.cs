using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    int _playerId;
    [SerializeField]
    int _baseMaxHealth = 100;
    [SerializeField]
    int _currentMaxHealth;
    [SerializeField]
    int _currentHealth;
    bool _isAlive = true;

    public int PlayerId { get { return _playerId; } set { _playerId = value; } }
    public int MaxHealth { get { return _currentMaxHealth; } set { _currentMaxHealth = value; } }
    public int CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }
    public bool IsAlive { get { return _isAlive; } set { _isAlive = value; } }

    void OnGameStart()
    {
        ResetData();
    }

    public void ResetData()
    {
        _isAlive = true;

        _currentMaxHealth = _baseMaxHealth;
        _currentHealth = _baseMaxHealth;
    }
}
