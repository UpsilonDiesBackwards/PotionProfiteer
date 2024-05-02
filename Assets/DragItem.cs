using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IInitializePotentialDragHandler, IDragHandler, IPointerEnterHandler
{
    public GameObject collectible;
    public GameObject player;

    

    public Image uiPic;

    Ray ray;
    RaycastHit hit;

    void Awake()
    {
        uiPic = GetComponent<Image>();
        
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        if (uiPic.sprite != null)
        {
            Instantiate(collectible, player.transform.position, Quaternion.identity);
            // RemoveFromInventory(inventorySlot inv);
            
            //collectible.transform.SetParent(canvas.transform, false);

            eventData.pointerDrag = collectible;
        }   
    }

    public void RemoveFromInventory(InventorySlot remove)
    {
        remove.RemoveFromStack(1);
    }

    public void OnDrag(PointerEventData eventData)
    {
        collectible.transform.position = GetMousePos();
    }

    Vector3 GetMousePos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.name);
        }
    }
}
