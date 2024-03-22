using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchMovement : MonoBehaviour
{
    public Joystick joystick;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    float joystickSensitivity = 0.2f;

    // Update is called once per frame
    void Update()
    {
        if (joystick.Horizontal >= joystickSensitivity)
        {
            horizontalMove = runSpeed;
        }
        else if (joystick.Horizontal <= -joystickSensitivity)
        {
            horizontalMove = -runSpeed;
        }
        else
        {
            horizontalMove = 0;
        }
    }
}
