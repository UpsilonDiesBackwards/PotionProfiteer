using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    Vector3 mousePosition;
    Vector2 rotationVector;
    float angle;
    
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        rotationVector = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(rotationVector.y, rotationVector.x) * Mathf.Rad2Deg - 90 % 360);
    }
}
