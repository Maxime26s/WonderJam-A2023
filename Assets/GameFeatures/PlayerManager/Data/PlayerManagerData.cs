using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerData : MonoBehaviour
{
    List<PlayerController> _playersList = new List<PlayerController>();
    int _currentPlayerId;
    List<int> _playerTurnOrderList = new List<int>();
    PlayerTurnOrderEnum _playerTurnOrder;
	[SerializeField]
	List<int> _playersToSpawn = new List<int>();

    public List<PlayerController> PlayersList { get => _playersList; set => _playersList = value; }
    public List<int> PlayerTurnOrderList { get => _playerTurnOrderList; set => _playerTurnOrderList = value; }
	public List<int> PlayersToSpawn { get => _playersToSpawn; set => _playersToSpawn = value; }
    public PlayerTurnOrderEnum PlayerTurnOrder { get => _playerTurnOrder; set => _playerTurnOrder = value; }

    public void ResetData()
    {
        PlayersList.Clear();
        PlayerTurnOrderList.Clear();
        PlayerTurnOrder = PlayerTurnOrderEnum.Ascending;
    }

    public void SetCurrentPlayer(int playerId)
    {
        _currentPlayerId = playerId;
    }

    public PlayerController GetPlayer(int id)
    {
        for (int i = 0; i < PlayersList.Count; i++)
        {
            if (PlayersList[i].PlayerData.PlayerId == id)
            {
                return PlayersList[i];
            }
        }

        Debug.Log("Couldn't get the player with the id : " + id);
        return null;
    }

    public PlayerController GetPlayerByIndex(int index)
    {
        return PlayersList[index];
    }
    public PlayerController GetCurrentPlayer()
    {
        return GetPlayer(_currentPlayerId);
    }
    public int GetCurrentPlayerId()
    {
        return _currentPlayerId;
    }

    public PlayerController GetNextAlivePlayer()
    {
        PlayerController player = GetCurrentPlayer();

        int playerIndex = player.PlayerData.PlayerIndex;

        //for (int i = 0; i < PlayerTurnOrderList.Count; i++)
        //{
        //    if (PlayerTurnOrderList[i] == _currentPlayerId)
        //    {
        //        playerIndex = i;
        //    }
        //}

        for (int i = 1; i < PlayerTurnOrderList.Count; i++)
        {
            player = GetPlayerByIndex(PlayerTurnOrderList[(playerIndex + 1) % PlayerTurnOrderList.Count]);

            if (player.PlayerData.IsAlive)
            {
                return player;
            }
        }

        Debug.Log("Couldn't get the next alive player (you probably won???)");
        return GetPlayer(PlayerTurnOrderList[playerIndex]);
    }

    public PlayerController GetNextPlayer()
    {
        for (int i = 0; i < PlayerTurnOrderList.Count; i++)
        {
            if (PlayerTurnOrderList[i] == _currentPlayerId)
            {
                return GetPlayer(PlayerTurnOrderList[(i + 1) % PlayerTurnOrderList.Count]);
            }
        }

        Debug.Log("Couldn't get the next player");
        return null;
    }


    public enum PlayerTurnOrderEnum { Ascending, Descending, Random }
}
