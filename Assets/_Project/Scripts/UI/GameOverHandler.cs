using System;
using Game.Battle;
using Game.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI
{
    public class GameOverHandler : MonoBehaviour
    {
        [Inject] private Battleground _battleground;

        private IDisposable _onLeftSideDieCallback;

        [SerializeField] private PanelAnimationView _leaderBoardPanel;
        [SerializeField] private SettingsView _settingsView;
        
        private void OnEnable()
        {
            _onLeftSideDieCallback = _battleground.OnLeftSideDie.Subscribe(OnLeftSideDie);
        }

        private void OnDisable()
        {
            _onLeftSideDieCallback.Dispose();
        }

        private void OnLeftSideDie(Unit _)
        {
            _leaderBoardPanel.ShowPanel();
            _settingsView.SetInGame(false);
            _settingsView.PauseWithoutMenu();
        }
    }
}