using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    public class PanelAnimationView : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _outerPanel;
        [SerializeField] private RectTransform _innerPanel;
        [SerializeField] private GameObject _raycastOverlay;

        [SerializeField] private float _moveSpeed;

        private Tween _tween;

        private bool _visible;
        public bool Visible => _visible;
        
        private void Awake()
        {
            _innerPanel.pivot = new Vector2(0.5f, 1f);
            _innerPanel.anchoredPosition = new Vector2(0f, -100f);
        }

        public Sequence ShowPanel()
        {
            _tween?.Kill();
            _outerPanel.gameObject.SetActive(true);

            var seq = DOTween.Sequence().Append(_innerPanel.DOPivot(new Vector2(0.5f, 0f), _moveSpeed))
                .Join(_innerPanel.DOMoveY(100f, _moveSpeed)).AppendCallback(() =>
                {
                    _raycastOverlay.SetActive(false);
                    _visible = true;
                })
                .SetUpdate(true);
            _tween = seq;

            return seq;
        }

        public Sequence HidePanel()
        {
            _tween?.Kill();

            var seq = DOTween.Sequence().Append(_innerPanel.DOPivot(new Vector2(0.5f, 1f), _moveSpeed))
                .Join(_innerPanel.DOMoveY(-100f, _moveSpeed)).AppendCallback(() =>
                {
                    _outerPanel.gameObject.SetActive(false);
                    _raycastOverlay.SetActive(true);
                    _visible = false;
                }).SetUpdate(true);
            _tween = seq;

            return seq;
        }
    }
}
