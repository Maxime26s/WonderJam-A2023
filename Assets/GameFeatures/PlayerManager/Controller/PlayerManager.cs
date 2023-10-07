using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton
{
    PlayerManagerData _playerManagerData = new PlayerManagerData();
    public PlayerManagerData PlayerManagerData { get { if (_playerManagerData == null) {  } return _playerManagerData; } }

    [SerializeField]
    GameObject _playerPrefab;

    public void SpawnAllPlayers()
    {
        PlayerManagerData.ResetData();
        for(int i = 0; i < PlayerManagerData.TotalNbPlayer; i++)
        {
            SpawnPlayer(i);
        }
    }

    public void SpawnPlayer(int playerId)
    {
        if(_playerPrefab != null)
        {
            GameObject newPlayer = Instantiate(_playerPrefab);

            PlayerController playerController = newPlayer.GetComponentInChildren<PlayerController>();
            playerController.PlayerData.ResetData();
            playerController.PlayerData.PlayerId = playerId;

            PlayerManagerData.PlayersList.Add(playerController);
        }
    }

    public void DetermineTurnOrder()
    {
        PlayerManagerData.PlayerTurnOrderList.Clear();

        switch (PlayerManagerData.PlayerTurnOrder)
        {
            case PlayerManagerData.PlayerTurnOrderEnum.Ascending:

                for(int i =0;i< PlayerManagerData.PlayersList.Count; i++)
                {
                    PlayerManagerData.PlayerTurnOrderList.Add(PlayerManagerData.PlayersList[i].PlayerData.PlayerId);
                }
                break;
            case PlayerManagerData.PlayerTurnOrderEnum.Descending:
                for (int i = PlayerManagerData.PlayersList.Count -1 ; i >= 0 ; i--)
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

        //todo move people on the field after we shuffle to reflect the turn order
    }
}