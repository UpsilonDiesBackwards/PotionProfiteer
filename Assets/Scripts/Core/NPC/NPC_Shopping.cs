using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Shopping : MonoBehaviour
{
    public GameObject[] npcList; //will contain all npcs in here to draw one at random
    public GameObject npcEntrance;


    private int randomNPCPull;

    public int daysToShop;
    public static bool isNPCInShop = false;
    public static bool ResetDays = false;

    // Start is called before the first frame update
    void Start()
    {
        randomNPCPull = Random.Range(0, 7);
        daysToShop = Random.Range(0, 2);
        GetComponent<DayNightCycle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isNPCInShop == false)
        {
            if (DayNightCycle.inGameHours >= 8)
            {
               TestDayRange();
                ResetDays = false;
            }
        }
    }

    public void PullNPCFromList()
    {
        //if time is between  8 am and 5pm spawn them
        Instantiate(npcList[randomNPCPull], npcEntrance.transform.position, Quaternion.identity);
        isNPCInShop = true;
    }

    void TestDayRange()
    {
        if (DayNightCycle.shopDays >= daysToShop)
        {
            PullNPCFromList();
            ResetDays = true;
        }
    }
}