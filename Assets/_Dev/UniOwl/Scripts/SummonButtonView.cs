using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Magic
{
    public class SummonButtonView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [SerializeField] private SpriteRenderer _innerCircle;
        [SerializeField] private SpriteRenderer _outerCircle;
        [SerializeField] private SpriteRenderer _hand;

        [SerializeField] private ParticleSystem _psSummon;


        [SerializeField] private SummonButtonState _defaultState;
        [SerializeField] private SummonButtonState _hoverState;
        [SerializeField] private SummonButtonState _clickedState;

        [SerializeField] private float _transitionTime;
        
        private Tween _transitionTween;

        private bool pointerOver;
        
        private void Start()
        {
            _innerCircle.transform.localScale = Vector3.one * _defaultState._innerScale;
            _outerCircle.transform.localScale = Vector3.one * _defaultState._outerScale;
            _hand.transform.localScale = Vector3.one * _defaultState._handScale;
            _innerCircle.color = _defaultState._innerTint;
            _outerCircle.color = _defaultState._outerTint;
            _hand.color = _defaultState._handTint;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            pointerOver = true;
            DoTransition(_hoverState);
            Debug.Log("Enter");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            pointerOver = false;
            DoTransition(_defaultState);
            Debug.Log("Exit");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            DoTransition(_clickedState);
            Debug.Log("Down");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            DoTransition(pointerOver ? _hoverState : _defaultState);
            Debug.Log("Up");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _psSummon.Play();
            Debug.Log("Click");
        }

        private void DoTransition(SummonButtonState state)
        {
            _transitionTween?.Kill();
            
            _transitionTween = 
                DOTween.Sequence()
                    .Join(_innerCircle.transform.DOScale(state._innerScale, _transitionTime))
                    .Join(_innerCircle.DOColor(state._innerTint, _transitionTime))
                    .Join(_outerCircle.transform.DOScale(state._outerScale, _transitionTime))
                    .Join(_outerCircle.DOColor(state._outerTint, _transitionTime))
                    .Join(_hand.transform.DOScale(state._handScale, _transitionTime))
                    .Join(_hand.DOColor(state._handTint, _transitionTime));
        }
    }

}