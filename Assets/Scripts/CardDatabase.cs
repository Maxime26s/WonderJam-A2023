using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Database", menuName = "Database/Cards Database")]
public class CardDatabase : ScriptableObject
{
    [SerializeField] private List<Card> _cards = new List<Card>();
    public List<Card> Cards => _cards;
}
