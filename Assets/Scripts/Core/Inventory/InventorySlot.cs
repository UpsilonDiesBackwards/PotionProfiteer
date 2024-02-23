using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private InventoryItemData itemData; // reference to data
    [SerializeField] private int stackSize; // current stack size = how many of the data we have

    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;

    public InventorySlot(InventoryItemData source, int amount) // constructor to make an occupied inv slot
    {
        itemData = source;
        stackSize = amount;
    }

    public InventorySlot() // make an empty inv slot
    {
        ClearSlot();
    }

    public void ClearSlot() // clears the slot
    {
        itemData = null;
        stackSize = -1;
    }

    public void AssignItem(InventorySlot invSlot) // assigns item to a slot
    {
        if (itemData == invSlot.itemData) AddToStack(invSlot.stackSize); // does the slot contain same item, add to slot if it is
        else // overwrite slot with inv slot we pass in
        {
            itemData = invSlot.itemData;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }

    public void UpdateInventorySlot(InventoryItemData data, int amount) // updates slot directly 
    {
        itemData = data;
        stackSize = amount;
    }
    public bool EnoughRoomLeftInStack(int amountToAdd, out int amountRemaining) // checks if there is enough room in stack for amount we are adding
    {
        amountRemaining = ItemData.MaxStackSize - stackSize;
        return EnoughRoomLeftInStack(amountToAdd);
    }

    public bool EnoughRoomLeftInStack(int amountToAdd)
    {
        if(stackSize + amountToAdd <= itemData.MaxStackSize) return true;
        else return false;
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;

    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }

    public bool SplitStack(out InventorySlot splitStack)
    {
        if (stackSize <= 1)
        {   
            splitStack = null;
            return false;
        }
        
        int halfStack = Mathf.RoundToInt(stackSize / 2);
        RemoveFromStack(halfStack);

        splitStack = new InventorySlot(itemData, halfStack);
        return true;

            
    }
    


}
