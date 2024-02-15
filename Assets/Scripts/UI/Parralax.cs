using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform target;
    [SerializeField] float parralaxFactor;
    Vector2 startPosition;

    Vector2 travel => (Vector2)cam.transform.position - startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        transform.position = startPosition + travel * parralaxFactor;
    }
}
