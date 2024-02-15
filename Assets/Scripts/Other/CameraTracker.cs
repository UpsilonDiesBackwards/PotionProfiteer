using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    public Transform robot;
    Vector3 cursor;
    Vector3 target;
    GameObject cameraLocation;
    float distance;
    float mod;

    void Start()
    {
        cameraLocation = GameObject.Find("Camera Location 0");
    }

    void FixedUpdate()
    {
        cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        distance = Vector3.Magnitude(cameraLocation.transform.position - robot.position);

        if (distance < 9)
        { 
            target = cameraLocation.transform.position;
            mod = distance / 2;
        }
        else
        {
            target = cursor;
            mod = 7;
        }

        transform.position = robot.position + ((target - robot.position) / mod);
    }
}
