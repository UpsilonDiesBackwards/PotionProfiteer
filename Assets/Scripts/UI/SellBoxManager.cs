using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellBoxManager : MonoBehaviour {
    [SerializeField] private GameObject _sellingInterface;

    private bool isInRange = false;

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
            _sellingInterface.SetActive(true);
            Player.Instance.Freeze(true);
        }
    }

    public void CloseMenu() {
        _sellingInterface.SetActive(false);
        Player.Instance.Freeze(false);
    }

}
