using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellBox : MonoBehaviour
{
    public Inventory inventory;
    public LayerMask resourceLayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Resource") || other.gameObject.layer == resourceLayer)
        {
            // Debug.Log("ITs On Trigger");

            Player.Instance.spondulixs += other.gameObject.GetComponent<DragableObject>().data.value;

            //other.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(other.gameObject);
        }
    }
}
