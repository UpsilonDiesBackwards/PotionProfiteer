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
    public static bool isTalking = false;
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
        if (atCounter == false) //move towards desk
        {
            transform.position = Vector2.MoveTowards(transform.position, CounterDesk.transform.position, moveSpeed * Time.deltaTime); 
        }
        if (transform.position == CounterDesk.transform.position && !isTalking) //if at desk start dialogue - after dialogue leave shop
        {
            Debug.Log(CounterDesk.transform.position);
            Debug.Log(gameObject.transform.position);
            atCounter = true;
            NPC_Shopping.isNPCInShop = true;
            Invoke("OpenDialogue", 0.2f);
            Invoke("HeadToExit", 3f);
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

        if (transform.position == ExitDoor.transform.position) //should remove the NPC from the shop and reset the shopping visits
        {
            gameObject.SetActive(false);
            NPC_Shopping.isNPCInShop = false; 
        }
    }

    void OpenDialogue()
    {
        if (atCounter && Input.GetKeyDown(KeyCode.E))
        {
            TalkWithNPCs.SetActive(true); //activates dialogue box if unticked
        }
    }
}
