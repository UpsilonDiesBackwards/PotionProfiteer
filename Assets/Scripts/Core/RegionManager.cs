using System.Collections;
using PPPS.Core;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class RegionManager : MonoBehaviour {
    public string[] scenesToLoad;
    private int currentSceneIndex = -1;

    public GameObject player;

    void Awake() {
        foreach (string scene in scenesToLoad) {
            StartCoroutine(LoadSceneAsync(scene));
            // Debug.Log("Loaded" + scene);
        }
    }

    IEnumerator LoadSceneAsync(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone) {
            yield return null;
        }

        Scene loadedScene = SceneManager.GetSceneByName(sceneName);
        loadedScene.GetRootGameObjects()[0].SetActive(false);

        ActivateScene(5); // '0' is the index for the desert scene.

        GameManager.Instance.UpdatePreviousScene();
    }

    public void ActivateScene(int sceneIndex) {
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCount) {
            if (currentSceneIndex != -1) { // deactivate root obj of prev scene
                Scene previousScene = SceneManager.GetSceneAt(currentSceneIndex);
                GameObject[] previousRootObjects = previousScene.GetRootGameObjects();
                foreach (GameObject rootObject in previousRootObjects) {
                    rootObject.SetActive(false);
                }
            }

            // Debug.Log("CurrentScene: " + currentSceneIndex + " sceneIndex: " + sceneIndex);

            // activate root obj of new scene
            Scene nextScene = SceneManager.GetSceneAt(sceneIndex);
            SceneManager.SetActiveScene(nextScene);
            GameObject[] nextRootObjects = nextScene.GetRootGameObjects();
            foreach (GameObject rootObject in nextRootObjects) {
                rootObject.SetActive(true);
            }


            WeatherManager.Instance.ClearWeather();
            WeatherManager.Instance.StopAudio();
            WeatherManager.Instance.RollWeather(nextScene.name);

            TeleportPlayerToIndicator(nextScene);

            currentSceneIndex = sceneIndex;
        } else {
            Debug.LogWarning("Invalid scene index: " + sceneIndex);
        }
    }


    public void TeleportPlayerToIndicator(Scene scene) {
        GameObject indicator = GameObject.FindGameObjectWithTag("TeleportIndicator");

        if (indicator && player) {
            player.transform.position = indicator.transform.position;
        } else {
            Debug.LogWarning("Teleporter indicator or player not found!");
        }
    }

}
