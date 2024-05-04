using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellBox : MonoBehaviour {
    public LayerMask resourceLayer;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Resource") || other.gameObject.layer == resourceLayer) {
            Player.Instance.spondulixs += other.gameObject.GetComponent<DragableObject>().data.value;

            Destroy(other.gameObject);
        }
    }
}
