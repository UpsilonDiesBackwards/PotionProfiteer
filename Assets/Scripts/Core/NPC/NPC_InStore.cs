using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;

public class NPC_InStore : MonoBehaviour
{
    public GameObject CounterDesk;
    public GameObject ExitDoor;
    public GameObject TalkWithNPCs;
    private float moveSpeed;

    public Animator upAnim;
    public static bool atCounter = false;
    private void Start()
    {
        moveSpeed = 4f;
        upAnim = GetComponent<Animator>();
        CounterDesk = GameObject.FindGameObjectWithTag("Desk");
        ExitDoor = GameObject.FindGameObjectWithTag("DoorExit");
        TalkWithNPCs = GameObject.FindGameObjectWithTag("Dialogue");
    }

    // Update is called once per frame
    void Update()
    {
        //PlayAnimation();
        MoveTowardsCounter();
        LeaveShop();
    }

    void MoveTowardsCounter()
    {
        if (atCounter == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, CounterDesk.transform.position, moveSpeed * Time.deltaTime); 
        }
        if (transform.position == CounterDesk.transform.position)
        {
            atCounter = true;
           // GameObject.FindGameObjectWithTag("Dialogue").SetActive(true);
        }
    }

    void LeaveShop()
    {
        if (DialogueBox.finishedDial == true)
        {
            HeadToExit();
        }
    }

    void HeadToExit()
    {
        transform.position = Vector2.MoveTowards(transform.position, ExitDoor.transform.position, moveSpeed * Time.deltaTime);

        if (transform.position == ExitDoor.transform.position)
        {
            NPC_Shopping.isNPCInShop = false;
            //gameObject.SetActive(false);
        }
    }
}
