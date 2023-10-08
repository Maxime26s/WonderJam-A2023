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

    void Start()
    {
    }

    public void Init()
    {
        SpawnAllPlayers();
        DetermineTurnOrder();
        PlayerManagerData.SetCurrentPlayer(PlayerManagerData.PlayerTurnOrderList[0]);
    }

    public void SpawnAllPlayers()
    {

        if (PlayerManagerData.PlayersToSpawn != null && PlayerManagerData.PlayersToSpawn.Count > 2)
        {
            for (int i = 0; i < PlayerManagerData.PlayersToSpawn.Count; i++)
            {
                SpawnPlayer(PlayerManagerData.PlayersToSpawn[i]);
            }
        }
        else
        {
            Debug.Log("WE ARE HACKING RN");
            Debug.Log("Player to spawn not set");
            for (int i = 0; i < 4; i++)
            {
                SpawnPlayer(i);
            }
        }

    }

    public void SpawnPlayer(int playerId)
    {
        if (_playerPrefab != null)
        {
            GameObject newPlayer = Instantiate(_playerPrefab, BattleGroundManager.Instance.GetCurrentBattleGround().GetAllPlayerPositions(PlayerManagerData.TotalNbPlayer)[playerId].transform);

            PlayerController playerController = newPlayer.GetComponentInChildren<PlayerController>();
            playerController.PlayerData.ResetData();
            playerController.PlayerData.PlayerId = playerId;
            playerController.Mulligan();

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
                    PlayerManagerData.PlayerTurnOrderList.Add(PlayerManagerData.PlayersList[i].PlayerData.PlayerId);
                }
                break;
            case PlayerManagerData.PlayerTurnOrderEnum.Descending:
                for (int i = PlayerManagerData.PlayersList.Count - 1; i >= 0; i--)
                {
                    PlayerManagerData.PlayerTurnOrderList.Add(PlayerManagerData.PlayersList[i].PlayerData.PlayerId);
                }
                break;
            case PlayerManagerData.PlayerTurnOrderEnum.Random:
                for (int i = 0; i < PlayerManagerData.PlayersList.Count; i++)
                {
                    PlayerManagerData.PlayerTurnOrderList.Add(PlayerManagerData.PlayersList[i].PlayerData.PlayerId);
                }
                PlayerManagerData.PlayerTurnOrderList.Shuffle();
                break;
            default:
                for (int i = 0; i < PlayerManagerData.PlayersList.Count; i++)
                {
                    PlayerManagerData.PlayerTurnOrderList.Add(PlayerManagerData.PlayersList[i].PlayerData.PlayerId);
                }
                break;
        }
    }

    public IEnumerator MoveAllPlayerNextPosition()
    {
        for (int i = 0; i < PlayerManagerData.PlayerTurnOrderList.Count; i++)
        {
            PlayerController player = PlayerManagerData.GetPlayer(PlayerManagerData.PlayerTurnOrderList[i]);

            Transform endPosition = BattleGroundManager.Instance.GetCurrentBattleGround().GetPlayerNextPosition(PlayerManagerData.TotalNbPlayer, PlayerManagerData.PlayerTurnOrderList[(i + 1) % PlayerManagerData.PlayerTurnOrderList.Count]);

            PlayerHolder ph = player.transform.parent.parent.parent.GetComponent<PlayerHolder>();
            ph.SetTarget(endPosition.position, endPosition.localScale, _timeToMoveToNewPosition);
        }

        for (int i = 0; i < PlayerManagerData.PlayerTurnOrderList.Count; i++)
        {
            PlayerController player = PlayerManagerData.GetPlayer(PlayerManagerData.PlayerTurnOrderList[i]);

            PlayerHolder ph = player.transform.parent.parent.parent.GetComponent<PlayerHolder>();
            ph.Move();
        }

        yield return new WaitForSeconds(_timeToMoveToNewPosition);

        PlayerManagerData.SetCurrentPlayer(PlayerManagerData.GetNextAlivePlayer().PlayerData.PlayerId);
    }
}