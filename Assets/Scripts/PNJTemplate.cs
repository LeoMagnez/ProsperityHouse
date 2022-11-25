using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCTemplate", menuName = "ProsperityHouse/NPC Template", order = 2)]
public class PNJTemplate : ScriptableObject
{
    public string npcName;
    public string npcJob;
    public string npcDialogue;
    public ItemTemplate[] searchedItems;
    public Sprite npcArtwork;
    
}
