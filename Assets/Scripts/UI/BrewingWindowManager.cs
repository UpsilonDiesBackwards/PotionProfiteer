using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewingWindowManager : MonoBehaviour {
    [SerializeField] private GameObject _brewingInterface;
    [SerializeField] private GameObject _recipesInterface;

    public BrewingManager brewingManager;

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
            _brewingInterface.SetActive(true);
            Player.Instance.Freeze(true);
        }
    }

    public void Brew() {
        CloseMenu();
        brewingManager.ConfirmIngredients();
        Debug.Log("Confirming ingredients...");
    }

    public void CloseMenu() {
        _brewingInterface.SetActive(false);
        Player.Instance.Freeze(false);
    }

    public void ToggleRecipeTab() {
        if (_recipesInterface.activeInHierarchy) {
            _recipesInterface.SetActive(false);
        }
        else {
            _recipesInterface.SetActive(true);
        }
    }
}
