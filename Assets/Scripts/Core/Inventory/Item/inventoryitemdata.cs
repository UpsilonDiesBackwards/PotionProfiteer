using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New/Inventory/Item")]
public class InventoryItemData : ScriptableObject
{
    public Sprite Icon;
    public int ID;
    public string DisplayName;
    public int MaxStackSize;
    public string Description;
    
    
}
