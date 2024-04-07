using Unity.Properties;
using UnityEngine;

public class DoNotDestroyOnLoad : MonoBehaviour {
    private static DoNotDestroyOnLoad _instance;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        
        if (!_instance) {
            _instance = this;
        } else {
            Debug.LogError("More than one " + this.name + "in the scene!");
            Destroy(gameObject);
        }
    }
}
