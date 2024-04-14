using DG.Tweening;
using UniOwl.Audio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI
{
    public class UniSelectableView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Selectable _selectable;

        [SerializeField] private Graphic _image;
        [SerializeField] private Graphic _text;

        [SerializeField] private AudioClip hoverClip;
        [SerializeField] private AudioClip clickClip;

        [SerializeField] private Color _defaultImageColor;
        [SerializeField] private Color _hoverImageColor;
        [SerializeField] private Color _clickImageColor;
        [SerializeField] private Color _defaultTextColor;
        [SerializeField] private Color _hoverTextColor;
        [SerializeField] private Color _clickTextColor;

        [SerializeField] private float fadeDuration = .1f;

        private Tween _imageTweener, _textTweener;

        private void OnEnable()
        {
            _image.color = _defaultImageColor;
            _text.color = _defaultTextColor;
        }

        private void OnDisable()
        {
            _imageTweener.Kill();
            _textTweener.Kill();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_selectable != null && !_selectable.interactable) return;

            _imageTweener.Kill();
            _textTweener.Kill();
            _imageTweener = _image.DOColor(_hoverImageColor, fadeDuration).SetUpdate(true);
            _textTweener = _text.DOColor(_hoverTextColor, fadeDuration).SetUpdate(true);
            AudioSFXSystem.PlayClip2D(hoverClip);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_selectable != null && !_selectable.interactable) return;

            _imageTweener.Kill();
            _textTweener.Kill();
            _imageTweener = _image.DOColor(_defaultImageColor, fadeDuration).SetUpdate(true);
            _textTweener = _text.DOColor(_defaultTextColor, fadeDuration).SetUpdate(true);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_selectable != null && !_selectable.interactable) return;

            _imageTweener.Kill();
            _textTweener.Kill();
            _imageTweener = _image.DOColor(_clickImageColor, fadeDuration).SetUpdate(true);
            _textTweener = _text.DOColor(_clickTextColor, fadeDuration).SetUpdate(true);
            AudioSFXSystem.PlayClip2D(clickClip);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_selectable != null && !_selectable.interactable) return;

            _imageTweener.Kill();
            _textTweener.Kill();
            _imageTweener = _image.DOColor(_hoverImageColor, fadeDuration).SetUpdate(true);
            _textTweener = _text.DOColor(_hoverTextColor, fadeDuration).SetUpdate(true);
        }
    }
}