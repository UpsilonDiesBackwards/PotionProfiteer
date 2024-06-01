using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public List<ItemData> items = new List<ItemData>();
    public InventorySlot[] slots;

    private AudioSource source;
    public AudioClip pickupClip;

    public void AddItem(ItemData item) {
        if (items.Count < slots.Length) {
            items.Add(item);
            UpdateUI();

            Debug.Log("Added to inventory: " + item.name);
        } else {
            Debug.Log("Inventory full");
        }
    }

    public void RemoveItem(ItemData item) {
        items.Remove(item);
        Debug.Log("Removed from inventory: " + item.name);
    }

    void UpdateUI() {
        for (int i = 0; i < slots.Length; i++) {
            if (i < items.Count) {
                slots[i].AddItem(items[i]);
                PlayAudio(pickupClip);
            } else {
                slots[i].ClearSlot();
            }
        }
    }

    void PlayAudio(AudioClip clip) {
        if (!source) {
            source = gameObject.AddComponent<AudioSource>();
        }

        source.PlayOneShot(clip, 0.7f);
    }
}
