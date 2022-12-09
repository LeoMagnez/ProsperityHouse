using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyItem : MonoBehaviour
{
    public Button button;

    public ItemTemplate template;

    public void OnBuy()
    {
        if(Gamemanager.Instance.money > 0)
        {

            Gamemanager.Instance.BuyItemFromSeller(this);
        }
        else
        {
            button.interactable = false;
            Gamemanager.Instance.money = 0;
            Debug.Log("Impossible");
        }

        //Debug.Log("babar");
    }




}
