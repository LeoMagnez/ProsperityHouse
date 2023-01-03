using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SellerSliding : MonoBehaviour
{
    public RectTransform pos;

    public Vector3 startPosX;
    public Vector3 endPosX;

    public float lerpSpeed = 1.0f;
    public bool open;

    public NPCManager npcManager;
    public Gamemanager gm;
    public TextMeshProUGUI dialogueText;
    public TimeAccelerator adManager;
    public bool tutorial;


    public void Update()
    {
        if(open)
        {
            gm.canStartTimer = false;
            gm.canStartNPCTimer = false;
            gm.backgroundChanging.canChangeTime = false;
            pos.anchoredPosition = Vector3.Lerp(pos.anchoredPosition, endPosX, lerpSpeed * Time.deltaTime);
            SellerDialogues();
            

        }
        else
        {
            if(!gm.npcSliding.open && !adManager.isPopUpActive)
            {
                gm.canStartTimer = true;
                gm.canStartNPCTimer = true;
                gm.backgroundChanging.canChangeTime = true;
            }
            if(adManager.adRunning)
            {
                gm.canStartTimer= false;
                gm.backgroundChanging.canChangeTime = false;
            }

            pos.anchoredPosition = Vector3.Lerp(pos.anchoredPosition, startPosX, lerpSpeed * Time.deltaTime);

        }
    }

    public void ButtonClick()
    {
        open = !open;
        gm.backgroundChanging.canChangeTime = true;
        if(gm.timer <= 0)
        {
            gm.timer = 90f;
            gm.backgroundChanging.dayLerpSpeed = 0f;
            gm.backgroundChanging.nightLerpSpeed = 0f;
        }
        if(tutorial)
        {
            tutorial = false;
        }
    }

    public void SellerDialogues()
    {
        if (tutorial)
        {
            dialogueText.text = "Tu peux m'acheter des marchandises pour les revendre plus cher à tes clients !";
        }
        else
        {
            switch (gm.phase)
            {
                case 0:
                    dialogueText.text = "Que veux-tu acheter aujourd'hui ?";
                    break;
                case 1:
                    dialogueText.text = "Dialogue 2";
                    break;
                case 2:
                    dialogueText.text = "Dialogue 3";
                    break;
                case 3:
                    dialogueText.text = "Dialogue 4";
                    break;
            }
        }

    }
}

