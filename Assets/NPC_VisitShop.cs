using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_VisitShop : MonoBehaviour
{
    public GameObject[] npcList; //will contain all npcs in here to draw one at random

    private int randomNPCPull = Random.Range(0, 7);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PullNPCFromList();
    }

    void PullNPCFromList()
    {
        object[] npcList = new object[randomNPCPull];

        npcList[randomNPCPull - 1] = this;

        Instantiate(this);
    }
}
