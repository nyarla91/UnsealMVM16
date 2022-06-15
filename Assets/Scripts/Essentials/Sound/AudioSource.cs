using UnityEngine;

namespace Essentials.Sound
{
    public class AudioSource : MonoBehaviour
    {
        [SerializeField] private UnityEngine.AudioSource _audioSource;

        public void Play(AudioClip clip, float volume)
        {
            _audioSource.clip = clip;
            _audioSource.volume = volume;
            _audioSource.Play();
        }
    }

}