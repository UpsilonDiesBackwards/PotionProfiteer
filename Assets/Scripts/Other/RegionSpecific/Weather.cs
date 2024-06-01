using UnityEngine;

[System.Serializable]
public struct WeatherType
{
    public string name;
    public string tag; // since you can not reference scene objects in SOs, find the game objects via tags instead.
    public AudioClip audio;
}

[CreateAssetMenu(fileName = "Weather", menuName = "New/Weather")]
public class Weather : ScriptableObject {
    public WeatherType[] weatherTypes;
}
