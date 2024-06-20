using UnityEngine;

namespace Core
{
    public class AudioManager : MonoBehaviour
    {
        private AudioSource _audioSource;

        public float _volume { get; private set; }

        public void Construct()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(AudioClip clip)
        {
            if (_audioSource == null) return;

            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }

            _audioSource.clip = clip;
            _audioSource.Play();
        }

        public void StopSound()
        {
            if (_audioSource == null) return;

            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }
        }

        public void SetVolumeValue(float value)
        {
            _audioSource.volume = value;
            _volume = value;
        }
    }
}
