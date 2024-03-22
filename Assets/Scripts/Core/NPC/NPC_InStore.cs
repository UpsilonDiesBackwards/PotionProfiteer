using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;

public class NPC_InStore : MonoBehaviour
{
    public GameObject CounterDesk;
    private float moveSpeed;
    // Update is called once per frame
    void Update()
    {
        MoveTowardsCounter();
    }

    void MoveTowardsCounter()
    {
        moveSpeed = 4f;
        transform.position = Vector2.MoveTowards(transform.position, CounterDesk.transform.position, moveSpeed * Time.deltaTime);
    }
}
