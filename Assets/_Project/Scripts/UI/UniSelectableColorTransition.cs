using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI
{
    public class UniSelectableColorTransition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Selectable _selectable;

        [SerializeField] private Graphic _targetGraphic;
        
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _hoverColor;
        [SerializeField] private Color _clickColor;

        [SerializeField] private float fadeDuration = .1f;

        private Tween _tweener;

        private void OnEnable()
        {
            _targetGraphic.color = _defaultColor;
        }

        private void OnDisable()
        {
            _tweener.Kill();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_selectable != null && !_selectable.interactable) return;

            _tweener.Kill();
            _tweener = _targetGraphic.DOColor(_hoverColor, fadeDuration).SetUpdate(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_selectable != null && !_selectable.interactable) return;

            _tweener.Kill();
            _tweener = _targetGraphic.DOColor(_defaultColor, fadeDuration).SetUpdate(true);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_selectable != null && !_selectable.interactable) return;

            _tweener.Kill();
            _tweener = _targetGraphic.DOColor(_clickColor, fadeDuration).SetUpdate(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_selectable != null && !_selectable.interactable) return;

            _tweener.Kill();
            _tweener = _targetGraphic.DOColor(_hoverColor, fadeDuration).SetUpdate(true);
        }
    }
}