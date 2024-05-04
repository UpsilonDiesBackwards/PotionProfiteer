using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication;
using UnityEngine;

public class ItemPickUp : MonoBehaviour {
    public InventoryItemData itemData;

    public float speed = 6.0f;
    public float pickUpDist = 2.5f;
    public float despawnTime = 35.0f;

    void Awake() {
    }

    void Update() {
        despawnTime -= Time.deltaTime;
        if (despawnTime <= 0) { Destroy(gameObject); }

        float lootDist = Vector3.Distance(transform.position, Player.Instance.transform.position);

        transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, 
            speed * Time.deltaTime);

        if (lootDist > pickUpDist) { return; } // player too far, don't move anwyhere

        if (lootDist < 0.1f) { // player got close, add to inventory then destroy pickup
            var targetInventory = Player.Instance.GetComponent<Inventory>();
            if (!targetInventory) { Debug.Log("Could not find target inventory."); return; }

            if (targetInventory.InventorySystem.AddToInventory(itemData, 1)) {
                Destroy(gameObject);                
            }
        }
    }
}
