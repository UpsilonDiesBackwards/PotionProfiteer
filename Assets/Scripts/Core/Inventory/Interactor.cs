using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    public Transform InteractionPoint;
    public float InteractionPointRadius;
    public bool IsInteracting {  get; private set; }
    public LayerMask InteractionLayer;


    private void Update()
    {
        var colliders = Physics2D.OverlapCircle(InteractionPoint.position, InteractionPointRadius, InteractionLayer);
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            // for (int i = 0; colliders.Length; i++)

            // {
            var interactable = colliders.GetComponent<IInteractable>();

            if (interactable != null) StartInteraction(interactable);
            //   }
        }

    }
    void StartInteraction(IInteractable interactable)
    {
        interactable.Interact(this, out bool interactSuccessful);
    }

    void EndInteraction()
    {
        IsInteracting = false;
    }

}
