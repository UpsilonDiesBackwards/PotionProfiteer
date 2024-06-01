using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMenu : MonoBehaviour {
    [Header("References")]
    [SerializeField] private GameObject _noteWindow;
    [SerializeField] private GameObject _player;

    private bool isInRange = false;

    void Awake() { _player = Player.Instance.gameObject; }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            isInRange = true;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            isInRange = false;
        }    
    }

    void Update() {
        if (isInRange && Input.GetKey(KeyCode.E)) {
            _noteWindow.SetActive(true);
            Player.Instance.Freeze(true);
        }
    }

    public void CloseMenu() {
        _noteWindow.SetActive(false);
        Player.Instance.Freeze(false);
    }
}
