using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SoldierData", menuName ="Data/Soldier", order = 0)]
public class SoldierData : CardData
{
    public int Health;
    public Card.Tribe Tribe;
}
