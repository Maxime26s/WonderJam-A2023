using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField]
    PlayerManagerData _playerManagerData;
    public PlayerManagerData PlayerManagerData { get { if (_playerManagerData == null) { } return _playerManagerData; } }

    [SerializeField]
    GameObject _playerPrefab;

    float _timeToMoveToNewPosition = 2f;

    Coroutine _checkWinCoroutine = null;

    [SerializeField]
    Color[] _playerColors;

    public void Init()
    {
        SpawnAllPlayers();
        DetermineTurnOrder();
        PlayerManagerData.SetCurrentPlayer(PlayerManagerData.PlayersList[PlayerManagerData.PlayerTurnOrderList[0]].PlayerData.PlayerDeviceId);
    }

    public void SpawnAllPlayers()
    {

        if (PlayerManagerData.PlayersToSpawn != null && PlayerManagerData.PlayersToSpawn.Count >= 2)
        {
            for (int i = 0; i < PlayerManagerData.PlayersToSpawn.Count; i++)
            {
                SpawnPlayer(PlayerManagerData.PlayersToSpawn[i], i);
            }
        }
        else
        {
            Debug.Log("WE ARE HACKING RN");
            Debug.Log("Player to spawn not set");
            for (int i = 0; i < 4; i++)
            {
                SpawnPlayer(i, i);
            }
        }

    }

    public void SpawnPlayer(int playerDeviceId, int playerIndex)
    {
        if (_playerPrefab != null)
        {
            GameObject newPlayer = Instantiate(_playerPrefab, BattleGroundManager.Instance.GetCurrentBattleGround().GetAllPlayerPositions(PlayerManagerData.PlayersToSpawn.Count)[playerIndex].transform);

            PlayerController playerController = newPlayer.GetComponentInChildren<PlayerController>();
            playerController.PlayerData.ResetData();
            playerController.PlayerData.PlayerDeviceId = playerDeviceId;
            playerController.PlayerData.PlayerIndex = playerIndex;
            playerController.SpriteRenderer.color = _playerColors[playerIndex];
            playerController.DrawHand();

            PlayerManagerData.PlayersList.Add(playerController);
        }
    }

    public void DetermineTurnOrder()
    {
        PlayerManagerData.PlayerTurnOrderList.Clear();

        switch (PlayerManagerData.PlayerTurnOrder)
        {
            case PlayerManagerData.PlayerTurnOrderEnum.Ascending:

                for (int i = 0; i < PlayerManagerData.PlayersList.Count; i++)
                {
                    PlayerManagerData.PlayerTurnOrderList.Add(PlayerManagerData.PlayersList[i].PlayerData.PlayerIndex);
                }
                break;
            case PlayerManagerData.PlayerTurnOrderEnum.Descending:
                for (int i = PlayerManagerData.PlayersList.Count - 1; i >= 0; i--)
                {
                    PlayerManagerData.PlayerTurnOrderList.Add(PlayerManagerData.PlayersList[i].PlayerData.PlayerIndex);
                }
                break;
            case PlayerManagerData.PlayerTurnOrderEnum.Random:
                for (int i = 0; i < PlayerManagerData.PlayersList.Count; i++)
                {
                    PlayerManagerData.PlayerTurnOrderList.Add(PlayerManagerData.PlayersList[i].PlayerData.PlayerIndex);
                }
                PlayerManagerData.PlayerTurnOrderList.Shuffle();
                break;
            default:
                for (int i = 0; i < PlayerManagerData.PlayersList.Count; i++)
                {
                    PlayerManagerData.PlayerTurnOrderList.Add(PlayerManagerData.PlayersList[i].PlayerData.PlayerIndex);
                }
                break;
        }
    }

    public IEnumerator MoveAllPlayerNextPosition()
    {
        for (int i = 0; i < PlayerManagerData.PlayerTurnOrderList.Count; i++)
        {
            PlayerController player = PlayerManagerData.GetPlayerByIndex(PlayerManagerData.PlayerTurnOrderList[i]);

            Transform endPosition = BattleGroundManager.Instance.GetCurrentBattleGround().GetPlayerNextPosition(PlayerManagerData.PlayersToSpawn.Count, PlayerManagerData.PlayerTurnOrderList[(i + 1) % PlayerManagerData.PlayerTurnOrderList.Count]);


            PlayerHolder ph = player.transform.parent.parent.parent.GetComponent<PlayerHolder>();
            ph.SetTarget(endPosition.position, endPosition.localScale, _timeToMoveToNewPosition);
        }

        for (int i = 0; i < PlayerManagerData.PlayerTurnOrderList.Count; i++)
        {
            PlayerController player = PlayerManagerData.GetPlayerByIndex(PlayerManagerData.PlayerTurnOrderList[i]);

            PlayerHolder ph = player.transform.parent.parent.parent.GetComponent<PlayerHolder>();
            ph.Move();
        }

        yield return new WaitForSeconds(_timeToMoveToNewPosition);

        PlayerManagerData.SetCurrentPlayer(PlayerManagerData.GetNextAlivePlayer().PlayerData.PlayerDeviceId);
    }

    public void CheckPlayerWin()
    {
        PlayerController winningPlayer = null;
        bool win = false;

        foreach(PlayerController player in PlayerManagerData.PlayersList)
        {
            if (player.PlayerData.IsAlive)
            {
                if(winningPlayer != null)
                {
                    win = false;
                    break;
                }
                else
                {
                    winningPlayer = player;
                    win = true;
                }
            }
        }

        if (win && _checkWinCoroutine == null)
        {
            _checkWinCoroutine = StartCoroutine(WaitAFrameToConfirmWin(winningPlayer));
        }
    }

    IEnumerator WaitAFrameToConfirmWin(PlayerController winner)
    {
        yield return null;

        ShowWinner(winner);
    }

    void ShowWinner(PlayerController winner)
    {
		GameManager.Instance.winnerPlayer = winner.PlayerData.PlayerIndex;

		SceneLoader.Instance.LoadLevel("WinScreen");
    }
}