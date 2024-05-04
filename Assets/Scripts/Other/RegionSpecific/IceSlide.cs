using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class IceSlide : MonoBehaviour {
    public float force = 10f;
    public float idleTimeThreshold = 1f;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private bool _isSliding = false;
    [SerializeField] private Vector2 _slideDirection;

    [SerializeField] private Vector2 lastPosition;
    [SerializeField] private float _idleTimer = 0f;

    private void Start() {
        _rb = Player.Instance.gameObject.GetComponent<Rigidbody2D>();

        lastPosition = _rb.position;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            _slideDirection = _rb.velocity.normalized;

            _isSliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            _isSliding = false;
        }
    }

    private void Slide() {
        _rb.AddForce(_slideDirection * force, ForceMode2D.Force);
    }

    private void Update() {
        if (_isSliding) {
            _idleTimer += Time.deltaTime;

            if (_idleTimer >= idleTimeThreshold) { // Fail safe - If player is sliding buts not moving (i.e stuck), stop sliding
                if (_rb.position == lastPosition) {
                    _isSliding = false;
                    _idleTimer = 0f;
                } else {
                    _idleTimer = 0f;
                    lastPosition = _rb.position;
                }
            }

            Slide();
        }

        Player.Instance.Freeze(_isSliding);
    }
}
