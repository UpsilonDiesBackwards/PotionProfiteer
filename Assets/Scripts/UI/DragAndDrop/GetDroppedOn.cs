using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GetDroppedOn : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("item dropped on me");

        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.position = transform.position; //item position becomes the position of the cauldron
        }
    }
}
