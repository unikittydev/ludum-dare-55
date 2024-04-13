using UnityEngine;
using UnityEngine.Audio;

namespace UniOwl.Audio
{
    public class AudioMixerManager : MonoBehaviour
    {
        private const string sfxVolume = "sfxVolume";
        private const string musicVolume = "musicVolume";
        
        [SerializeField] private AudioMixer mixer;

        public static float GetVolumeFromSliderValue(float value)
        {
            return Mathf.Log10(Mathf.Max(value, .0001f)) * 20f;
        }
        
        public void SetSFXVolume(float value)
        {
            mixer.SetFloat(sfxVolume, GetVolumeFromSliderValue(value));
        }

        public void SetMusicVolume(float value)
        {
            mixer.SetFloat(musicVolume, GetVolumeFromSliderValue(value));
        }
    }
}