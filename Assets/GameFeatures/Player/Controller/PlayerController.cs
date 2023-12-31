using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    PlayerData _playerData;
    public PlayerData PlayerData { get { return _playerData; } }

    [SerializeField]
    Animator _animator;
    public Animator Animator { get => _animator; set => _animator = value; }

    [SerializeField]
    SpriteRenderer _spriteRenderer;
    public SpriteRenderer SpriteRenderer { get => _spriteRenderer; set => _spriteRenderer = value; }


    [SerializeField]
    Slider _slider;

    [SerializeField]
    float _timeToWaitPlayerDeathAnim = 3f;
    WaitForSeconds _waitForSecondPlayerDeathAnim;

    private ControllerActions controllerActions;

    private void Awake()
    {
        controllerActions = new ControllerActions();

        _waitForSecondPlayerDeathAnim = new WaitForSeconds(_timeToWaitPlayerDeathAnim);

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
        if (GameManager.Instance.GameState == GameState.Playing)
        {
            //This player Clicked
            if (PlayerData.PlayerDeviceId == context.control.device.deviceId)
            {
                //This is the current player
                if (PlayerData.PlayerDeviceId == PlayerManager.Instance.PlayerManagerData.GetCurrentPlayerId())
                {
                    if (GameManager.Instance.GameState == GameState.Playing)
                        StartCoroutine(GameManager.Instance.TurnOver());
                }
            }
        }
    }

    public void ReceiveHealing(float healing)
    {
        if (PlayerData.IsAlive && healing >= 0)
        {
            PlayerData.CurrentHealth += healing;

            if (PlayerData.MaxHealth < PlayerData.CurrentHealth)
                PlayerData.CurrentHealth = PlayerData.MaxHealth;

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

    public void CheckPlayerDies()
    {
        float hpPercent = PlayerData.CurrentHealth / PlayerData.MaxHealth;
        if (_slider)
        {
            _slider.value = hpPercent;
        }

        if (hpPercent <= 0)
        {
            PlayerDies();
        }
    }

    void PlayerDies()
    {
        PlayerData.IsAlive = false;
        Animator.SetTrigger("Death");

        PlayerManager.Instance.CheckPlayerWin();

        if (PlayerManager.Instance.PlayerManagerData.GetCurrentPlayerId() == PlayerData.PlayerDeviceId)
        {
            BeatController.Instance.StopPlaying();

            StartCoroutine(PlayerDiesWaitForAnimationCoroutine());
        }
    }

    IEnumerator PlayerDiesWaitForAnimationCoroutine()
    {
        yield return _waitForSecondPlayerDeathAnim;

        GameManager.Instance.TurnOver();
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
    public void DrawHand()
    {
        PlayerData.cards.DrawHand();
    }
}