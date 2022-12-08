using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerSliding : MonoBehaviour
{
    public RectTransform pos;

    public Vector3 startPosX;
    public Vector3 endPosX;

    public float lerpSpeed = 1.0f;
    public bool open;

    public NPCManager npcManager;
    public Gamemanager gm;


    public void Update()
    {
        if(open)
        {
            gm.canStartTimer = false;
            pos.anchoredPosition = Vector3.Lerp(pos.anchoredPosition, endPosX, lerpSpeed * Time.deltaTime);

        }
        else
        {
            gm.canStartTimer = true;
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
    }
}

