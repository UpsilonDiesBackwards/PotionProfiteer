using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class FrogController : MonoBehaviour {
    [Header("Movement")]
    public float speed = 1f;

    public float maxMoveDuration = 3f;
    public float minRestDuration = 3f;
    public float maxRestDuration = 12f;

    [Header("Probabilities")]
    public float croakProbability = 0.2f;
    public float blinkProbability = 0.1f;
    public float turnAroundProbability = 0.3f;

    [Header("Audio")]
    public float minWalkPitch = 0.7f;
    public float maxWalkPitch = 1.3f;

    public float minCroakPitch = 0.7f;
    public float maxCroakPitch = 1.3f;

    private Rigidbody2D _rb;
    private AudioSource _audioSource;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private AudioClip _walkSound;
    [SerializeField] private AudioClip _croakSound;

    [SerializeField] private bool _isMoving = false;
    [SerializeField] private bool _isBlinking = false;
    [SerializeField] private bool _isCroaking = false;
    [SerializeField] private bool _isSmiling = false;
    private bool _isInRange = false;
    [SerializeField] private float _moveTimer = 0f;
    [SerializeField] private float _restTimer = 0f;

    private Vector2 _movementDirection = Vector2.right;

    void OnEnable() { StartCoroutine(InitialiseBroga()); }

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    IEnumerator InitialiseBroga() {
        yield return new WaitForSeconds(Random.Range(0f, 2.25f));

        StartCoroutine(Blink());
        StartCoroutine(Croak());
    }

    void Update() {
        Movement();

        if (_isInRange && Input.GetKeyDown(KeyCode.E) && !_isSmiling) {
            StartCoroutine(Smile());
        }
    }

    void Movement() {
        if (!_isSmiling) {
            if (_isMoving) {
                _rb.velocity = _movementDirection * speed;

                _moveTimer += Time.deltaTime;
                if (_moveTimer >= maxMoveDuration) {
                    _isMoving = false;
                    _animator.SetBool("isMoving", false);
                    _restTimer = 0f;

                }
            } else {
                _rb.velocity = Vector2.zero;

                _restTimer += Time.deltaTime;
                if (_restTimer >= Random.Range(minRestDuration, maxRestDuration)) {
                    _isMoving = true;
                    _animator.SetBool("isMoving", true);
                    _moveTimer = 0f;
            
                    if (Random.value < turnAroundProbability) {
                        float randomDirection = Random.Range(0f, 1f);
                        if (randomDirection < 0.5f) {
                            _movementDirection = new Vector2(Mathf.Sign(Random.Range(-1f, 1f)), 0f);
                            _spriteRenderer.flipX = _movementDirection.x < 0f;
                        } else {
                            _movementDirection = new Vector2(0f, Mathf.Sign(Random.Range(-1f, 1f)));
                        }
                    }
                }
            }
        }
    }

    public void PlayWalkSound() {
        float pitch = Random.Range(minWalkPitch, maxWalkPitch);

        _audioSource.pitch = pitch;
        _audioSource.PlayOneShot(_walkSound, 0.75f);
    }

    IEnumerator Blink() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(6.0f, 16.0f));

            _isBlinking = true;
            _animator.SetBool("isBlinking", true);

            yield return new WaitForSeconds(0.5f);

            _animator.SetBool("isBlinking", false);

            _isBlinking = false;
        }
    }

    IEnumerator Croak() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(8.0f, 26.0f));

            _isCroaking = true;
            _animator.SetBool("isCroaking", true);

            float pitch = Random.Range(minCroakPitch, maxCroakPitch);
            _audioSource.pitch = pitch;
            _audioSource.PlayOneShot(_croakSound, 0.75f);

            yield return new WaitForSeconds(_croakSound.length);

            _animator.SetBool("isCroaking", false);
            _isCroaking = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player") && !_isSmiling) {
            // Ensure that the trigger is not activated while the frog is already smiling
            _isInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            // Reset the trigger flag when the player exits the frog's range
            _isInRange = false;
        }
    }

    IEnumerator Smile() {
        _isSmiling = true;
        _animator.SetBool("isSmiling", true);
        _isMoving = false;

        // Freeze player's movement and trigger petting animation
        Player.Instance.Freeze(true);
        Player.Instance.GetComponent<Animator>().SetBool("isPetting", true);

        yield return new WaitForSeconds(1.0f);

        _animator.SetBool("isSmiling", false);
        _isSmiling = false;

        Player.Instance.GetComponent<Animator>().SetBool("isPetting", false);

        yield return new WaitForSeconds(0.5f);

        // Unfreeze player's movement and reset petting animation
        Player.Instance.Freeze(false);

        _isMoving = true;
    }
}
