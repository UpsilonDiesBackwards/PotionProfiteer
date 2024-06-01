using UnityEngine;
using UnityEngine.EventSystems;

public class CauldronDropHandler : MonoBehaviour, IDropHandler {
    public BrewingManager brewingManager;

    public void OnDrop(PointerEventData eventData) {
        Debug.Log("Item dropped on cauldron!");
        
        if (DragHandler.itemBeingDragged != null) {
            InventorySlot slot = DragHandler.itemBeingDragged.GetComponentInParent<InventorySlot>();
            if (slot != null && slot.item != null) {
                Debug.Log("Item data found: " + slot.item.itemName);
                
                // Add the item to the cauldron
                brewingManager.AddIngredient(slot.item);
                slot.ClearSlot();
                Debug.Log("Item dropped into cauldron: " + slot.item.itemName);
            } else {
                Debug.Log("No item data found in slot.");
            }
        } else {
            Debug.Log("No item being dragged.");
        }
    }
}
