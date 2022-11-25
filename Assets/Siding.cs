using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siding : MonoBehaviour
{
    public RectTransform pos;

    public Vector3 startPosX;
    public Vector3 endPosX;

    public float lerpSpeed = 1.0f;
    bool open;
    

    void Update()
    {
        if(open)
        {
            pos.anchoredPosition = Vector3.Lerp(pos.anchoredPosition, endPosX, lerpSpeed * Time.deltaTime);
        }
        else
        {
            pos.anchoredPosition = Vector3.Lerp(pos.anchoredPosition, startPosX, lerpSpeed * Time.deltaTime);
        }
        
    }

    public void ButtonClick()
    {
        open = !open;
    }
}
