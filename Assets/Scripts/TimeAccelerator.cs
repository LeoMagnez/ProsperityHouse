using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAccelerator : MonoBehaviour
{
    public Gamemanager gm;
    public GameObject adDisplay;
    public Slider loadingBar;
    public float adTimer;
    public float maxTimer;
    public bool adRunning;

    public bool hasAdPlayed;
    public Button playAd;
    public BackgroundChanging bgChanging;

    // Start is called before the first frame update
    public void StartAd()
    {
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
                gm.canStartTimer = true;
                gm.canStartNPCTimer=true;
                gm.backgroundChanging.canChangeTime = true;
                //desactive la pub
            }

        }

    }

    public IEnumerator TimeAcceleration()
    {
        yield return new WaitForSeconds(30f);

        hasAdPlayed = true;
        playAd.interactable = false;

    }

    public IEnumerator ReturnTimeToNormal()
    {
        yield return new WaitForSeconds(120f);

        hasAdPlayed = false;
        playAd.interactable = true;

    }

}
