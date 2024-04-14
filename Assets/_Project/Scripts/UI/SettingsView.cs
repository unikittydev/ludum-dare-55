using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _outerPanel;
        [SerializeField] private RectTransform _innerPanel;
        [SerializeField] private GameObject _raycastOverlay;

        [SerializeField] private float _moveSpeed;
        
        private Tween _tween;

        private bool paused;
        
        [SerializeField]
        private bool inGame = true;

        [SerializeField] private GameObject mainMenuButtons, gameMenuButtons;

        private void Awake()
        {
            _innerPanel.pivot = new Vector2(0.5f, 1f);
            _innerPanel.anchoredPosition = new Vector2(0f, -100f);
            
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

        public void Pause()
        {
            if (inGame)
            {
                paused = true;
                Time.timeScale = 0f;
            }
            
            ShowPanel();
        }

        public void Resume()
        {
            if (inGame)
                paused = false;
            
            HidePanel().AppendCallback(() =>
            {
                if (inGame)
                    Time.timeScale = 1f;
            });
        }

        public Sequence ShowPanel()
        {
            _tween?.Kill();
            _outerPanel.gameObject.SetActive(true);

            var seq = DOTween.Sequence().
                Append(_innerPanel.DOPivot(new Vector2(0.5f, 0f), _moveSpeed)).
                Join(_innerPanel.DOMoveY(100f, _moveSpeed)).
                AppendCallback(() => _raycastOverlay.SetActive(false)).
                SetUpdate(true);
            _tween = seq;

            return seq;
        }
        
        public Sequence HidePanel()
        {
            _tween?.Kill();

            var seq = DOTween.Sequence().
                Append(_innerPanel.DOPivot(new Vector2(0.5f, 1f), _moveSpeed)).
                Join(_innerPanel.DOMoveY(-100f, _moveSpeed)).
                AppendCallback(() =>
                {
                    _outerPanel.gameObject.SetActive(false);
                    _raycastOverlay.SetActive(true);
                }).
                SetUpdate(true);
            _tween = seq;

            return seq;
        }
    }
}