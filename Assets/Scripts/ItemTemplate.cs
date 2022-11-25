using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "ItemTemplate", menuName = "ProsperityHouse/Item Template", order = 1)]
public class ItemTemplate : ScriptableObject
{
    public string itemName;
    public Sprite itemArtwork;
    public int itemBuyPrice;
}
