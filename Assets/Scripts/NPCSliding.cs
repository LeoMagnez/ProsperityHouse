using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCSliding : MonoBehaviour
{
    public RectTransform pos;
    public Button button;

    public Vector3 startPosX;
    public Vector3 endPosX;

    public float lerpSpeed = 1.0f;
    public bool open;

    public NPCManager npcManager;
    public Gamemanager gm;
    public TimeAccelerator adManager;
    public int easterEgg;

    

    void Update()
    {
        if(open)
        {
            gm.canStartTimer = false;
            pos.anchoredPosition = Vector3.Lerp(pos.anchoredPosition, endPosX, lerpSpeed * Time.deltaTime);
        }
        else
        {
            if(!adManager.adRunning)
            {
                gm.canStartTimer = true;
            }
            if(!adManager.isPopUpActive)
            {
                gm.canStartTimer = true;
            }

            pos.anchoredPosition = Vector3.Lerp(pos.anchoredPosition, startPosX, lerpSpeed * Time.deltaTime);
        }

        if(gm.timer <= 3f && !open)
        {
            button.interactable = false;
        }

        if(gm.timer >= 3f && !open)
        {
            button.interactable = true;
        }

        if(!gm.isNotificationActive)
        {
            button.interactable = false;
            gm.notification.SetActive(false);
        }
        if(gm.isNotificationActive)
        {
            button.interactable = true;
            gm.notification.SetActive(true);
        }
        if(adManager.adRunning)
        {
            gm.canStartTimer = false;
        }
        
    }

    public void ButtonClick()
    {
        open = !open;

        if(open)
        {
            gm.counter += 1;

            gm.canStartTimer = false;
            gm.canStartNPCTimer = false;
            gm.backgroundChanging.canChangeTime = false;
            gm.npcTimer = 10f;
            easterEgg = Random.Range(0, 10000);

            if(gm.counter == 3)
            {
                npcManager.SpecialNPCTrade();
            }

            else if (easterEgg == 2645)
            {
                npcManager.EasterEgg();
            }
            else
            {
                npcManager.NewNPCTrade();
            }



        }
        else
        {
            gm.isNotificationActive = false;
            gm.canStartTimer = true;
            gm.canStartNPCTimer = true;
            gm.backgroundChanging.canChangeTime = true;
            npcManager.ReloadMargin();
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
