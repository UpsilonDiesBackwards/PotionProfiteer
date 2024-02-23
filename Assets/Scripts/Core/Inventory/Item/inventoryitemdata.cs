using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// scriptable object, defines what an item is in the game
/// 
/// </summary>
[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    public Sprite Icon;
    public int ID;
    public string DisplayName;
    public int MaxStackSize;
    public string Description;
    
    
}
