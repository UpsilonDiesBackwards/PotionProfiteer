using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TrainUpgradeMenu : MonoBehaviour {
    private bool isInRange = false;

    [SerializeField] private GameObject _upgradeMenu;
    [SerializeField] private GameObject _greenhouseBuyButton;
    [SerializeField] private GameObject _storageBuyButton;

    [SerializeField] private TrainUpgradeManager _upgradeManager;
    private bool hasGreenhouse = false;
    private bool hasStorage = false;

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
            _upgradeMenu.SetActive(true);
            Player.Instance.Freeze(true);
        }

        if (hasGreenhouse) {
            _greenhouseBuyButton.GetComponent<Button>().interactable = false;
        }

        if (hasStorage) {
            _storageBuyButton.GetComponent<Button>().interactable = false;
        }
    }

    public void GreenHousePurchase() {
        hasGreenhouse = _upgradeManager.BuyGreenHouse();
    }

    public void StoragePurchase() {
        hasStorage = _upgradeManager.BuyStorage();
    }

    public void CloseMenu() {
        _upgradeMenu.SetActive(false);
        Player.Instance.Freeze(false);
    }
}
