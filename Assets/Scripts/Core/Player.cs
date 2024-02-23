using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDataPersistence {
    [Header("References")]
    public AudioSource audioSource;
    [SerializeField] private GameObject _graphic;
    private Rigidbody2D _rb;
    private Animator _animator;
    private Transform _rotTracker;

    [Header("Properties")]
    public float moveSpeed;
    public bool frozen = false;
    [HideInInspector] public string groundType = "grass";
    private Vector3 _origLocalScale;
    [SerializeField] public Vector2 direction;
    private float _curAngle;
    private float _dirDegrees;

    [Header("Sounds")]
    public AudioClip defaultWalkSound;
    public AudioClip grassSound;
    public AudioClip snowSound;
    public AudioClip sandSound;
    public AudioClip bayouSound;
    public AudioClip stepSound;

    private static Player instance;
    public static Player Instance {
        get {
            if (instance == null) instance = GameObject.FindObjectOfType<Player>();
            return instance;
        }
    }

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _rotTracker = GetComponentInChildren<Rotator>().transform;

        _origLocalScale = transform.localScale;

        SetGroundType();
    }

    public void OnMove(InputValue value) {
        direction = value.Get<Vector2>();
        Debug.Log("sex on the beach!");
    }

    void FixedUpdate() {
        if (!frozen) {
            _rb.velocity = direction * moveSpeed;

            // Handle rotational animations
            _dirDegrees = ((Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 270) % 360;
            _curAngle = _rotTracker.eulerAngles.z;
        
            // if (direction.x > 0.01f) { // Flip the player graphic's localScale
            //     _graphic.transform.localScale = new Vector3(_origLocalScale.x, transform.localScale.y, transform.localScale.z);
            // } else if (direction.x < 0.01f) {
            //     _graphic.transform.localScale = new Vector3(-_origLocalScale.x, transform.localScale.y, transform.localScale.z);
            // }

            _animator.SetFloat("direction", (_curAngle + 22.5f) % 360);
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
            _animator.SetFloat("direction", 0);
            _rb.velocity = Vector3.zero;
        }

        frozen = freeze;
    }

    public void PlayStepSound() {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(stepSound, Mathf.Abs(direction.x) / 10 * Mathf.Abs(direction.y) / 10);
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
