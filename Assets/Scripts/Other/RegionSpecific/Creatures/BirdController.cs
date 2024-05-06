using System.Collections;
using UnityEngine;

public class BirdController : MonoBehaviour {
    public float flightSpeed = 5f;
    public float detectionRadius = 5f;
    public float flyAwayDistance = 10f;

    [SerializeField] private bool playerInRange = false;
    private Vector3 originalPosition;
    private float direction;

    private Animator _animator;

    void Start() {
        _animator = GetComponent<Animator>();

        originalPosition = transform.position;
        direction = Mathf.Sign(-transform.localScale.x);

        StartCoroutine(RandomlyFlipDirection());
    }

    void Update() {
        if (playerInRange) {
            transform.position += Vector3.right * direction * flightSpeed * Time.deltaTime;

            _animator.SetBool("isFlying", true);
            float distanceToOriginal = Vector3.Distance(transform.position, originalPosition);
            if (distanceToOriginal > flyAwayDistance) {
                playerInRange = false;
            }

            // Flip the sprite's orientation if the direction changes
            if (direction > 0 && transform.localScale.x < 0 || direction < 0 && transform.localScale.x > 0) {
                Vector3 newScale = transform.localScale;
                newScale.x *= -1f;
                transform.localScale = newScale;
            }
        } else {
            _animator.SetBool("isFlying", false);
        }
    }

    IEnumerator RandomlyFlipDirection() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(5f, 20f));
            
            if (!playerInRange) {
                direction *= -1f;
                Vector3 newScale = transform.localScale;
                newScale.x *= -1f;
                transform.localScale = newScale;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player") && playerInRange) {
            StartCoroutine(DelayExit());
        }
    }

    IEnumerator DelayExit() {
    yield return new WaitForSeconds(0.5f);
        if (!playerInRange) {
            playerInRange = false;
        } else {
            playerInRange = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerInRange) {
            playerInRange = false;
        }
    }
}
