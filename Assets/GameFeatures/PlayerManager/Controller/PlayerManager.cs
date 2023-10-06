using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    GameObject _playerPrefab;

    List<PlayerController> _playersList; 

    public void SpawnPlayer()
    {
        if(_playerPrefab != null)
        {
            Instantiate(_playerPrefab);
        }
    }
}
