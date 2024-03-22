using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New/Item")]
public class InventoryItemData : ScriptableObject
{
    public Sprite Icon;
    public int ID;
    public string DisplayName;
    public int MaxStackSize;
    public string Description;
    
    public enum Category {
        NOTAPLANT,
        
        Spicy,
        SpecialSpicy,

        Sour,
        SpecialSour,

        Sweet,
        SpecialSweet,

        Salty,
        SpecialSalty, // AYO?!!?! Keep this game PG-13 i stg.

        Umami,
        SpecialUmami,
    }
    public Category category;
}
