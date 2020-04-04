using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CardData", menuName ="Data/Card", order = 0)]
public class CardData : ScriptableObject
{
    public int Health;
    public Card.Tribe Tribe;
    public Material Material;
}
