using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampWater : MonoBehaviour {
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private float _animationSpeedMultiplier = 0.5f;

    [SerializeField] private float _playerSpeedBuffer;

    void OnEnable() {
        _playerSpeedBuffer = Player.Instance.moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            Player.Instance.moveSpeed = _speed;
            ChangeAnimationSpeed(_animationSpeedMultiplier);
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            Player.Instance.moveSpeed = _playerSpeedBuffer;
            ResetAnimationSpeed();
        }
    }

    void ChangeAnimationSpeed(float mult) {
        if (Player.Instance.animator) {
            Player.Instance.animator.speed *= mult;
        }
    }

    private void ResetAnimationSpeed() {
        if (Player.Instance.animator != null) {
            Player.Instance.animator.speed = 1f;
        }
    }
}
