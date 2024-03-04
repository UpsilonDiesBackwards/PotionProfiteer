using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
    {

    public Volume PostProcessingVolume;
    [Header ("Day Counter")]
    public float inGameSeconds; 
    public float inGameMinutes;
    public float inGameHours;
    private float clock;
    private float goblinRaid = 3;
    public TextMeshProUGUI whatTimeIsIt;
    public TextMeshProUGUI DayTracker; 
    public int dayNumber = 1;

    [Header("DayMan ahaaa")]
    
    public bool Day = true;
    public bool Night = false;

    [Header ("Beddie Byes")]
    public bool isInBed = false; //checks to see if the player has gone to sleep for the day
    public GameObject player; //needs to check if the player is in da bed, -- make bed a OnTrigger
    public bool StreetLampsOn;
    public GameObject[] StreetLamps; //all lights that are not the sun turn on

    //can have a sprite renderer here for a moon/stars if we want

    void Awake() {
        player = Player.Instance.gameObject;
        PostProcessingVolume = GetComponent<Volume>();
    }

    void Update()
    {
        CreatingTime();
        NightTime();
        DayEvents();
    }

    void CreatingTime()
    {
        inGameSeconds += Time.deltaTime * 20;

        if (inGameSeconds >= 6)
        {
            inGameSeconds = 0;
            inGameMinutes += 1;
        }
        if (inGameMinutes >= 3)
        {
            inGameHours += 0.30f;
            if (inGameMinutes >= 6)
            {
                inGameMinutes = 0;
                inGameHours += 1;
            }
        }
        if (inGameHours >= 24)
        {
            inGameHours = 0;
            dayNumber += 1;
        }
        clock = inGameHours;
        DayCounterAndTimeDisplay();
    }

    void NightTime()
    {
        if (inGameHours >= 0 && inGameHours < 6) //midnight til dawn
        {
            Night = true;
        }

        if (inGameHours >= 17.30f &&  inGameHours <= 19) //from 5:30pm til 7pm aka transition from afternoon to night
        {
            Day = false;
            Night = true;

            PostProcessingVolume.weight = (float)inGameMinutes / 60; 
            if (StreetLampsOn == false)
            {
                for (int i = 0; i < StreetLamps.Length; i++) //should turn on street lamps as a light source
                {
                    StreetLamps[i].SetActive(true);
                }
                StreetLampsOn = true;
            }
        }

        if (Night == true)
        {
            //make NPCs go "home" 
            //Any night time events
        }
        ZZZ();
    }

    void ZZZ()
    {
        if (Night == true && isInBed == true) 
        {
            dayNumber++;
            Night = false;
            Day = true;
            isInBed = false;

            //Kick player out of bed
        }
    }

    void DayEvents()
    {
        if (inGameHours >= 6 && inGameHours < 17.30f)
        {
            Night = false;
            Day = true;
            PostProcessingVolume.weight = 1 - (float)inGameMinutes / 60; //goes from 1 strength to 0

            if (StreetLampsOn == true)
            {
                for (int i = 0; i < StreetLamps.Length; i++) //should turn off street lamps as a light source
                {
                    StreetLamps[i].SetActive(false); ;
                }
                StreetLampsOn = false;
            }
        }



        for (int i = 0; i < goblinRaid; i ++)
        {
            //goblin raid
        }
        //crops grow different days
    }

    void DayCounterAndTimeDisplay()
    {
        if (clock > 0 && clock < 12)
        {
            whatTimeIsIt.text = inGameHours.ToString("00") + (":") + inGameMinutes.ToString("00") + "AM";
        }
        if (clock >= 12 && clock < 24)
        {
            whatTimeIsIt.text = clock.ToString("00") + (":") + inGameMinutes.ToString("00") + "PM";
        }
        DayTracker.text = "Day: " + dayNumber;
    }
}
