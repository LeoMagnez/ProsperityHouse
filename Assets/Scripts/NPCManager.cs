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
    public TextMeshProUGUI itemMargin;
    public Image itemArtwork;
    public float itemMarginMultiplier;
    public float itemFinalValue;

    [SerializeField]
    private PNJTemplate[] npcList;
    private int npcCurrent;

    [SerializeField]
    public ItemTemplate[] npcCurrentItems;
    public int itemCurrent;

    public Gamemanager gm;

    // Start is called before the first frame update
    void Start()
    {
        npcCurrent = 0;
        itemCurrent = 0;
        npcList = gm.NPCSpawner();
        npcCurrentItems = npcList[0].searchedItems;
        itemCurrent = Random.Range(0, npcCurrentItems.Length);
        ReloadNPC();
        ReloadItems();
        ReloadMargin();
    }

    void Update()
    {

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
        item = npcCurrentItems[itemCurrent];
        itemNameText.text = item.itemName;
        itemBuyPriceText.text = item.itemBuyPrice.ToString();
        itemArtwork.sprite = item.itemArtwork;
        
    }

    public void ReloadMargin()
    {
        itemMarginMultiplier = Random.Range(0.15f, 0.3f);
        itemFinalValue = Mathf.RoundToInt(item.itemBuyPrice * itemMarginMultiplier);
        itemMargin.text = "+" + itemFinalValue;
    }

    public void NewNPCTrade()
    {
        npcList = gm.NPCSpawner();
        if (npcCurrent < npcList.Length - 1)
        {
            npcCurrent += 1;
        }
        else
        {
            npcCurrent = 0;
        }
        npcCurrentItems = npcList[npcCurrent].searchedItems;
        itemCurrent = Random.Range(0, npcCurrentItems.Length);
        Debug.Log(itemCurrent);
        ReloadNPC();
        ReloadItems();
        ReloadMargin();
    }

}


















/*                                ======================
                                  ######################
                                  ######################
                       +++++++++++----------------------=+++++++++++
                       ###########=:::::::::::::::::::::=###########
                       ###########=:::::::::::::::::::::=###########
                 ######:::::::::::::::::::::::::::::::::::::::::::::######
                 ######:::::::::::::::::::::::::::::::::::::::::::::######
                 ######:::::::::::::::::::::::::::::::::::::::::::::######
                 ######:::::::::::::::::::::::::::::::::::::::::::::######
                 ######:::::::::::::::::::::::::::::::::::::::::::::######
            :::::++++++::::::::::::::::-=====:::::::::::-=====::::::++++++:::::
           -#####::::::::::::::::::::::=#####:::::::::::=#####::::::::::::#####-
           -#####::::::::::::::::::::::=#####:::::::::::=#####::::::::::::#####-
           -#####::::::::::::::::::::::=#####:::::::::::=#####::::::::::::#####-
           -#####::::::::::::::::::::::=#####:::::::::::=#####::::::::::::#####-
           -#####::::::::::::::::::::::=#####:::::::::::=#####::::::::::::#####-
           -#####:::::::::::::::::::::::-----::::::::::::-----::::::::::::#####-
           -#####:::::::::::::::::::::::::::::::::::::::::::::::::::::::::#####-
           -#####:::::::::::::::::::::::::::::::::::::::::::::::::::::::::#####-
           -#####:::::::::::************::::::::::::::::::::::************#####-
           -#####:::::::::::************::::::::::::::::::::::************#####-
           -#####:::::::::::************::::::::::::::::::::::************#####-
                 ######:::::::::::::::::::::::::::::::::::::::::::::#####
                 ######:::::::::::::::::::::::::::::::::::::::::::::#####
                 ######------:::::::::::::::::::::::::::::::::------#####
                 ######*****=:::::::::::::::::::::::::::::::::=*****#####
                 ######*****=:::::::::::::::::::::::::::::::::=*****#####
                 ######=============================================#####
                 ######:::::=***********###########***********::::::#####
                 ######:::::=***********###########***********::::::#####
                       #####=::::::::::=###########=:::::::::::=######
                       #####=::::::::::=###########=:::::::::::=######
                            -##########-           -###########-
                            -##########-           -###########-
*/
