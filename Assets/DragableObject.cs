using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragableObject : MonoBehaviour
{
    public InventoryItemData data;

    public GameObject item;
    public RectTransform _rectTransform;
    private int moveSpeed = 2;
    public bool isDragging = false;
    private void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        if (Input.GetMouseButton(0))
        {
            isDragging = true;
            //Debug.Log(Input.mousePosition);
            //Debug.Log("TEST");
            _rectTransform.position = Vector3.MoveTowards(transform.position, mousePos, moveSpeed);
        }
        isDragging = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Cauldron") && !isDragging)
        {
            Destroy(gameObject);
        }
    }
}
