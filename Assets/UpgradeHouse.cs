using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHouse : MonoBehaviour
{
    public Gamemanager gm;
    
    public void ButtonClick()
    {
        gm.NextPhase();
    }
}
