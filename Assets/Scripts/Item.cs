using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int itemPrice;

    public ItemTemplate template;

    public void OnBuy()
    {
        Gamemanager.Instance.BuyItemFromSeller(this);

        //Debug.Log("babar");
    }
}
