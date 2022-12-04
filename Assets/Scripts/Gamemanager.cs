/*============================ PROSPERITY HOUSE - GAME MANAGER ============================
 
Created by LAGARDE Rosalie & MAGNEZ Léo - Copyright 2023 ETPA Toulouse, France

 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*============================ TABLE OF CONTENT ============================
 
SINGLETON

VARIABLES

FUNCTIONS
    -NPCs
    -SELLER
    -PHASE
    -MONEY

 */
public class Gamemanager : MonoBehaviour
{
    /* ============================ SINGLETON ============================*/
    #region Singleton
    private static Gamemanager instance;
    
    // Méthode d'accès statique (point d'accès global)
    public static Gamemanager Instance { get { return instance; } private set { instance = value; } }

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    // Suppression d'une instance précédente (sécurité...sécurité...)

        instance = this;
    }
    #endregion


    /* ============================ VARIABLES ============================*/
    #region VARIABLES

    [Header("References")]
    public NPCManager npcManager;
    

    [Header("Currency")]
    public float money = 500f;
    public TextMeshProUGUI moneyText;
    public Item[] itemPrice;
    public TextMeshProUGUI upgradeText;
    public float upgradeCost;

    [Header("Phase")]
    public int phase;

    [Header("Day/Night Cycle")]
    public SellerSliding sellerPanelSliding;
    public float timer;
    public bool canStartTimer;


    [Header("NPC Spawns")]
    public PNJTemplate[] firstPhase;

    public PNJTemplate[] secondPhase;

    public PNJTemplate[] thirdPhase;

    public PNJTemplate[] fourthPhase;

    [Header("Seller")]
    public GameObject itemsFirstPhase;
    public GameObject itemsSecondPhase;
    public GameObject itemsThirdPhase;
    public GameObject itemsFourthPhase;

    [Header("End Game")]
    public bool endOfGame;
    #endregion

    /*============================ FUNCTIONS ============================*/
    #region FUNCTIONS


    // Start is called before the first frame update
    void Start()
    {
        phase = 0;
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = money.ToString();
        upgradeText.text = "Upgrade - " + upgradeCost.ToString();
        CurrencyModify();



        SellerAppears();
        SellerItemsSpawner();
        UpgradeCost();


        
    }

    /*============================ NPCs ============================*/
    #region NPCs

    public PNJTemplate[] NPCSpawner()
    {
        switch(phase)
        {
            case 0:
                return firstPhase;

            case 1:
                return secondPhase;

            case 2:
                return thirdPhase;

            case 3:
                return fourthPhase;

        }
        return firstPhase;
        
    }
    #endregion

    /*============================ SELLER ============================*/
    #region SELLER

    public void SellerItemsSpawner()
    {
        if(phase == 0)
        {
            itemsFirstPhase.SetActive(true);
            itemsSecondPhase.SetActive(false);
            itemsThirdPhase.SetActive(false);
            itemsFourthPhase.SetActive(false);
        }

        if (phase == 1)
        {
            itemsFirstPhase.SetActive(true);
            itemsSecondPhase.SetActive(true);
            itemsThirdPhase.SetActive(false);
            itemsFourthPhase.SetActive(false);
        }

        if (phase == 2)
        {
            itemsFirstPhase.SetActive(true);
            itemsSecondPhase.SetActive(true);
            itemsThirdPhase.SetActive(true);
            itemsFourthPhase.SetActive(false);
        }

        if (phase == 3)
        {
            itemsFirstPhase.SetActive(true);
            itemsSecondPhase.SetActive(true);
            itemsThirdPhase.SetActive(true);
            itemsFourthPhase.SetActive(true);
        }
    }

    public void SellerAppears()
    {
        if (canStartTimer)
        {
            timer -= 1f * Time.deltaTime;
        }

        if (timer <= 0f)
        {
            canStartTimer = false;
            sellerPanelSliding.SellerPanelOpen();
        }

    }
    #endregion

    /*============================ PHASE ============================*/
    #region PHASE



    public void NextPhase()
    {
        phase++;
    }
    #endregion

    /*============================ MONEY ============================*/
    #region MONEY

    public void CurrencyModify()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            money = money + 500;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) && money >= 0)
        {
            money = money - 500;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) && money <= 0)
        {
            money = 0;
            Debug.Log("T'as plus d'argent, arrête.");
        }
    }

    public void SellResearchedItem()
    {
        money += npcManager.item.itemBuyPrice + npcManager.itemFinalValue;
    }

    public void BuyItemFromSeller(Item _item)
    {
        money -= _item.itemPrice;
        //Debug.Log("ouioui");
    }

    public void UpgradeCost()
    {
        if(phase == 0)
        {
            upgradeCost = 250f;
        }

        if (phase == 1)
        {
            upgradeCost = 700f;
        }

        if (phase == 2)
        {
            upgradeCost = 1500f;
        }

        if (phase == 3)
        {
            upgradeCost = 4500f;
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