using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    PlayerData _playerData;
    public PlayerData PlayerData { get { return _playerData; } }

    [SerializeField]
    Animator _animator;
    public Animator Animator { get => _animator; set => _animator = value; }

    private ControllerActions controllerActions;

    private void Awake()
    {
        controllerActions = new ControllerActions();

        controllerActions.Gameplay.SkipTurn.performed += OnSkipPerformed;
    }

    private void OnDestroy()
    {
        controllerActions.Gameplay.SkipTurn.performed -= OnSkipPerformed;
    }

    private void OnEnable()
    {
        controllerActions.Enable();
    }

    private void OnDisable()
    {
        controllerActions.Disable();
    }

    private void OnSkipPerformed(InputAction.CallbackContext context)
    {
        if(GameManager.Instance.GameState == GameState.Playing)
        {
            //This player Clicked
            if(PlayerData.PlayerId == context.control.device.deviceId)
            {
                //This is the current player
                if (PlayerData.PlayerId == PlayerManager.Instance.PlayerManagerData.GetCurrentPlayerId())
                {
                    GameManager.Instance.TurnOver();
                }
            }
        }
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

    /// <summary>
    /// This throws away all cards in hand and draws 5 new ones.
    /// </summary>
    public void Mulligan()
    {
        PlayerData.cards.DrawHand();
    }
}