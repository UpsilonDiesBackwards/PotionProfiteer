
using System;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

namespace PPPS.Core {
    public class AnimatorFunctions : MonoBehaviour {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Animator _setBoolInAnimator;

        void Start() {
            if (!_audioSource) _audioSource = Player.Instance.audioSource;    
        }

        public void HidePlayer(bool hide) {
            Player.Instance.Hide(hide);
        }

        public void FreezePlayer(bool freeze) {
            Player.Instance.Freeze(freeze);
        }

        public void PlaySound(AudioClip whichSound) {
            _audioSource.PlayOneShot(whichSound);
        }

        public void EmitParticles(int amount) {
            _particleSystem.Emit(amount);
        }

        public void SetTimeScale(float time) {
            Time.timeScale = time;
        }

        public void SetAnimBoolToFalse(string boolName) {
            _setBoolInAnimator.SetBool(boolName, false);
        }

        public void SetAnimBoolToTrue(string boolName) {
            _setBoolInAnimator.SetBool(boolName, true);
        }

        public void FadeOutMusic() {
            GameManager.Instance.gameMusic.GetComponent<AudioTrigger>().maxVolume = 0.0f;
        }
    }
}
