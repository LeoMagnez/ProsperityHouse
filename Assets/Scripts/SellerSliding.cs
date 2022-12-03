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



    void Update()
    {



    }

    public void SellerPanelOpen()
    {

        pos.anchoredPosition = Vector3.Lerp(pos.anchoredPosition, endPosX, lerpSpeed * Time.deltaTime);

    }

    public void SellerPanelClose()
    {
        pos.anchoredPosition = Vector3.Lerp(pos.anchoredPosition, startPosX, lerpSpeed * Time.deltaTime);
    }
}

