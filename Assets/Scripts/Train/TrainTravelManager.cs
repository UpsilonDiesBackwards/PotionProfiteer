using System.Collections;
using System.Collections.Generic;
using PPPS.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;

public class TrainTravelManager : MonoBehaviour {
    [Header("References")]
    [SerializeField] private RegionManager _regionManager;
    [SerializeField] private GameObject _travelWindow;
    [SerializeField] private GameObject _finishWindow;
    [SerializeField] private TextMeshProUGUI _travelCost;
    [SerializeField] private TextMeshProUGUI _leaveCost;
    [SerializeField] private GameObject _player;

    [Header("Properties")]
    [SerializeField] private int travelCost = 15;
    [SerializeField] private int gtfohCost = 2000;

    private bool isInRange = false;

    void Awake() {
        _regionManager = GameObject.FindGameObjectWithTag("RegionManager").GetComponent<RegionManager>();
        _player = Player.Instance.gameObject;

        _travelCost.text = "Travel cost: " + travelCost.ToString() + " Spondulixs";
        _leaveCost.text = "Get the HELL out of here travel cost: " + gtfohCost.ToString() + " Spondulixs";
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
            _travelWindow.SetActive(true);
            Player.Instance.Freeze(true);
        }
    }

    public void Travel(int sceneIndex) {
        _regionManager.ActivateScene(sceneIndex);

        if (Player.Instance.spondulixs >= travelCost) {
            _player.GetComponent<Player>().spondulixs -= travelCost;
        }

        GameManager.Instance.UpdatePreviousScene();
        CloseMenu();
    }

    public void FinishGame() {
        if (Player.Instance.spondulixs >= gtfohCost) {
            _player.GetComponent<Player>().spondulixs -= gtfohCost;
            
            _finishWindow.SetActive(true);
            Player.Instance.Freeze(true);
            CloseMenu();
        }
    }

    public void CloseMenu() {
        _travelWindow.SetActive(false);
        Player.Instance.Freeze(false);
    }
}
