using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    public class SettingsView : MonoBehaviour
    {
        private bool paused;
        
        [SerializeField]
        private bool inGame = true;

        [SerializeField] private GameObject mainMenuButtons, gameMenuButtons;

        [SerializeField] private PanelAnimationView _panelAnimationView;

        private void Awake()
        {
            SetInGame(inGame);
        }

        public void SetInGame(bool value)
        {
            inGame = value;

            if (!inGame)
            {
                Time.timeScale = 1f;
                paused = false;
            }
            mainMenuButtons.SetActive(!inGame);
            gameMenuButtons.SetActive(inGame);
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape) || !inGame) return;
            
            if (!paused)
                Pause();
            else
                Resume();
        }

        public void PauseWithoutMenu()
        {
            if (!inGame) return;
            paused = true;
            Time.timeScale = 0f;
        }
        
        public void Pause()
        {
            PauseWithoutMenu();
            _panelAnimationView.ShowPanel();
        }

        public void Resume()
        {
            if (inGame)
                paused = false;
            
            _panelAnimationView.HidePanel().AppendCallback(() =>
            {
                if (inGame)
                    Time.timeScale = 1f;
            });
        }
    }
}