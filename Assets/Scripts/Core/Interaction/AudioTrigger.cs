using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PPPS.Core
{
    public class AudioTrigger : MonoBehaviour{
        private AudioSource _audioSource;

        [SerializeField] private bool _autoPlay;
        [SerializeField] private float _fadeInSpeed;
        [SerializeField] private float _fadeOutSpeed;
        [SerializeField] private bool _loop;
        [SerializeField] public AudioClip sound;

        public float maxVolume;
        private bool _triggered;

        void Start() {
            Reset(false, sound, 0);
            StartCoroutine(EnableCollider());
        }

        void Update() {
            _audioSource.loop = _loop;

            if (_triggered || _autoPlay) {
                if (!_audioSource.isPlaying) {
                    _audioSource.Play();
                }

                if (_audioSource.volume < maxVolume) {
                    _audioSource.volume += _fadeInSpeed * Time.deltaTime;
                }
            } else {
                if (_audioSource.volume > 0) {
                    _audioSource.volume -= _fadeOutSpeed * Time.deltaTime;
                } else {
                    _audioSource.Stop();
                }
            }
        }

        void OnTriggerStay2D(Collider2D col) {
            if (col.gameObject == Player.Instance.gameObject) {
                if (!_triggered) {
                    _triggered = true;
                }
            }
        }

        void OnTriggerExit2D(Collider2D col) {
            if (col == Player.Instance) {
                _triggered = false;
            }
        }

        public void Reset(bool play, AudioClip clip, float startVolume = 1.0f) {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = startVolume;
            _audioSource.clip = clip;

            if (play) {
                _audioSource.Stop();
                _audioSource.Play();
            }
        }

        private IEnumerator EnableCollider() {
            /*
                If the player spawns inside a trigger area, wait 4 seconds to enable so the trigger will occur.
            */
            yield return new WaitForSeconds(4.0f);
            GetComponent<BoxCollider2D>().enabled = true;
        }

    }
}
