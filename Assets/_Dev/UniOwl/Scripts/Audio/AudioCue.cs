using UnityEngine;
using UnityEngine.Audio;

namespace UniOwl.Audio
{
    [CreateAssetMenu(menuName = "Audio/Audio Cue", fileName = "AC_Cue")]
    public class AudioCue : ScriptableObject
    {
        [SerializeField] private AudioClip[] clip;
        [SerializeField] private float minPitch = 1f, maxPitch = 1f;
        [SerializeField] private float minVolume = 1f, maxVolume = 1f;
        
        [SerializeField] private AudioMixerGroup groupOverride;

        public AudioClip GetRandomClip() => clip[Random.Range(0, clip.Length)];

        public float GetPitch() => Random.Range(minPitch, maxPitch);

        public float GetVolume() => Random.Range(minVolume, maxVolume);

        public AudioMixerGroup GroupOverride => groupOverride;
    }
}