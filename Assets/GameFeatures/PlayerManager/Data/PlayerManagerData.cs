using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerData : MonoBehaviour
{
    int _totalNbPlayer;
    public int TotalNbPlayer { get; set; }

    List<PlayerController> _playersList;
    int _currentPlayerId;
    List<int> _playerTurnOrderList;
    PlayerTurnOrderEnum _playerTurnOrder;

    public List<PlayerController> PlayersList { get => _playersList; set => _playersList = value; }
    public List<int> PlayerTurnOrderList { get => _playerTurnOrderList; set => _playerTurnOrderList = value; }
    public PlayerTurnOrderEnum PlayerTurnOrder { get => _playerTurnOrder; set => _playerTurnOrder = value; }

    public void ResetData()
    {
        PlayersList.Clear();
        PlayerTurnOrderList.Clear();
        PlayerTurnOrder = PlayerTurnOrderEnum.Ascending;
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
    public PlayerController GetCurrentPlayer()
    {
        return GetPlayer(_currentPlayerId);
    }
    public int GetCurrentPlayerId()
    {
        return _currentPlayerId;
    }

    public PlayerController GetNextPlayer()
    {
        for (int i = 0; i < PlayerTurnOrderList.Count; i++)
        {
            if (PlayerTurnOrderList[i] == _currentPlayerId)
            {
                return GetPlayer(PlayerTurnOrderList[i + 1 % PlayerTurnOrderList.Count]);
            }
        }

        Debug.Log("Couldn't get the next player");
        return null;
    }




    public enum PlayerTurnOrderEnum { Ascending, Descending, Random }
}