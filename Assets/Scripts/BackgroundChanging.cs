using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChanging : MonoBehaviour
{

    public Gamemanager gm;
    public SpriteRenderer spriteRenderer;
    public TimeAccelerator adManager;

    public Color dayColor;
    public Color eveningColor;
    public Color nightColor;
    public bool canChangeTime;
    public float duration; //durée en secondes

    public float dayLerpSpeed = 0f;
    public float nightLerpSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if(canChangeTime)
        {
            TimeOfDay();
        }


    }



    public void TimeOfDay()
    {
        //DAY TO EVENING
        spriteRenderer.color = Color.Lerp(dayColor, eveningColor, dayLerpSpeed);
        if(dayLerpSpeed < 1f && !adManager.hasAdPlayed)
        {
            dayLerpSpeed += Time.deltaTime / duration;
        }

        if (dayLerpSpeed < 1f && adManager.hasAdPlayed)
        {
            dayLerpSpeed += (Time.deltaTime / duration) * 2;
        }

        //EVENING TO NIGHT
        if (dayLerpSpeed >= 1f)
        {
            spriteRenderer.color = Color.Lerp(eveningColor, nightColor, nightLerpSpeed);

            if (nightLerpSpeed < 1f && !adManager.hasAdPlayed)
            {
                nightLerpSpeed += Time.deltaTime / duration;
            }

            if (nightLerpSpeed < 1f && adManager.hasAdPlayed)
            {
                nightLerpSpeed += (Time.deltaTime / duration) * 2;
            }
        }

    }

}
