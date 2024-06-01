using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPrompt : MonoBehaviour {
    [SerializeField] private GameObject _interactPrompt;
    [SerializeField] private bool isInRange = false;

    void Start() {
        _interactPrompt.SetActive(false);
    }

    void Update() {
        _interactPrompt.SetActive(isInRange);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            CropGrowth cropGrowth = gameObject.GetComponent<CropGrowth>();

            if (cropGrowth != null && cropGrowth.fullyGrown) {
                isInRange = true;
            } else if (cropGrowth == null) {
                isInRange = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            CropGrowth cropGrowth = gameObject.GetComponent<CropGrowth>();

            if (cropGrowth != null && !cropGrowth.fullyGrown) {
                isInRange = false;
            } else if (cropGrowth == null) {
                isInRange = false;
            }
        }
    }
}
