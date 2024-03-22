using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CauldronController : MonoBehaviour {
    private Dictionary<InventoryItemData.Category, float> values = new Dictionary<InventoryItemData.Category, float>();

    public void AddIngredient(InventoryItemData iData, float iWeight) {
        UpdateValues(iData.category, iWeight); // iWeight is how influential that item is. Base = 0.5, special = 1
    }

    private void UpdateValues(InventoryItemData.Category category, float value) {
        if (values.ContainsKey(category)) {
            values[category] += value;
        } else {
            values.Add(category, value);
        }
    }

    public void BrewPotion() {
        Debug.Log("Potion Brewing");
    }
}
