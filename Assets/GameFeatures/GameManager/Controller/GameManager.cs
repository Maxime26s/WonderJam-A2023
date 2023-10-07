using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    GameState _gameState = GameState.None;
    public GameState GameState { get => _gameState; set => _gameState = value; }


    [SerializeField]
    PlayerManager _playerManager;
    [SerializeField]
    BeatController _beatController;

    float _timeBeforeStartGame = 3f;
    float _timeBetweenEachTurn = 3f;

    WaitForSeconds _waitBeforeStartGame;
    WaitForSeconds _waitBetweenEachRound;

    public void Awake()
    {
        ResetData();

        _waitBeforeStartGame = new WaitForSeconds(_timeBeforeStartGame);
        _waitBetweenEachRound = new WaitForSeconds(_timeBetweenEachTurn);
    }

    public IEnumerator StartGame()
    {
        GameState = GameState.GameBegin;

        _playerManager.Init();

        yield return _waitBeforeStartGame;

        StartNextRound();
    }

    public void TurnOver()
    {
        StartCoroutine(ChangeTurn());
    }

    public IEnumerator ChangeTurn()
    {
        GameState = GameState.ChangingTurn;

        yield return _playerManager.MoveAllPlayerNextPosition();

        StartCoroutine(StartNextRound());
    }

    public IEnumerator StartNextRound()
    {
        yield return _waitBetweenEachRound;

        GameState = GameState.Playing;

        StartTics();
    }

    public void StartTics()
    {
        _beatController.StartPlaying();
    }

    public void EndGame()
    {

    }

    public void ResetData()
    {
        GameState = GameState.Idle;
        _playerManager.PlayerManagerData.ResetData();
    }
}
public enum GameState { Idle, GameBegin, Playing, ChangingTurn, PlayerDeath, GameEnd}
