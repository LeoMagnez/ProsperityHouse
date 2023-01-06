using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SellerSliding : MonoBehaviour
{
    public RectTransform pos;

    public Vector3 startPosX;
    public Vector3 endPosX;

    public float lerpSpeed = 1.0f;
    public bool open;

    public NPCManager npcManager;
    public Gamemanager gm;
    public LocalizedText dialogueText;
    public SellerDialogues dialogueManager;
    public int sellerDialogueIndex;
    public int tutorialTextIndex;
    public Button continueButton;
    public Button quitButton;
    public TimeAccelerator adManager;
    public bool tutorial;

    public void Start()
    {
        tutorial = true;
        quitButton.interactable = false;
        continueButton.GetComponent<Button>().enabled = true;
    }

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
        sellerDialogueIndex = 0;
        continueButton.interactable = true;
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
            dialogueText.Key = dialogueManager.GetTutorialText()[tutorialTextIndex];
            dialogueText.UpdateText();
        }
        else
        {
            dialogueText.Key = dialogueManager.GetSellerDialogues(Gamemanager.Instance.phase)[sellerDialogueIndex];
            dialogueText.UpdateText();
        }


    }



    public void ContinueSellerDialogue()
    {
        if (tutorial)
        {
            tutorialTextIndex += 1;
            if (tutorialTextIndex == dialogueManager.GetTutorialText().Length - 1)
            {
                quitButton.interactable = true;
                continueButton.interactable = false;
            }
            SellerDialogues();
        }

        else
        {
            sellerDialogueIndex += 1;
            if (sellerDialogueIndex == dialogueManager.GetSellerDialogues(Gamemanager.Instance.phase).Length - 1)
            {
                quitButton.interactable=true;
                continueButton.interactable = false;
            }
            SellerDialogues();
        }

    }
}

