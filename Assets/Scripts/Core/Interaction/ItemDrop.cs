using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour {
    public GameObject item;

    public void DropItem() {
        Instantiate(item, transform.position, Quaternion.identity);
    }
}
