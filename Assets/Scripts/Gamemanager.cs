using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gamemanager : MonoBehaviour
{
    public int money = 500;
    public TextMeshProUGUI moneyText;
    public int phase;

    [Header("NPC Spawns")]
    public PNJTemplate[] firstPhase;

    public PNJTemplate[] secondPhase;

    public PNJTemplate[] thirdPhase;

    public PNJTemplate[] fourthPhase;

    [Header("End Game")]
    public bool endOfGame;

    // Start is called before the first frame update
    void Start()
    {
        phase = 0;
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = money.ToString();
        CurrencyModify();
    }

    public PNJTemplate[] NPCSpawner()
    {
        switch(phase)
        {
            case 0:
                return firstPhase;

            case 1:
                return secondPhase;

            case 2:
                return thirdPhase;
            case 3:
                return fourthPhase;

        }
        return firstPhase;

    }

    public void NextPhase()
    {
        phase++;
    }

    public void CurrencyModify()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            money = money + 500;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) && money >= 0)
        {
            money = money - 500;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) && money <= 0)
        {
            money = 0;
            Debug.Log("T'as plus d'argent, arrête.");
        }
    }
}
