using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragableObject : MonoBehaviour
{
    public GameObject item;
    public RectTransform _rectTransform;
    private int moveSpeed = 2;
    private void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        if (Input.GetMouseButton(0))
        {
            Debug.Log(Input.mousePosition);
            Debug.Log("TEST");
            _rectTransform.position = Vector3.MoveTowards(transform.position, mousePos, moveSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cauldron")
        {
            Destroy(collision.gameObject);
        }
    }

}
