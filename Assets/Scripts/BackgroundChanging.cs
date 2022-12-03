using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChanging : MonoBehaviour
{

    public Gamemanager gm;
    public SpriteRenderer spriteRenderer;

    public Color dayColor;
    public Color eveningColor;
    public Color nightColor;
    public float duration; //durée en secondes

    float dayLerpSpeed = 0f;
    float nightLerpSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        TimeOfDay();

    }



    public void TimeOfDay()
    {
        //DAY TO EVENING
        spriteRenderer.color = Color.Lerp(dayColor, eveningColor, dayLerpSpeed);
        if(dayLerpSpeed < 1f)
        {
            dayLerpSpeed += Time.deltaTime / duration;
        }

        //EVENING TO NIGHT
        if(dayLerpSpeed >= 1f)
        {
            spriteRenderer.color = Color.Lerp(eveningColor, nightColor, nightLerpSpeed);

            if (nightLerpSpeed < 1f)
            {
                nightLerpSpeed += Time.deltaTime / duration;
            }
        }

    }

}
