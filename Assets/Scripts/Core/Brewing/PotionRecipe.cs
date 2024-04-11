using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New/Potion Recipe")]
public class PotionRecipe : ScriptableObject {
    public string name;

    public InventoryItemData[] ingredients;

    public GameObject potion;
}
