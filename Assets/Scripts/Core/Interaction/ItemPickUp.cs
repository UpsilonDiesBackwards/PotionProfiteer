using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour {
    public float speed = 2.0f;
    public float pickUpDist = 2.5f;
    public float despawnTime = 10.0f;

    void Awake() {
    }

    void Update() {
        despawnTime -= Time.deltaTime;
        if (despawnTime <= 0) { Destroy(gameObject); }

        float lootDist = Vector3.Distance(transform.position, Player.Instance.transform.position);

        transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, 
            speed * Time.deltaTime);

        if (lootDist > pickUpDist) { return; } // player too far, don't move anwyhere

        if (lootDist < 0.1f) { Destroy(gameObject); } // player got close, destroy pickup
    }
}
