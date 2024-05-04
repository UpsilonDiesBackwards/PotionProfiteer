using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TrainUpgradeMenu : MonoBehaviour {
    [SerializeField] private TrainUpgradeManager _upgradeManager;

    [Header("References")]
    
     [SerializeField] private GameObject _upgradeMenu;
    [SerializeField] private GameObject _greenhouseBuyButton;
    [SerializeField] private GameObject _storageBuyButton;

    [SerializeField] private TextMeshProUGUI _greenhousePrice;
    [SerializeField] private TextMeshProUGUI _storagePrice;

    private bool isInRange = false;

    private bool hasGreenhouse = false;
    private bool hasStorage = false;

    void Awake() {
        _greenhousePrice.text = _upgradeManager.greenHouseCarridgeCost.ToString() + " Spondulixs";
        _storagePrice.text = _upgradeManager.storageCarridgeCost.ToString() + " Spondulixs";
    }

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
            _greenhousePrice.text = "Brought!";
        }

        if (hasStorage) {
            _storageBuyButton.GetComponent<Button>().interactable = false;
            _storagePrice.text = "Brought!s";
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
