using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CardData", menuName ="Data/Card", order = 0)]
public class CardData : ScriptableObject
{
    public Material Material;
    public string Name;
    public Card.Type Type;
}
