using UnityEngine;

namespace PPPS.Core {
    public class GameManager : MonoBehaviour {
        public AudioSource audioSource;
        [SerializeField] public AudioTrigger gameMusic;
        [SerializeField] public AudioTrigger gameAmbience;

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
    }
}