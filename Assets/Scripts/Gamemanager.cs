/*============================ PROSPERITY HOUSE - GAME MANAGER ============================
 
Created by LAGARDE Rosalie & MAGNEZ Léo - Copyright 2023 ETPA Toulouse, France

 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    public InventoryManager inventoryManager;
    public BackgroundChanging backgroundChanging;
    public NPCSliding npcSliding;
    public TimeAccelerator adManager;
    public ParticleSystem upgradeParticles;
    public GameObject languageChooser;
    public LocalizedText quitSeller;
    public LocalizedText sellerName;
    public AudioManager audioManager;
    

    [Header("Currency")]
    public float money = 500f;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI upgradeCostText;
    public LocalizedText upgradeText;
    public float upgradeCost;
    public Button upgradeButton;

    [Header("Phase")]
    public int phase;
    public GameObject firstHouse;
    public GameObject secondHouse;
    public GameObject thirdHouse;
    public GameObject fourthHouse;

    [Header("Day/Night Cycle")]
    public SellerSliding sellerPanelSliding;
    public float timer;
    public bool canStartTimer;
    public GameObject notification;
    public bool isNotificationActive;


    [Header("NPC Spawns")]
    public int counter = 0;
    public bool canStartNPCTimer;
    public float npcTimer;

    public PNJTemplate[] firstPhase;

    public PNJTemplate[] secondPhase;

    public PNJTemplate[] thirdPhase;

    public PNJTemplate[] fourthPhase;

    [Header("Special NPCs")]
    public PNJTemplate[] specialFirstPhase;

    public PNJTemplate[] specialSecondPhase;

    public PNJTemplate[] specialThirdPhase;

    public PNJTemplate[] specialFourthPhase;

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
        Application.targetFrameRate = 60;
        sellerPanelSliding.open = true;
        phase = 0;
        firstHouse.SetActive(true);
        secondHouse.SetActive(false);
        thirdHouse.SetActive(false);
        fourthHouse.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = money.ToString();
        upgradeCostText.text = " - " + upgradeCost.ToString();
        upgradeText.UpdateText();
        quitSeller.UpdateText();
        sellerName.UpdateText();
        CurrencyModify();


        SellerAppears();
        SellerItemsSpawner();
        UpgradeCost();
        CanBuy();
        CanUpgrade();

        if(money < 0)
        {
            money = 0;
        }


        
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

    public PNJTemplate[] SpecialNPCSpawner()
    {
        switch (phase)
        {
            case 0:
                return specialFirstPhase;

            case 1:
                return specialSecondPhase;

            case 2:
                return specialThirdPhase;

            case 3:
                return specialFourthPhase;

        }
        return specialFirstPhase;

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
        if (canStartTimer && !adManager.hasAdPlayed)
        {
            timer -= 1f * Time.deltaTime;
        }

        if(canStartTimer && adManager.hasAdPlayed)
        {
            StartCoroutine(adManager.ReturnTimeToNormal());
            timer -= 2f * Time.deltaTime;
           
        }

        if (timer <= 0f)
        {
            canStartTimer = false;
            sellerPanelSliding.open = true;
        }

    }

    public void CanBuy()
    {
        var tempItems = FindObjectsOfType<BuyItem>();
        for (int i = 0; i < tempItems.Length; i++)
        {
            if(money < tempItems[i].template.itemBuyingPrice)
            {
                tempItems[i].button.interactable = false;
            }
            else
            {
                tempItems[i].button.interactable = true;
            }

        }
    }
    #endregion

    /*============================ PHASE ============================*/
    #region PHASE



    public void NextPhase()
    {
        phase++;
        counter = 0;

        switch (phase)
        {
            case 0:
                firstHouse.SetActive(true);
                secondHouse.SetActive(false);
                thirdHouse.SetActive(false);
                fourthHouse.SetActive(false);
                break;
            case 1:
                firstHouse.SetActive(false);
                secondHouse.SetActive(true);
                thirdHouse.SetActive(false);
                fourthHouse.SetActive(false);
                break;
            case 2:
                firstHouse.SetActive(false);
                secondHouse.SetActive(false);
                thirdHouse.SetActive(true);
                fourthHouse.SetActive(false);
                break;
            case 3:
                firstHouse.SetActive(false);
                secondHouse.SetActive(false);
                thirdHouse.SetActive(false);
                fourthHouse.SetActive(true);
                break;

        }
        
    }

    public void EndOfGame()
    {
        if(phase == 3 && counter == 3)
        {
            endOfGame = true;
        }
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


    public void BuyItemFromSeller(BuyItem _item)
    {
        if(money > _item.template.itemBuyingPrice)
        {
            money -= _item.template.itemBuyingPrice;
            inventoryManager.Add(_item.template);
        }

    }

    public void FinalTrade(BuyItem _item)
    {
        if(phase == 3 && counter == 3)
        {
            inventoryManager.Add(_item.template);
        }
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
            upgradeButton.gameObject.SetActive(false);
            upgradeCost = 4500f;
        }

    }

    public void CanUpgrade()
    {
        if(money < upgradeCost)
        {
            upgradeButton.interactable = false;
        }
        else
        {
            upgradeButton.interactable = true;
        }
    }
    #endregion

    /*============================ LANGUAGE ============================*/
    #region LANGUAGE

    public void ChangeLanguage()
    {
        languageChooser.SetActive(false);
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