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
    public ItemTemplate searchedItemTemplate;

    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemBuyPriceText;
    public TextMeshProUGUI itemMargin;
    public Image itemArtwork;
    public float itemMarginMultiplier;
    public float itemFinalValue;

    [Header("Special NPCs")]
    [SerializeField]
    private PNJTemplate[] specialNPC;
    private int specialNPCCurrent;

    [SerializeField]
    private PNJTemplate[] npcList;
    private int npcCurrent;

    [SerializeField]
    public ItemTemplate[] npcCurrentItems;
    public int itemCurrent;

    public Gamemanager gm;
    public InventoryManager inventoryManager;
    public Button sellButton;

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
        if(!inventoryManager.itemDictionnary.TryGetValue(searchedItemTemplate, out InventoryItem item))
        {
            sellButton.interactable = false;
        }
        else
        {
            sellButton.interactable = true;
        }
    }
    /*============================ Classic NPCs ============================*/
    #region Classic NPCs
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
        searchedItemTemplate = npcCurrentItems[itemCurrent];
        itemNameText.text = searchedItemTemplate.itemName;
        itemBuyPriceText.text = searchedItemTemplate.itemSellingPrice.ToString();
        itemArtwork.sprite = searchedItemTemplate.itemArtwork;
        
    }

    public void ReloadMargin()
    {
        itemMarginMultiplier = Random.Range(0.15f, 0.3f);
        itemFinalValue = Mathf.RoundToInt(searchedItemTemplate.itemSellingPrice * itemMarginMultiplier);
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
    #endregion

    /*============================ Special NPCs ============================*/
    #region Special NPCs
    
    public void ReloadSpecialNPCs()
    {
        npc = specialNPC[specialNPCCurrent];
        npcNameText.text = npc.npcName;
        npcJobText.text = npc.npcJob;
        npcDialogueText.text = npc.npcDialogue;
        npcArtwork.sprite = npc.npcArtwork;
    }
    
    public void SpecialNPCTrade()
    {
        specialNPC = gm.SpecialNPCSpawner();
        if(specialNPCCurrent < specialNPC.Length - 1)
        {
            specialNPCCurrent += 1;
        }
        else
        {
            specialNPCCurrent = 0;
        }
        npcCurrentItems = specialNPC[specialNPCCurrent].searchedItems;
        itemCurrent = Random.Range(0, npcCurrentItems.Length);
        ReloadSpecialNPCs();
        ReloadItems();
        ReloadMargin();
    }
    #endregion

    //-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
    //           Vente d'item requis
    //-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

    public void SellItem()
    {

        //Items
        if(inventoryManager.itemDictionnary.TryGetValue(searchedItemTemplate, out InventoryItem item))
        {
            gm.inventoryManager.Remove(searchedItemTemplate);
            gm.money += searchedItemTemplate.itemSellingPrice + itemFinalValue;

        }

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
