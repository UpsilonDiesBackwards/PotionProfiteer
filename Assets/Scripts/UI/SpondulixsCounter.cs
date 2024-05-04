using UnityEngine;
using TMPro;

public class SpondulixsCounter : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _counterText;

    void Update() {
        _counterText.text = Player.Instance.spondulixs.ToString();
    }
}
