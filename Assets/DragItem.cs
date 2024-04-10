using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IInitializePotentialDragHandler, IDragHandler, IPointerEnterHandler
{
    public GameObject collectible;
    public GameObject player;

    Ray ray;
    RaycastHit hit;

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.name);
        }
    }
}
