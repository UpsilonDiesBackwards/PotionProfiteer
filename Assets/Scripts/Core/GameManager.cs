using UnityEngine;

namespace PPPS.Core {
    public class GameManager : MonoBehaviour {
        public AudioSource audioSource;
        [SerializeField] public AudioTrigger gameMusic;
        [SerializeField] public AudioTrigger gameAmbience;

        public string previousScene = "";

        private static GameManager instance;
        public static GameManager Instance {
            get {
                if (instance == null) instance = GameObject.FindObjectOfType<GameManager>();
                return instance;
            }
        }

        void Start() {
            audioSource = GetComponent<AudioSource>();
        }

        public void UpdatePreviousScene() {
            previousScene = Application.loadedLevelName;
        }
    }
}