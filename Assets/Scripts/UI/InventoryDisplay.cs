using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;
    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary; // pair up UI slots with the system slots.
    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

    public virtual void Start()
    {

    }

    public abstract void AssignSlot(InventorySystem invToDisplay); // implemented in child classes

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach (var slot in slotDictionary)
        {
            if (slot.Value == updatedSlot) //slot value - the "under the hood" inv slot
            {
                slot.Key.UpdateUISlot(updatedSlot); // slot key - UI representation of the value
            }
        }
    }

    public void SlotClicked(InventorySlot_UI clickedUISlot)
    {
        mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
        Instantiate(clickedUISlot.AssignedInventorySlot.ItemData.go, Player.Instance.transform.position, Quaternion.identity);

        clickedUISlot.AssignedInventorySlot.RemoveFromStack(1);
        clickedUISlot.ClearSlot();
        
        // // clicked slot has item - mouse doesn't have item - pick up item
        // bool isShiftPressed = Mouse.current.rightButton.isPressed;
        // if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData == null)
        // {
        //     Debug.Log(clickedUISlot.AssignedInventorySlot.ItemData.go);


        //     // is player holding shift key, split the stack
        //     if (isShiftPressed && clickedUISlot.AssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot)) // split stack
        //     {
        //         mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
        //         clickedUISlot.UpdateUISlot();
        //         return;
        //     }
        //     else // pick up item in clicked slot
        //     {
        //         mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
        //         Instantiate(clickedUISlot.AssignedInventorySlot.ItemData.go, Player.Instance.transform.position, Quaternion.identity);

        //         clickedUISlot.ClearSlot();
        //         return;
        //     }
            
        // }
        
        // // clicked slot doesnt have item - mouse has item - place mouse item in empty slot
        // if (clickedUISlot.AssignedInventorySlot.ItemData == null && mouseInventoryItem.AssignedInventorySlot.ItemData != null)
        // {
        //     clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
        //     clickedUISlot.UpdateUISlot();

        //     mouseInventoryItem.ClearSlot();
        // }

        // // both slots have an item - decide what to do

        // if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData != null)
        // {
        //     bool isSameItem = clickedUISlot.AssignedInventorySlot.ItemData == mouseInventoryItem.AssignedInventorySlot.ItemData;
        //     // are both items the same, if so combine them
        //     if (isSameItem && clickedUISlot.AssignedInventorySlot.EnoughRoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize))
        //     {
        //         clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
        //         clickedUISlot.UpdateUISlot();

        //         mouseInventoryItem.ClearSlot();
        //     }
        //     else if (isSameItem && !clickedUISlot.AssignedInventorySlot.EnoughRoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize, out int leftInStack))
        //     {
        //         if (leftInStack < 1) SwapSlots(clickedUISlot); // stack is full so swap items
        //         else // slot isnt at max so take whats needed from mouse inv
        //         {
        //             int remainingOnMouse = mouseInventoryItem.AssignedInventorySlot.StackSize - leftInStack; //calculate what would be remaining on mouse

        //             clickedUISlot.AssignedInventorySlot.AddToStack(leftInStack); //fill up the UI stack
        //             clickedUISlot.UpdateUISlot();

        //             var newItem = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.ItemData, remainingOnMouse);
        //             mouseInventoryItem.ClearSlot();
        //             mouseInventoryItem.UpdateMouseSlot(newItem); // make mouse inv slot have new amount
        //             return;
        //         }
        //     }
            
        //     else if (! isSameItem)
        //     {
        //         SwapSlots(clickedUISlot);
        //         return;
        //     }    

        // }      
                        
    }

    private void SwapSlots(InventorySlot_UI clickedUISlot)
    {
        var clonedSlot = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.ItemData, mouseInventoryItem.AssignedInventorySlot.StackSize);
        mouseInventoryItem.ClearSlot();

        mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);

        clickedUISlot.ClearSlot();
        clickedUISlot.AssignedInventorySlot.AssignItem(clonedSlot);
        clickedUISlot.UpdateUISlot();
    }




}
