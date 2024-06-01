using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    public ItemData item;
    public Image icon;

    public void AddItem(ItemData newItem) {
        item = newItem;
        icon.sprite = newItem.icon;
        icon.enabled = true;
    }

    public void ClearSlot() {
        // Debug.Log("Clearing Slot");
        Player.Instance.GetComponent<Inventory>().RemoveItem(item);
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}
