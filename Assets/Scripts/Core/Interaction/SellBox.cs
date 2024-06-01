using UnityEngine;
using UnityEngine.EventSystems;

public class SellBoxDropHandler : MonoBehaviour, IDropHandler {
    private AudioSource source;
    public AudioClip plopClip;

    public void OnDrop(PointerEventData eventData) {

        if (DragHandler.itemBeingDragged != null) {
            InventorySlot slot = DragHandler.itemBeingDragged.GetComponentInParent<InventorySlot>();
            if (slot != null && slot.item != null) {
                PlayAudio(plopClip);

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

    void PlayAudio(AudioClip clip) {
        if (!source) {
            source = gameObject.AddComponent<AudioSource>();
        }

        source.PlayOneShot(clip, 0.7f);
    }
}
