using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
    {
    [Header ("Day Counter")]
    private float dayTimer;
    private float goblinRaid = 3;
    public Text whatTimeIsIt;
    public int dayNumber = 1;

    [Header ("DayMan ahaaa")]
    public bool Day = true;
    public bool Night = false;

    [Header ("Beddie Byes")]
    public bool isInBed = false; //checks to see if the player has gone to sleep for the day
    public GameObject player; //needs to check if the player is in da bed, -- make bed a OnTrigger
        // Update is called once per frame

    void Awake() {
        player = Player.Instance.gameObject;
    }

    void Update()
    {
        dayTimer += Time.deltaTime;
        whatTimeIsIt.text = dayTimer.ToString("00:00");

        if (dayTimer >= 16f )
        {
            Night = true;
        }
    }


    void NightTime()
    {
        if (Night == true)
        {
            //do night time shit like make the light strength weaker
            //make NPCs go "home" 
            //Any night time events
        }
    }

    void ZZZ()
    {
        if (Night == true && isInBed == true) 
        {
            //go to sleep bitch 
            dayNumber++;
            Night = false;
            Day = true;
            isInBed = false;
            dayTimer = 0f;
            //Kick player out of bed
        }
    }

    void DayEvents()
    {
        
        for (int i = 0; i < goblinRaid; i ++)
        {
            //goblin raid
        }
        //crops grow different days
    }
}
