using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampWater : MonoBehaviour {
    [SerializeField] private float _speed = 2.0f;

    [SerializeField] private float _playerSpeedBuffer;

    void OnEnable() {
        _playerSpeedBuffer = Player.Instance.moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            Player.Instance.moveSpeed = _speed;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            Player.Instance.moveSpeed = _playerSpeedBuffer;
        }
    }
}
