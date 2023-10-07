using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    int _playerId;
    [SerializeField]
    float _baseMaxHealth = 100;
    [SerializeField]
    float _currentMaxHealth;
    [SerializeField]
    float _currentHealth;
    [SerializeField]
    List<BaseCard> _hand;
    bool _isAlive = true;

    public int PlayerId { get { return _playerId; } set { _playerId = value; } }
    public float MaxHealth { get { return _currentMaxHealth; } set { _currentMaxHealth = value; } }
    public float CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }
    public bool IsAlive { get { return _isAlive; } set { _isAlive = value; } }
    public List<BaseCard> hand { get { return _hand; } set { _hand = value; } }
    public Deck deck;

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
