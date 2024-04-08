using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegionActivator : MonoBehaviour {
    private RegionManager _regionManager;
    public int sceneIndex;

    void Awake() {
        _regionManager = GameObject.FindGameObjectWithTag("RegionManager").GetComponent<RegionManager>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            _regionManager.ActivateScene(sceneIndex);
            Debug.Log("attempting to load scene: " + SceneManager.GetSceneByBuildIndex(sceneIndex).name);
        }
    }
}
