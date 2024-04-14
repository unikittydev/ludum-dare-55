using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    public class SettingsView : MonoBehaviour
    {
        private bool _paused;
        
        [SerializeField]
        private bool _inGame = true;

        [SerializeField] private GameObject mainMenuButtons, gameMenuButtons;

        [SerializeField] private PanelAnimationView _panelAnimationView;
        
        private void Awake()
        {
            SetInGame(_inGame);
        }

        public void SetInGame(bool value)
        {
            _inGame = value;

            if (!_inGame)
            {
                Time.timeScale = 1f;
                _paused = false;
            }
            mainMenuButtons.SetActive(!_inGame);
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
            if (!_inGame) return;
            _paused = true;
            Time.timeScale = 0f;
        }
        
        public void Pause()
        {
            PauseWithoutMenu();
            _panelAnimationView.ShowPanel();
        }

        public void Resume()
        {
            if (_inGame)
                _paused = false;
            
            _panelAnimationView.HidePanel().AppendCallback(() =>
            {
                if (_inGame)
                    Time.timeScale = 1f;
            });
        }
    }
}