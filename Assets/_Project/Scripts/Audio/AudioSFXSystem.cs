using UnityEngine;
using UnityEngine.Audio;

namespace UniOwl.Audio
{
    public class AudioSFXSystem : MonoBehaviour
    {
        private static AudioSFXSystem _instance;

        [SerializeField] private AudioSource audioSourcePrefab;
        [SerializeField] private AudioMixerGroup sfxGroup;

        private ComponentPool<AudioSource> _pool;

        [SerializeField] private AudioSource musicSource;
        
        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else if (_instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            _pool = new ComponentPool<AudioSource>(audioSourcePrefab, 0);
            DontDestroyOnLoad(this);
        }

        public static void PlayMusic(AudioClip clip)
        {
            _instance.musicSource.Stop();
            _instance.musicSource.clip = clip;
            _instance.musicSource.Play();
        }

        public static void StopMusic()
        {
            _instance.musicSource.Stop();
        }
        
        public static void PlayCue2D(AudioCue cue) =>
            PlayCue(cue, Vector3.zero, _instance.transform, true);
        
        public static void PlayCue(AudioCue cue, Vector3 position) =>
            PlayCue(cue, position, _instance.transform, false);
        
        public static void PlayCue(AudioCue cue, Vector3 position, Transform parent) =>
            PlayCue(cue, position, parent, false);

        private static void PlayCue(AudioCue cue, Vector3 position, Transform parent, bool is2D)
        {
            if (!cue) return;
            
            var clip = cue.GetRandomClip();

            var source = _instance._pool.Get(position, Quaternion.identity, parent);
            source.clip = clip;
            source.outputAudioMixerGroup = cue.GroupOverride ? cue.GroupOverride : _instance.sfxGroup;
            source.spatialBlend = is2D ? 0f : 1f;
            
            source.volume = cue.GetVolume();
            source.pitch = cue.GetPitch();
            
            source.Play();
            
            Destroy(source.gameObject, clip.length);
        }

        public static void PlayClip2D(AudioClip clip) =>
            PlayClip(clip, Vector3.zero, _instance.transform, true);

        public static void PlayClip(AudioClip clip, Vector3 position) =>
            PlayClip(clip, position, _instance.transform, false);

        public static void PlayClip(AudioClip clip, Vector3 position, Transform parent) =>
            PlayClip(clip, position, parent, false);
        
        private static void PlayClip(AudioClip clip, Vector3 position, Transform parent, bool is2D)
        {
            if (!clip) return;
            
            var source = _instance._pool.Get(position, Quaternion.identity, parent);
            source.clip = clip;
            source.outputAudioMixerGroup = _instance.sfxGroup;
            source.spatialBlend = is2D ? 0f : 1f;
            source.Play();

            Destroy(source.gameObject, clip.length);
        }
    }
}
