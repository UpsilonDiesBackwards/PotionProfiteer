using JetBrains.Annotations;
using PPPS.Core;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class RegionActivator : MonoBehaviour {
    private RegionManager _regionManager;
    public int sceneIndex;

    public bool useGlobalLastScene = false;

    void Awake() {
        _regionManager = GameObject.FindGameObjectWithTag("RegionManager").GetComponent<RegionManager>();
    }

    void Update() {
        if (useGlobalLastScene) {
            switch(GameManager.Instance.previousScene) {
                case "Desert":
                    sceneIndex = 1;
                    break;
                case "Plains":
                    sceneIndex = 2;
                    break;
                case "Tundra":
                    sceneIndex = 3;
                    break;
                case "Bayou":
                    sceneIndex = 4;
                    break;
                default:
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            _regionManager.ActivateScene(sceneIndex);
        }
    }
}
