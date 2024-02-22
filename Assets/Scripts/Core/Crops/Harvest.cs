using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEditor.TerrainTools;
using UnityEngine;

public class Harvest : MonoBehaviour {
    private Rigidbody2D _rb;

    public float offsetDist = -1.0f;
    public float pickUpRadius = 1.5f;

    private bool isInRange = false;
    [SerializeField] private Collider2D currentCropCollider;

    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (isInRange && currentCropCollider != null && currentCropCollider.GetComponent<CropGrowth>().fullyGrown) {
            if (Input.GetKeyDown(KeyCode.E)) {
                currentCropCollider.GetComponent<CropGrowth>().OnHarvested();
                ChopDown();
                isInRange = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.GetComponent<CropGrowth>() != null && col.GetComponent<CropGrowth>().fullyGrown) {
            currentCropCollider = col;
            isInRange = true;
        }
    }

    void ChopDown() {
        Debug.Log("Harvey");
        Vector2 pos = _rb.position + Player.Instance.GetComponent<PlayerMovement>().direction * offsetDist;
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
