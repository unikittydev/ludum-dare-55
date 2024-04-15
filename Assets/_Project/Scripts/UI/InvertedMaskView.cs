using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    public class InvertedMaskView : MonoBehaviour
    {
        [SerializeField] private RectTransform _mask;
        [SerializeField] private RectTransform _canvasRoot; 
        [SerializeField] private Transform _target;
        
        [SerializeField] private Camera _camera; 
        
        [SerializeField] private float _fadeDuration = 3f;

        private Tween _tween;
        
        private void Start()
        {
            FadeOutMask();
        }
        
        private void OnDisable()
        {
            _tween?.Kill();
        }
        
        public Sequence FadeInMask()
        {
            _tween?.Kill();
            
            _mask.gameObject.SetActive(true);
            _mask.anchoredPosition = GetScreenPosition();;
            
            var seq = DOTween.Sequence()
                .Append(_mask.DOSizeDelta(Vector2.zero, _fadeDuration))
                .SetUpdate(true);
            _tween = seq;
            
            return seq;
        }

        public Sequence FadeOutMask()
        {
            _tween?.Kill();
            
            _mask.anchoredPosition = GetScreenPosition();
            _mask.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0f);
            _mask.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
            _mask.gameObject.SetActive(true);
            
            var seq = DOTween.Sequence()
                .AppendInterval(.5f)
                .Append(_mask.DOSizeDelta(Vector2.one * _canvasRoot.rect.width * 2f, _fadeDuration))
                .AppendCallback(() => _mask.gameObject.SetActive(false))
                .SetUpdate(true);
            _tween = seq;
            
            return seq;
        }

        private Vector2 GetScreenPosition()
        {
            var position = _target != null ?
                GetCameraScreenPoint(_camera, _canvasRoot, _target.position) :
                _mask.anchoredPosition;
            
            return position;
        }
        
        private static Vector2 GetCameraScreenPoint(Camera camera, RectTransform canvas, Vector3 worldPosition)
        {
            var viewport = camera.WorldToViewportPoint(worldPosition);
            viewport.x *= canvas.rect.width;
            viewport.y *= canvas.rect.height;
            return new Vector2(viewport.x, viewport.y);
        }
    }
}
