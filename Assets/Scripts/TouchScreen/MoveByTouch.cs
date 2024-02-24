using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByTouch : MonoBehaviour
{
    public GameObject Player;
    private float moveSpeed;
    // Update is called once per frame
    void Update()
    {
        SingleTouch();
    }

    void SingleTouch() //moves whatever has this script around the screen to wherever the finger touched the screen, can drag with this too
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            transform.position = touchPosition;

            // IN THEORY this line should work but its not transform.position = Vector3.MoveTowards(transform.position, touchPosition, moveSpeed);
           
        }
    }

    void MultiTouch() //draws a red line from the center of the screen to wherever the multiple fingers are at, doesn't work on PC need to test on phone
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 multiTouchPos = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            Debug.DrawLine(Vector3.zero, multiTouchPos, Color.red);
        }
    }

    void TouchTowardsMove()
    {
        
    }
}
