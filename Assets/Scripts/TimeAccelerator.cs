using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAccelerator : MonoBehaviour
{
    public Gamemanager gm;
    public GameObject adDisplay;
    public GameObject adPopUp;
    public Slider loadingBar;
    public LocalizedText adText;
    public LocalizedText yes_text;
    public LocalizedText no_text;
    public float adTimer;
    public float maxTimer;
    public bool adRunning;
    public bool isPopUpActive;

    public bool hasAdPlayed;
    public Button playAd;
    public BackgroundChanging bgChanging;

    // Start is called before the first frame update
    public void StartAd()
    {
        adPopUp.SetActive(false);
        adDisplay.SetActive(true);
        adRunning = true;
        adTimer = 0f;
   
        //met timer à zero

    }

    // Update is called once per frame
    void Update()
    {
        if (adRunning)
        {
            gm.canStartTimer = false;
            gm.backgroundChanging.canChangeTime = false;
            gm.canStartNPCTimer = false;
            if (adTimer < maxTimer)
            {
                adTimer += Time.deltaTime;

                loadingBar.value = adTimer / maxTimer;
                StartCoroutine(TimeAcceleration());
            }

            else
            {
                adRunning = false;
                adDisplay.SetActive(false);
                gm.backgroundChanging.canChangeTime = true;
                //desactive la pub
            }

        }

        if(isPopUpActive)
        {
            gm.canStartTimer = false;
            gm.backgroundChanging.canChangeTime = false;
            gm.canStartNPCTimer = false;
            adPopUp.SetActive(true);
            adText.UpdateText();
            yes_text.UpdateText();
            no_text.UpdateText();
        }
        else
        {
            gm.backgroundChanging.canChangeTime = true;
            adPopUp.SetActive(false);
        }

    }

    public IEnumerator TimeAcceleration()
    {
        yield return new WaitForSeconds(15f);

        hasAdPlayed = true;
        playAd.interactable = false;
        isPopUpActive = false;

    }

    public IEnumerator ReturnTimeToNormal()
    {
        yield return new WaitForSeconds(120f);

        hasAdPlayed = false;
        playAd.interactable = true;

    }

    public void PopUp()
    {
        isPopUpActive = true;
    }

    public void ClosePopUp()
    {
        isPopUpActive=false;
    }
}
