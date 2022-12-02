/*============================ PROSPERITY HOUSE - GAME MANAGER ============================
 
Created by LAGARDE Rosalie & MAGNEZ Léo - Copyright 2023 ETPA Toulouse, France

 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*============================ TABLE OF CONTENT ============================
 
VARIABLES
FUNCTIONS

 */
public class Gamemanager : MonoBehaviour
{
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
    public NPCManager npcManager;
    public float money = 500f;
    public TextMeshProUGUI moneyText;
    public int phase;
    public Item[] itemPrice;
    public int currItemBuying;

    [Header("NPC Spawns")]
    public PNJTemplate[] firstPhase;

    public PNJTemplate[] secondPhase;

    public PNJTemplate[] thirdPhase;

    public PNJTemplate[] fourthPhase;

    [Header("End Game")]
    public bool endOfGame;

    /*============================ FUNCTIONS ============================*/

    // Start is called before the first frame update
    void Start()
    {
        phase = 0;
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = money.ToString();
        CurrencyModify();
    }

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

    public void NextPhase()
    {
        phase++;
    }

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
        Debug.Log("ouioui");
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