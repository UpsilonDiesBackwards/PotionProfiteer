using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDataPersistence {
    [Header("References")]
    public AudioSource audioSource;
    public Joystick joystick;
    public CameraEffects cameraEffects;
   
    [SerializeField] private GameObject _graphic;
    private Rigidbody2D _rb;
    public Animator animator;
    private Transform _rotTracker;

    [Header("Properties")]
    public float moveSpeed;
    public bool frozen = false;
    private bool _isMoving = false;
    private Vector2 _lastMove;
    [HideInInspector] public string groundType = "grass";
    private Vector3 _origLocalScale;
    public Vector2 direction;
    private float _curAngle;
    private float _dirDegrees;

    [Header("Sounds")]
    public AudioClip defaultWalkSound;
    public AudioClip grassSound;
    public AudioClip snowSound;
    public AudioClip sandSound;
    public AudioClip bayouSound;
    public AudioClip stepSound;

    [Header("Inventory")]
    public int spondulixs;

    private static Player instance;
    public static Player Instance {
        get {
            if (instance == null) instance = GameObject.FindObjectOfType<Player>();
            return instance;
        }
    }

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _rotTracker = GetComponentInChildren<Rotator>().transform;

        _origLocalScale = transform.localScale;

        SetGroundType();

    }

    public void OnMove(InputValue value) {
        direction = value.Get<Vector2>();
    }

    void FixedUpdate() {
        if (!frozen) {
            _isMoving = false;

            float input_hor = Input.GetAxisRaw("Horizontal");
            float input_ver = Input.GetAxisRaw("Vertical");

            Vector2 movement = new Vector2(input_hor, input_ver).normalized;
            movement *= moveSpeed;
            _rb.velocity = movement;

            if (Input.touchSupported == true)
            {
               // -_rb.velocity = joystick.Horizontal * moveSpeed ~~ getting a bunch of error codes with this line it works in PlayerTouchMovement idk why not here
            }

            // Handle rotational animations
            _dirDegrees = ((Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 270) % 360;
            _curAngle = _rotTracker.eulerAngles.z;
        
            if (input_hor > 0.5f || input_hor < -0.5f) {
                _isMoving = true;
                _lastMove = new Vector2(input_hor, 0f);
            }

            if (input_ver > 0.5f || input_ver < -0.5f)
            {
                _isMoving = true;
                _lastMove = new Vector2(0f, input_ver);
            }

            if (direction.x > 0.01f || _lastMove.x > 0.01) { // Flip the player graphic's localScale
                _graphic.transform.localScale = new Vector3(-_origLocalScale.x, transform.localScale.y, transform.localScale.z);
            } else if (direction.x < 0.01f) {
                _graphic.transform.localScale = new Vector3(_origLocalScale.x, transform.localScale.y, transform.localScale.z);
            }

            // _animator.SetFloat("direction", (_curAngle + 22.5f) % 360);
            animator.SetFloat("moveX", input_hor);
            animator.SetFloat("moveY", input_ver);
            animator.SetFloat("lastMoveX", _lastMove.x);
            animator.SetFloat("lastMoveY", _lastMove.y);

            animator.SetBool("isMoving", _isMoving);
        }
    }

    public void SetGroundType() {
        switch (groundType) {
            case "grass":
                stepSound = grassSound;
                break;
            default:
                stepSound = defaultWalkSound;
                break; 
        }
    }

    public void Freeze(bool freeze) {
        if (freeze) {
            _rb.velocity = Vector3.zero;
        }
        frozen = freeze;
    }

    public void PlayStepSound() {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        // audioSource.PlayOneShot(stepSound, Mathf.Abs(direction.x) / 10 * Mathf.Abs(direction.y) / 10);
    }

    public void Hide(bool hide) {
        Freeze(hide);
        gameObject.SetActive(!hide);
    }

    public void LoadData(GameData data) {
        transform.position = data.playerPos;
    }

    public void SaveData(ref GameData data) {
        data.playerPos = transform.position;
    }
}
