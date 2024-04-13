using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    public class PauseView : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _outerPanel;
        [SerializeField] private RectTransform _innerPanel;
        [SerializeField] private GameObject _raycastOverlay;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private Vector2 _moveAxis = Vector2.up;
        
        private Tween _tween;

        private bool paused;
        
        private void Start()
        {
            _innerPanel.pivot = new Vector2(0.5f, 1f);
            _innerPanel.anchoredPosition = new Vector2(0f, -100f);
        }
        
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            
            if (!paused)
                Pause();
            else
                Resume();
        }

        public void Pause()
        {
            paused = true;
            Time.timeScale = 0f;
            
            _tween?.Kill();
            
            _outerPanel.gameObject.SetActive(true);
            _tween = DOTween.Sequence().
                Append(_innerPanel.DOPivot(new Vector2(0.5f, 0f), _moveSpeed)).
                Join(_innerPanel.DOMoveY(100f, _moveSpeed)).
                AppendCallback(() => _raycastOverlay.SetActive(false)).
                SetUpdate(true);
        }

        public void Resume()
        {
            paused = false;
            
            _tween?.Kill();

            _tween = HidePanelSequence().AppendCallback(() =>
            {
                _outerPanel.gameObject.SetActive(false);
                _raycastOverlay.SetActive(true);
                Time.timeScale = 1f;
            });
        }

        public Sequence HidePanelSequence()
        {
            return DOTween.Sequence().
                Append(_innerPanel.DOPivot(new Vector2(0.5f, 1f), _moveSpeed)).
                Join(_innerPanel.DOMoveY(-100f, _moveSpeed)).
                SetUpdate(true);
        }
    }
}