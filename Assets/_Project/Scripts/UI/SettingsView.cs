using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class SettingsView : MonoBehaviour
    {
        private bool _paused;
        
        [SerializeField]
        private bool _inGame = true;

        [SerializeField] private GameObject gameMenuButtons;
        [SerializeField] private Slider _musicSlider, _sfxSlider;
        
        [SerializeField] private PanelAnimationView _panelAnimationView;

        private Tween _tween;

        private void Awake()
        {
            SetInGame(_inGame);
            
            _musicSlider.value = StaticStatsSaver.MusicVolume;
            _musicSlider.onValueChanged.Invoke(StaticStatsSaver.MusicVolume);
            _sfxSlider.value = StaticStatsSaver.SfxVolume;
            _sfxSlider.onValueChanged.Invoke(StaticStatsSaver.SfxVolume);
        }
        
		private void OnDisable()
		{
            _tween?.Kill();
		}

        public void UpdateSfxVolume(float value)
        {
            StaticStatsSaver.SfxVolume = value;
        }

        public void UpdateMusicVolume(float value)
        {
            StaticStatsSaver.MusicVolume = value;
        }
        
		public void SetInGame(bool value)
        {
            _inGame = value;

            if (!_inGame)
            {
                Time.timeScale = 1f;
                _paused = false;
            }
            gameMenuButtons.SetActive(_inGame);
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape) || !_inGame) return;
            
            if (!_paused)
                Pause();
            else
                Resume();
        }

        public void PauseWithoutMenu()
        {
            _paused = true;
            Time.timeScale = 0f;
        }
        
        public void Pause()
        {
            if (!_inGame) return;
            PauseWithoutMenu();
            _tween?.Kill();
			_tween = _panelAnimationView.ShowPanel();
		}

        public void Resume()
        {
            if (_inGame)
                _paused = false;
            
            _tween?.Kill();
            _tween = _panelAnimationView.HidePanel().JoinCallback(() =>
            {
                Time.timeScale = 1f;
            });
        }
    }
}