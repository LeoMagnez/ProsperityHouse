using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerDialogues : MonoBehaviour
{
    public SellerSliding seller;
    public string[] tutorialText;
    public string[] sellerDialoguePhase1;
    public string[] sellerDialoguePhase2;
    public string[] sellerDialoguePhase3;
    public string[] sellerDialoguePhase4;


    public string[] GetSellerDialogues(int index)
    {
        switch (index)
        {
            case 1:
                return sellerDialoguePhase1;
            case 2:
                return sellerDialoguePhase2;
            case 3:
                return sellerDialoguePhase3;
            case 4:
                return sellerDialoguePhase4;
            default:
                return sellerDialoguePhase1;
        }
    }

    public string[]GetTutorialText()
    {
        return tutorialText;
    }
}
