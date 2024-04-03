using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_VisitShop : MonoBehaviour
{
    public GameObject[] npcList; //will contain all npcs in here to draw one at random
    public GameObject npcEntrance;


    private int randomNPCPull;
    DayNightCycle dayTracker;

    public int daysToShop;
    public static bool isNPCInShop = false;


    // Start is called before the first frame update
    void Start()
    {
        randomNPCPull = Random.Range(0, 7);
        daysToShop = Random.Range(1, 3);
        dayTracker = GetComponent<DayNightCycle>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (isNPCInShop == false) 
        {
            if (DayNightCycle.inGameHours >= 8) 
            {
                PullNPCFromList();
                TestDayRange();
                
            }
        }
    }

    public void PullNPCFromList()
    {
        //if time is between  8 am and 5pm spawn them
        GameObject currentNPC = Instantiate(npcList[randomNPCPull], npcEntrance.transform.position, Quaternion.identity);
        isNPCInShop = true;
    }

    void TestDayRange()
    {
        if (DayNightCycle.shopDays >= daysToShop)
        {
            PullNPCFromList();
        }
    }
}
