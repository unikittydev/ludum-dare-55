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

        [SerializeField] private GameObject mainMenu;
        [SerializeField] private SettingsView settingsView;

        [SerializeField] private int gameBuildIndex;
        
        private Tween _tween;

        private void Start()
        {
            _mask.sizeDelta = Vector2.zero;
            _mask.anchoredPosition = new Vector2(Screen.width, Screen.height) * .5f;
            FadeOutMask();
        }

        public void LoadGame()
        {
            DOTween.Sequence()
                .Append(FadeInMask())
                .AppendCallback(() => mainMenu.SetActive(false))
                .AppendCallback(() => settingsView.SetInGame(true))
                .Append(FadeOutMask())
                .SetUpdate(true);
        }
        
        public void LoadMenu()
        {
            DOTween.Sequence()
                .Append(settingsView.HidePanel())
                .Append(FadeInMask())
                .AppendCallback(() => settingsView.SetInGame(false))
                .AppendCallback(() => mainMenu.SetActive(true))
                .Append(FadeOutMask())
                .SetUpdate(true);
        }

        public Sequence FadeInMask() => FadeInMask(_mask.anchoredPosition);

        public Sequence FadeInMask(Vector2 position)
        {
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
