using UnityEngine;

namespace DefaultNamespace
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _uiSource;
        [SerializeField] private AudioSource _effectsSource;

        [SerializeField] private AudioClip _onClickClip;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
            }
            
            Instance = this;
        }

        public void StartMusic(AudioClip clip)
        {
            _musicSource.clip = clip;
            _musicSource.loop = true;
            _musicSource.Play();
        }

        public void PlayUiSound(AudioClip clip)
        {
            _uiSource.PlayOneShot(clip);
        }

        public void PlayOnClick()
        {
            PlayUiSound(_onClickClip);
        }

        public void PlayEffectSound(AudioClip clip)
        {
            _effectsSource.PlayOneShot(clip);
        }
    }
}