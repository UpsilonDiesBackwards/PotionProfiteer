using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class DragAndDrop : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler
{
    public bool isHeld = false;
    
    public GameObject itemImage;
    public GameObject tomato;
    public GameObject cauldron;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
       canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnMouseDrag()
    {
        transform.position = GetMousePos(); //setting the mouse to the world position
    }

    Vector3 GetMousePos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("something");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
       // Instantiate(tomato, Input.mousePosition, Quaternion.identity);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = GetMousePos();
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }


    public void WhereItsAt()
    {
        float proximity = 5f;

        Vector3 cauldronPos = cauldron.transform.position;
        cauldronPos.z = 0;
        Vector3 itemPos = Input.mousePosition;

        float distance = Vector3.Distance(cauldronPos, itemPos);

        if (distance < proximity)
        {
            Debug.Log("ITEM PUT IN CAULDRON");
        }
        else
        {
           // DestroyImmediate(tomato, true); //WILL NEED TO BE PUT BACK INTO THE INVENTORY SLOT
        }

    }
}

