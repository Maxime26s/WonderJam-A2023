using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : Singleton<CardsManager>
{
    [SerializeField] private CardDatabase _cardDatabase = null;
    public CardDatabase CardDatabase => _cardDatabase;
}
