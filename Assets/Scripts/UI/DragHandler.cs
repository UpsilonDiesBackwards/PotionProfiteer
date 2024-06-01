using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public static GameObject itemBeingDragged;
    public CanvasGroup canvasGroup;
    private Vector3 _startPos;
    private Transform _startParent;

    public void OnBeginDrag(PointerEventData eventData) {
        itemBeingDragged = gameObject;
        _startPos = transform.position;
        _startParent = transform.parent;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventDta) {
        itemBeingDragged = null;
        canvasGroup.blocksRaycasts = true;
        if (transform.parent == _startParent) {
            transform.position = _startPos;
        }     
    }


}
