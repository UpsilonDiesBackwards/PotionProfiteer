using UnityEditor;
using UnityEngine;

public class TrainUpgradeManager : MonoBehaviour {
    [Header("References")]
    [SerializeField] private GameObject greenHouseCarridge;
    [SerializeField] private GameObject storageCarridge;
    
    // Colliders
    [SerializeField] private GameObject greenHouseCarridgeCollider;
    [SerializeField] private GameObject storageCarridgeCollider;

    [Header("Costs")]
    public int greenHouseCarridgeCost = 45;
    public int storageCarridgeCost = 90;
    private bool hasBroughtGreenHouse = false;
    private bool hasBroughtStorage = false;
    
    public bool BuyGreenHouse() {
        if (Player.Instance.spondulixs >= greenHouseCarridgeCost && !hasBroughtGreenHouse) {
            greenHouseCarridge.SetActive(true);
            greenHouseCarridgeCollider.SetActive(false);

            Player.Instance.spondulixs -= greenHouseCarridgeCost;
            hasBroughtGreenHouse = true;
            return true;
        }
        return false;
    }

    public bool BuyStorage() {
        if (Player.Instance.spondulixs >= storageCarridgeCost && !hasBroughtStorage) {
            storageCarridge.SetActive(true);
            storageCarridgeCollider.SetActive(false);
            
            Player.Instance.spondulixs -= storageCarridgeCost;
            hasBroughtStorage = true;
            return true;
        }
        return false;
    }
}
