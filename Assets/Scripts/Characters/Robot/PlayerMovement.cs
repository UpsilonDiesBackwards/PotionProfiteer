using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public Animator animator;
    
    public Transform rotationTracker;

    public Vector2 direction;
    public float cursorAngle;
    public float directionDegrees;

    public float moveSpeed = 4f;
    float sprint;
    public float sprintModifierF;
    public float sprintModifierFD;

    public bool sprintBool = false;
    public bool walkBool = false;

    void FixedUpdate()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        directionDegrees = ((Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 270) % 360;

        GetSprintSpeed();

        rb.velocity = moveSpeed * sprint * direction;

        cursorAngle = rotationTracker.eulerAngles.z;

        animator.SetFloat("direction", (cursorAngle + 22.5f) % 360);

        if (Mathf.Abs(rb.velocity.x) > 0 || Mathf.Abs(rb.velocity.y) > 0)
        {
            walkBool = true;
        }
        else
        {
            walkBool = false;
        }

        animator.SetBool("walk", walkBool);
    }

    void GetSprintSpeed()
    {
        float diff = Mathf.Abs(directionDegrees - cursorAngle);

        diff = Mathf.Min(diff, Mathf.Abs(diff - 360));

        if (diff < 22.5 && walkBool == true)
        {
            sprint = sprintModifierF;
            sprintBool = true;
        }
        else if (diff < 67.5 && walkBool == true)
        {
            sprint = sprintModifierFD;
            sprintBool = false;
        }
        else
        {
            sprint = 1;
            sprintBool = false;
        }
    }
}
