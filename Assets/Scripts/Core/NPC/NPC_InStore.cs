using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;

public class NPC_InStore : MonoBehaviour
{
    private GameObject NPC;
    public GameObject CounterDesk;

    DayNightCycle dayTracker;

    private int daysChecker;
    private int randomDayReturns;

    public float moveSpeed = 0.02f;
    // Start is called before the first frame update
    void Start()
    {
        dayTracker = GetComponent<DayNightCycle>();

        daysChecker = dayTracker.dayNumber;
        randomDayReturns = UnityEngine.Random.Range(3, 6);
    }

    // Update is called once per frame
    void Update()
    {
        ReturnToShop();
    }

    void MoveTowardsCounter()
    {
        transform.position = Vector2.MoveTowards(transform.position, CounterDesk.transform.position, moveSpeed * Time.deltaTime);
    }

    void ReturnToShop()
    {
        if (daysChecker > randomDayReturns)
        {
            MoveTowardsCounter();
        }
    }
}
