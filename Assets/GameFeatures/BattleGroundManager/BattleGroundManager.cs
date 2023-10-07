using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGroundManager : Singleton
{
    BattleGroundController _currentBattleGround;

    public void SetCurrentBattleground(BattleGroundController battleGroundController)
    {
        if(_currentBattleGround != null)
        {
            _currentBattleGround.gameObject.SetActive(false);
        }

        battleGroundController.gameObject.SetActive(true);
        _currentBattleGround = battleGroundController;
    }

    public BattleGroundController GetCurrentBattleGround()
    {
        if (_currentBattleGround != null)
        {
            return _currentBattleGround;
        }

        Debug.Log("No Current BattleGround");
        return null;
    }
}
