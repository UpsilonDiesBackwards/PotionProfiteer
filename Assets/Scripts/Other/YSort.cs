using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSort : MonoBehaviour
{
    void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sortingOrder = -(int) (transform.position.y * 100);
    }
    void Update()
    {
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        spriteRenderer.sortingOrder = -(int)(transform.position.y * 100);
    }
}
