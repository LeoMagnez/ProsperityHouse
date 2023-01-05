/*============================ PROSPERITY HOUSE - NPC MANAGER ============================
 
Created by LAGARDE Rosalie & MAGNEZ Léo - Copyright 2023 ETPA Toulouse, France

 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*============================ TABLE OF CONTENT ============================
 
VARIABLES

FUNCTIONS
    -CLASSIC NPCs
    -SPECIAL NPCs
    -SELL ITEM
 */
public class NPCManager : MonoBehaviour
{
    /* ============================ VARIABLES ============================*/
    #region VARIABLES
    [Header("NPC Characteristics")]
    public PNJTemplate npc;

    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI npcJobText;
    public LocalizedText npcDialogueText;
    public Image npcArtwork;
    public int npcDialogueIndex;
    public Button continueDialogueButton;
    public bool isSpecialNPC;

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

    [Header("Easter Egg")]
    public PNJTemplate[] easterEggNPC;
    private int easterEggNPCCurrent;

    [SerializeField]
    private PNJTemplate[] npcList;
    private int npcCurrent;

    [SerializeField]
    public ItemTemplate[] npcCurrentItems;
    public int itemCurrent;

    public Gamemanager gm;
    public InventoryManager inventoryManager;
    public Button sellButton;
    public TimeAccelerator adManager;
    public ParticleSystem coinParticles;
    #endregion


    /* ============================ FUNCTIONS ============================*/
    #region FUNCTIONS
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

        if(gm.canStartTimer && gm.canStartNPCTimer && !adManager.hasAdPlayed)
        {
            gm.npcTimer -= 1 * Time.deltaTime;
        }

        if (gm.canStartTimer && gm.canStartNPCTimer && adManager.hasAdPlayed)
        {
            gm.npcTimer -= 2 * Time.deltaTime;
        }

        if (gm.npcTimer <= 0)
        {
            gm.isNotificationActive = true;
        }

    }
    /*============================ CLASSIC NPCs ============================*/
    #region Classic NPCs
    public void ReloadNPC()
    {
        npc = npcList[npcCurrent];
        npcNameText.text = npc.npcName;
        npcJobText.text = npc.npcJob;
        npcDialogueText.Key = npc.GetDialoguesLists(Gamemanager.Instance.phase)[npcDialogueIndex];
        npcDialogueText.UpdateText();
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
        isSpecialNPC = false;
        continueDialogueButton.interactable = true;
        npcDialogueIndex = 0;
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
        ReloadNPC();
        ReloadItems();
        ReloadMargin();
    }

    public void NPCDialogues()
    {
        if(!isSpecialNPC)
        {
            npcDialogueIndex += 1;
            if (npcDialogueIndex == npc.GetDialoguesLists(Gamemanager.Instance.phase).Length-1)
            {
                continueDialogueButton.interactable = false;
            }
            ReloadNPC();
        }

        if(isSpecialNPC)
        {
            npcDialogueIndex += 1;
            if (npcDialogueIndex == npc.GetDialoguesLists(Gamemanager.Instance.phase).Length-1)
            {
                continueDialogueButton.interactable = false;
            }
            ReloadSpecialNPCs();
        }


    }


    #endregion

    /*============================ SPECIAL NPCs ============================*/
    #region Special NPCs
    
    public void ReloadSpecialNPCs()
    {

        npc = specialNPC[specialNPCCurrent];
        npcNameText.text = npc.npcName;
        npcJobText.text = npc.npcJob;
        npcDialogueText.Key = npc.GetDialoguesLists(Gamemanager.Instance.phase)[npcDialogueIndex];
        npcDialogueText.UpdateText();
        npcArtwork.sprite = npc.npcArtwork;
    }
    
    public void SpecialNPCTrade()
    {
        isSpecialNPC = true;
        continueDialogueButton.interactable = true;
        npcDialogueIndex = 1;
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

    public void ReloadEasterEgg()
    {
        npc = easterEggNPC[easterEggNPCCurrent];
        npcNameText.text = npc.npcName;
        npcJobText.text = npc.npcJob;
        npcDialogueText.Key = npc.GetDialoguesLists(Gamemanager.Instance.phase)[npcDialogueIndex];
        npcDialogueText.UpdateText();
        npcArtwork.sprite = npc.npcArtwork;
    }

    public void EasterEgg()
    {
        if(easterEggNPCCurrent < easterEggNPC.Length - 1)
        {
            easterEggNPCCurrent += 1;
        }
        else
        {
            easterEggNPCCurrent = 0;
        }
        npcCurrentItems = easterEggNPC[easterEggNPCCurrent].searchedItems;
        itemCurrent = Random.Range(0, npcCurrentItems.Length);
        ReloadEasterEgg();
        ReloadItems();
        ReloadMargin();
    }
    #endregion

    /*============================ SELL ITEM ============================*/
    #region Sell Item
    public void SellItem()
    {

        //Items
        if(inventoryManager.itemDictionnary.TryGetValue(searchedItemTemplate, out InventoryItem item))
        {
            coinParticles.Play();
            gm.inventoryManager.Remove(searchedItemTemplate);
            gm.money += searchedItemTemplate.itemSellingPrice + itemFinalValue;

        }

    }
    #endregion

    #endregion


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
