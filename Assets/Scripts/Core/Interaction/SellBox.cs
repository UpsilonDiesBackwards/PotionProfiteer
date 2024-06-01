using UnityEngine;
using UnityEngine.EventSystems;

public class SellBoxDropHandler : MonoBehaviour, IDropHandler {
    public void OnDrop(PointerEventData eventData) {
        Debug.Log("Item dropped on sell box!");

        if (DragHandler.itemBeingDragged != null) {
            InventorySlot slot = DragHandler.itemBeingDragged.GetComponentInParent<InventorySlot>();
            if (slot != null && slot.item != null) {
                Debug.Log("Item data found: " + slot.item.itemName);

                // Sell the item
                Player.Instance.spondulixs += slot.item.value;
                Debug.Log("Sold item: " + slot.item.itemName + " for " + slot.item.value + " spondulixs");

                // Clear the slot
                slot.ClearSlot();
                Debug.Log("Item removed from inventory: " + slot.item.itemName);
            } else {
                Debug.Log("No item data found in slot.");
            }
        } else {
            Debug.Log("No item being dragged.");
        }
    }
}
