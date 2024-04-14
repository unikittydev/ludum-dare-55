using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private RectTransform _mask;
        [SerializeField] private Transform _target;

        [SerializeField] private float _fadeDuration = 3f;

        [SerializeField] private PanelAnimationView _setingsAnimationView;
        [SerializeField] private PanelAnimationView _leaderboardAnimationView;
        [SerializeField] private SettingsView _settingsView;
        [SerializeField] private Camera _camera; 
        
        [SerializeField] private int gameBuildIndex;
        
        private Tween _tween;
        private Tween _secondTween;

        private void Start()
        {
            _mask.sizeDelta = Vector2.zero;
            _mask.anchoredPosition = new Vector2(Screen.width, Screen.height) * .5f;
            FadeOutMask();
        }
		private void OnDisable()
		{
            _tween?.Kill();
            _secondTween?.Kill();
		}

		public void RestartGame()
        {
            var panel = _setingsAnimationView.Visible ? _setingsAnimationView : _leaderboardAnimationView;

			_secondTween = DOTween.Sequence()
                .Append(panel.HidePanel())
                .Append(FadeInMask())
                .AppendCallback(() => SceneManager.LoadScene(gameBuildIndex))
                .AppendCallback(() => Time.timeScale = 1f)
                .SetUpdate(true);
        }

        public Sequence FadeInMask()
        {
            Vector2 position;

            if (_target != null)
                position = RectTransformUtility.WorldToScreenPoint(_camera, _target.position);
            else
                position = _mask.anchoredPosition;
            
            _tween?.Kill();
            
            _mask.gameObject.SetActive(true);
            _mask.anchoredPosition = position;
            
            var seq = DOTween.Sequence()
                .Append(_mask.DOSizeDelta(Vector2.zero, _fadeDuration))
                .SetUpdate(true);
            _tween = seq;
            
            return seq;
        }

        public Sequence FadeOutMask() => FadeOutMask(_mask.anchoredPosition);
        
        public Sequence FadeOutMask(Vector2 position)
        {
            _tween?.Kill();
            
            _mask.anchoredPosition = position;
            _mask.gameObject.SetActive(true);
            
            var seq = DOTween.Sequence()
                .Append(_mask.DOSizeDelta(new Vector2(Screen.width, Screen.width) * 2f, _fadeDuration))
                .AppendCallback(() => _mask.gameObject.SetActive(false))
                .SetUpdate(true);
            _tween = seq;
            
            return seq;
        }
    }
}
