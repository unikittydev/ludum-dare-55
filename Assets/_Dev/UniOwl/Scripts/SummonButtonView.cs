using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Magic
{
    public class SummonButtonView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        public ReactiveProperty<bool> Lock { get; set; } = new(false);

        [SerializeField] private SpriteRenderer _innerCircle;
        [SerializeField] private SpriteRenderer _outerCircle;
        [SerializeField] private SpriteRenderer _hand;

        [SerializeField] private SummonAnimationSequenceView animationSequenceView;
        
        [SerializeField] private SummonButtonState _defaultState;
        [SerializeField] private SummonButtonState _hoverState;
        [SerializeField] private SummonButtonState _clickedState;
        [SerializeField] private SummonButtonState _disabledState;

        [SerializeField] private float _transitionTime;
        
        private Tween _transitionTween;

        private bool pointerOver;

        private IDisposable lockValueChanged;
        
		private void Start()
        {
            _innerCircle.transform.localScale = Vector3.one * _defaultState._innerScale;
            _outerCircle.transform.localScale = Vector3.one * _defaultState._outerScale;
            _hand.transform.localScale = Vector3.one * _defaultState._handScale;
            _innerCircle.color = _defaultState._innerTint;
            _outerCircle.color = _defaultState._outerTint;
            _hand.color = _defaultState._handTint;
        }

        private void OnEnable()
        {
            lockValueChanged = Lock.Subscribe(OnLockedValueChanged);
        }

        private void OnDisable()
        {
            lockValueChanged.Dispose();
			_transitionTween?.Kill();
		}

		private void OnLockedValueChanged(bool locked)
        {
            if (locked)
                DoTransition(_disabledState);
            else
                DoTransition(pointerOver ? _hoverState : _defaultState);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            pointerOver = true;
            if (Lock.Value) return;
            DoTransition(_hoverState);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            pointerOver = false;
            if (Lock.Value) return;
            DoTransition(_defaultState);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Lock.Value) return;
            DoTransition(_clickedState);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (Lock.Value) return;
            DoTransition(pointerOver ? _hoverState : _defaultState);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Lock.Value) return;
            
            animationSequenceView.Summon();
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