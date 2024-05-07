using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro.EditorUtilities;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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
    [SerializeField] private float _moveTimer = 0f;
    [SerializeField] private float _restTimer = 0f;

    private Vector2 _movementDirection = Vector2.right;

    void OnEnable() {
        StartCoroutine(Blink());
        StartCoroutine(Croak());
    }

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        Movement();
    }

    void Movement() {
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

    public void PlayWalkSound() {
        float pitch = Random.Range(minWalkPitch, maxWalkPitch);

        _audioSource.pitch = pitch;
        _audioSource.PlayOneShot(_walkSound, 0.75f);
        _audioSource.Stop();
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
}
