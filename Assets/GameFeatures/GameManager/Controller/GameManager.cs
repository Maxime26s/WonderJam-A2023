using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    GameState _gameState = GameState.Idle;
    public GameState GameState { get => _gameState; set => _gameState = value; }


    [SerializeField]
    PlayerManager _playerManager;
    [SerializeField]
    BeatController _beatController;

    float _timeBeforeStartGame = 3f;
    float _timeBetweenEachTurn = 3f;

    WaitForSeconds _waitBeforeStartGame;
    WaitForSeconds _waitBetweenEachRound;

    [SerializeField]
    public TextMeshProUGUI countDownText;

    int tickCount = 0;

    public void Awake()
    {
        ResetData();

        _waitBeforeStartGame = new WaitForSeconds(_timeBeforeStartGame);
        _waitBetweenEachRound = new WaitForSeconds(_timeBetweenEachTurn);
    }

    //Bind this on the start game after player selection
    public IEnumerator StartGame()
    {
        GameState = GameState.GameBegin;

        _playerManager.Init();

        BeatController.Instance.OnBeatEvent += CountTick;

        yield return _waitBeforeStartGame;

        StartTics();
        
        yield return WaitForTick(3);

        StartCoroutine(StartNextRound());

        Ball.Instance.RenderActionPoint();
    }

    public void StartGameNOW()
    {
        StartCoroutine(StartGame());
    }

    //Bind this on running out of actions and skipping turn
    public void TurnOver()
    {
        StartCoroutine(ChangeTurn());
    }

    IEnumerator ChangeTurn()
    {
        GameState = GameState.ChangingTurn;

        yield return _playerManager.MoveAllPlayerNextPosition();

        StartCoroutine(StartNextRound());
    }

    IEnumerator StartNextRound()
    {
        int tick0 = tickCount;

        while (tickCount < tick0 + 3)  // Wait until tickCounter increments by 3 from its initial value
        {
            countDownText.text = (3 - (tickCount - tick0)).ToString();  // Update the TextMeshPro UI Text
            countDownText.gameObject.SetActive(true);
            
            // Yield for a short duration before checking again
            // This can be set to a smaller value if you expect rapid firing of the external event
            yield return new WaitForSeconds(0.1f);
        }

        countDownText.gameObject.SetActive(false);
        GameState = GameState.Playing;
    }

    void StartTics()
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

    void CountTick()
    {
        tickCount++;
    }

    public IEnumerator WaitForTick(int nTicks)
    {
        int tick0 = tickCount;

        while (tickCount < tick0 + nTicks)  // Wait until tickCounter increments by n from its initial value
        {
            yield return new WaitForSeconds(0.1f);
        }
    }
}
public enum GameState { Idle, GameBegin, Playing, ChangingTurn, PlayerDeath, GameEnd}
