using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IInitializePotentialDragHandler, IDragHandler
{
    public GameObject collectible;
    public GameObject player;
    public GameObject canvas;

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        Instantiate(collectible, player.transform.position, Quaternion.identity);
        //collectible.transform.SetParent(canvas.transform, false);

        eventData.pointerDrag = collectible;
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
}
