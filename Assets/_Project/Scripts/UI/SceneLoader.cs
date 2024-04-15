using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private PanelAnimationView _setingsAnimationView;
        [SerializeField] private PanelAnimationView _leaderboardAnimationView;
        [SerializeField] private InvertedMaskView _maskView;
        [SerializeField] private ThemeSwitcher _themeSwitcher;
        
        [SerializeField] private int gameBuildIndex;
        
        private Tween _tween;

        private void OnDisable()
        {
            _tween?.Kill();
        }

        public void RestartGame()
        {
            var panel = _setingsAnimationView.Visible ? _setingsAnimationView : _leaderboardAnimationView;

			_tween = DOTween.Sequence()
                .Append(panel.HidePanel())
                .Append(_maskView.FadeInMask())
                .Join(_themeSwitcher.FadeOutAllThemes())
                .AppendCallback(() => SceneManager.LoadScene(gameBuildIndex))
                .AppendCallback(() => Time.timeScale = 1f)
                .SetUpdate(true);
        }
    }
}
