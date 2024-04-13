using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    public class MainMenuLoader : MonoBehaviour
    {
        [SerializeField] private RectTransform _mask;
        [SerializeField] private Transform _target;

        [SerializeField] private float _fadeDuration = 3f;

        [SerializeField] private PauseView _pauseView;
        
        public void EnableTransition()
        {
            var centerPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, _target.position);
            
            _mask.gameObject.SetActive(true);
            _mask.anchoredPosition = centerPoint;

            DOTween.Sequence().Append(_pauseView.HidePanelSequence())
                .Append(_mask.DOSizeDelta(Vector2.zero, _fadeDuration))
                .Append(_mask.DOSizeDelta(new Vector2(Screen.width, Screen.width) * 2f, _fadeDuration))
                .SetUpdate(true);
        }
    }
}
