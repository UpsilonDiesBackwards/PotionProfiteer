using NUnit.Framework.Constraints;
using PPPS.Core;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Playables;
using UnityEngine;

public class WeatherManager : MonoBehaviour {
    public Weather[] weatherTypes;
    public string[] weatherEffectTags;

    [SerializeField] private AudioTrigger _ambienceTrigger; 
    
    private static WeatherManager instance;
    public static WeatherManager Instance {
        get {
            if (instance == null) instance = GameObject.FindObjectOfType<WeatherManager>();
            return instance;
        }
    }

    public void RollWeather(string sceneName) { // Dungeons and Daddies? 0_0
        Weather selectedWeather = null;

        foreach (Weather weather in weatherTypes) {
            if (weather.name == sceneName) {
                selectedWeather = weather;
                break;
            }
        }

        if (selectedWeather != null && selectedWeather.weatherTypes.Length > 0) {
            WeatherType randomWeatherType = selectedWeather.weatherTypes[Random.Range(0, selectedWeather.weatherTypes.Length)];

            GameObject weatherEffect = GameObject.FindGameObjectWithTag(randomWeatherType.tag);

            // Debug.Log("Selected Weather: " + selectedWeather);
            // Debug.Log("Selected Weather Type: " + randomWeatherType.tag);

            if (weatherEffect != null) {
                ParticleSystem particleSystem = weatherEffect.GetComponentInChildren<ParticleSystem>(true);
                if (particleSystem != null) {
                    GameObject particleSystemGameObject = particleSystem.gameObject;
                    Debug.Log("Activating: " + particleSystemGameObject);
                    ActivateWeather(particleSystemGameObject);

                    PlayAudio(randomWeatherType.audio);
                } else {
                    Debug.LogWarning("ParticleSystem component not found in weather effect: " + weatherEffect.name);
                }
            } else {
                Debug.LogWarning("Weather effect with tag " + randomWeatherType.tag + " not found.");
            }
        }
    }

    void ActivateWeather(GameObject effect) {
        if (effect != null) {
            effect.SetActive(true);
        } else {
            Debug.LogWarning("Weather type does not have a Weather Particle System!");
        }
    }

    public void ClearWeather() {
        foreach (string tag in weatherEffectTags) {
            GameObject[] weatherEffects = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject weatherEffect in weatherEffects) {

                foreach (Transform childTransform in weatherEffect.transform) {
                    GameObject childObject = childTransform.gameObject;
                    childObject.SetActive(false);
                }
            }
        }
    }

    void PlayAudio(AudioClip sound) {
        if (sound != null) {
            _ambienceTrigger.Reset(true, sound, 0.5f);
        } else {
            Debug.LogWarning("Weather type does not have a Weather Audio!");
        }
    }

    public void StopAudio() {
        _ambienceTrigger.Reset(false, null, 0);
    }
}