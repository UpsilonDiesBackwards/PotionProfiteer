using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WagonInventory : InventoryHolder, IInteractable
{
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

    public void Interact(Interactor interactor, out bool InteractSuccessful)
    {
        OnDynamicInventoryDisplayRequested?.Invoke(inventorySystem);
        InteractSuccessful = true;
    }
    public void EndInteraction()
    {
        
    }

    
}

