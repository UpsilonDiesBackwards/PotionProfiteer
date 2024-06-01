using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public ItemData itemData;

    public float launchForce = 5f;
    public float moveSpeed = 2f;
    public float detectionRadius = 2f;

    public Inventory inventory;

    private Rigidbody2D _rb;
    private bool _grounded = false;
    private bool _moveToPlayer = false;

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        inventory = Player.Instance.gameObject.GetComponent<Inventory>();

        GetComponent<SpriteRenderer>().sprite = itemData.icon;

        // Launch();
    }

    void Update() {
        if (_moveToPlayer) {
            FollowPlayer();
        } else {
            FindPlayer();
        }
    }

    void Launch() {
        _rb.AddForce(Vector2.up * launchForce, ForceMode2D.Impulse);
    }

    void FindPlayer() {
        Collider2D player = Physics2D.OverlapCircle(transform.position, detectionRadius, Player.Instance.gameObject.layer);
        if (player != null) { FollowPlayer(); }
    }

    void FollowPlayer() {
        GameObject player = Player.Instance.gameObject;
        if (player != null) {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            _rb.velocity = direction * moveSpeed;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            _grounded = true;
            // _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Abs(_rb.velocity.y)); // Bounce effect
        } else if (collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
            inventory.AddItem(itemData);

            Debug.Log("askompdsakjdsa");
        }
    }
}
