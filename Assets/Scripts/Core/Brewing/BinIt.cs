using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinIt : MonoBehaviour
{
    public LayerMask emptyLayer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Empty") || other.gameObject.layer == emptyLayer)
        {
            Destroy(other.gameObject);
        }
    }
}
