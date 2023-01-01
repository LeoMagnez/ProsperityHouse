using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTransitions : MonoBehaviour
{
    public GameObject fadingSquare;
    public int fadeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Gamemanager.Instance.endOfGame == true)
        {
            StartCoroutine(FadeBlackSquare());
        }

    }

    public IEnumerator FadeBlackSquare(bool fadeToBlack = true, int fadingSpeed = 5)
    {
        fadingSpeed = fadeSpeed;
        Color objectColor = fadingSquare.GetComponent<Image>().color;
        float fadeAmount;

        if(fadeToBlack)
        {
            while(fadingSquare.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadingSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                fadingSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        else
        {
            while(fadingSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadingSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                fadingSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }

        yield return new WaitForEndOfFrame();
    }
}
