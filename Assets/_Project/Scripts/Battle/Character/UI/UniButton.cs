using UniOwl.Audio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace UniOwl.UI
{
    public class UniButton : Button
    {
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

        protected override void OnEnable()
        {
            base.OnEnable();
            _image.color = _defaultImageColor;
            _text.color = _defaultTextColor;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _imageTweener.Kill();
            _textTweener.Kill();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            
            if (!interactable) return;
            
            _imageTweener.Kill();
            _textTweener.Kill();
            _imageTweener = _image.DOColor(_hoverImageColor, fadeDuration).SetUpdate(true);
            _textTweener = _text.DOColor(_hoverTextColor, fadeDuration).SetUpdate(true);
            AudioSFXSystem.PlayClip2D(hoverClip);
        }
        
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            
            if (!interactable) return;
            
            _imageTweener.Kill();
            _textTweener.Kill();
            _imageTweener = _image.DOColor(_defaultImageColor, fadeDuration).SetUpdate(true);
            _textTweener = _text.DOColor(_defaultTextColor, fadeDuration).SetUpdate(true);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            if (!interactable) return;
            
            _imageTweener.Kill();
            _textTweener.Kill();
            _imageTweener = _image.DOColor(_clickImageColor, fadeDuration).SetUpdate(true);
            _textTweener = _text.DOColor(_clickTextColor, fadeDuration).SetUpdate(true);
            AudioSFXSystem.PlayClip2D(clickClip);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            
            if (!interactable) return;
            
            _imageTweener.Kill();
            _textTweener.Kill();
            _imageTweener = _image.DOColor(_hoverImageColor, fadeDuration).SetUpdate(true);
            _textTweener = _text.DOColor(_hoverTextColor, fadeDuration).SetUpdate(true);
        }
    }
}