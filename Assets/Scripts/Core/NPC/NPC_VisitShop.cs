using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_VisitShop : MonoBehaviour
{
    public GameObject[] npcList; //will contain all npcs in here to draw one at random
    public GameObject npcEntrance;


    private int randomNPCPull;
    DayNightCycle dayTracker;

    private bool isNPCInShop = false;

    int daysChecker;
    int dayReturnRange;
    // Start is called before the first frame update
    void Start()
    {
        randomNPCPull = Random.Range(0, 7);
        dayTracker = GetComponent<DayNightCycle>();

        daysChecker = dayTracker.dayNumber;
        dayReturnRange = Random.Range(3, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if (isNPCInShop == false) 
        {
            PullNPCFromList();
            isNPCInShop = true;
        }
    }

    public void PullNPCFromList()
    {
        GameObject currentNPC = Instantiate(npcList[randomNPCPull], npcEntrance.transform.position, Quaternion.identity);
    }
}
