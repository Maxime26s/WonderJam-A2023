using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGroundController : MonoBehaviour
{
    [SerializeField]
    Transform _centerTransform;
    public Transform CenterTransform { get => _centerTransform; set => _centerTransform = value; }


    [SerializeField]
    List<Transform> _playerPositions2Players;
    [SerializeField]
    List<Transform> _playerPositions3Players;
    [SerializeField]
    List<Transform> _playerPositions4Players;

    [SerializeField]
    GameObject _playersParent;
    public GameObject PlayersParent { get => _playersParent; set => _playersParent = value; }

    public void Awake()
    {
        BattleGroundManager.Instance.SetCurrentBattleground(this);
    }

    public List<Transform> GetAllPlayerPositions(int numberOfPlayers)
    {
        switch (numberOfPlayers)
        {
            case 2:
                return _playerPositions2Players;
            case 3:
                return _playerPositions3Players;
            case 4:
                return _playerPositions4Players;
            default:
                Debug.Log("Wrong number of player you suck");
                return _playerPositions4Players;
        }
    }
    public Transform GetPlayerNextPosition(int numberOfPlayers, int playerTargetPosition)
    {
        //if(playerTargetPosition < numberOfPlayers)
        //{
            switch (numberOfPlayers)
            {
                case 2:
                    return _playerPositions2Players[playerTargetPosition];
                case 3:
                    return _playerPositions3Players[playerTargetPosition];
                case 4:
                    return _playerPositions4Players[playerTargetPosition];
                default:
                    Debug.Log("Wrong number of player you suck");
                    return _playerPositions4Players[playerTargetPosition];
            }
        //}
        //else
        //{
        //    Debug.Log("Number of player is wrong cant get player next position");
        //    return null;
        //}
    }
}
