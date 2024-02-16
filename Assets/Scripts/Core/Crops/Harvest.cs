using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor.TerrainTools;
using UnityEngine;

public class Harvest : MonoBehaviour
{
    private PlayerMovement _moveScript;
    private Rigidbody2D _rb;

    public float offsetDist = -1.0f;
    public float pickUpRadius = 1.5f;

    void Awake() {
        _moveScript = GetComponent<PlayerMovement>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E) && CropGrowth.fullyGrown == true) {
            ChopDown();
        }
    }

    void ChopDown() {
        Vector2 pos = _rb.position + _moveScript.direction * offsetDist;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, pickUpRadius);

        foreach (Collider2D c in colliders) {
            Tool hit = c.GetComponent<Tool>();

            if (hit != null) {
                hit.Hit();
                break;
            }
        }
    }
}
