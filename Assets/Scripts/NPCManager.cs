using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCManager : MonoBehaviour
{
    [Header("NPC Characteristics")]
    public PNJTemplate npc;

    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI npcJobText;
    public TextMeshProUGUI npcDialogueText;
    public Image npcArtwork;

    [Header("Item searched")]
    public ItemTemplate item;

    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemBuyPriceText;
    public Image itemArtwork;

    [SerializeField]
    private PNJTemplate[] npcList;
    private int npcCurrent;

    [SerializeField]
    private ItemTemplate[] itemList;
    private int itemCurrent;

    // Start is called before the first frame update
    void Start()
    {
        npcCurrent = 0;
        itemCurrent = 0;

        ReloadNPC();
        ReloadItems();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H) && npcCurrent == 1)
        {
            npcCurrent = 0;
            itemCurrent = 0;
            ReloadNPC();
            ReloadItems();
            Debug.Log("Madeline");
        }

        if(Input.GetKeyDown(KeyCode.D) && npcCurrent == 0)
        {
            npcCurrent = 1;
            itemCurrent = 1;
            ReloadNPC();
            ReloadItems();
            Debug.Log("Hildebert");
        }
    }

    public void ReloadNPC()
    {
        npc = npcList[npcCurrent];
        npcNameText.text = npc.npcName;
        npcJobText.text = npc.npcJob;
        npcDialogueText.text = npc.npcDialogue;
        npcArtwork.sprite = npc.npcArtwork;
    }

    public void ReloadItems()
    {
        item = itemList[itemCurrent];
        itemNameText.text = item.itemName;
        itemBuyPriceText.text = item.itemBuyPrice.ToString();
        itemArtwork.sprite = item.itemArtwork;
    }

}
