using System;
using DG.Tweening;
using Game.Battle;
using Game.Configs;
using Game.Summoning;
using UniRx;
using UnityEngine;
using Zenject;

public class ThemeSwitcher : MonoBehaviour
{
    [SerializeField] private AudioSource _startSource;
    [SerializeField] private AudioSource _battleSource;
    
    [SerializeField] private float _fadeDuration = .1f;

    [Inject] private SummonProvider _summonProvider;
    [Inject] private EnemiesConfig _config;
    [Inject] private Battleground _battleground;

    private IDisposable _summonCallback;
    private int _count;

    private IDisposable _leftSideDieCallback;
    
    private Tween _tween;

    private void Start()
    {
        PlayStartTheme();
        
        _count = 0;
        
        _summonCallback?.Dispose();
        _leftSideDieCallback?.Dispose();
        
        _summonCallback = _summonProvider.OnSummon
            .Subscribe(_ =>
            {
                _count++;
                if (_count < _config.SpawnAfterPlayerSummons)
                    return;
            
                _summonCallback.Dispose();
                SwitchToBattleTheme();
            });

        _leftSideDieCallback = _battleground.OnLeftSideDie.Subscribe(_ =>
        {
            StopBattleTheme();
        });
    }

    private void OnDisable()
    {
        _tween?.Kill();
        _summonCallback?.Dispose();
        _leftSideDieCallback?.Dispose();
    }

    public void PlayStartTheme()
    {
        FadeInTheme(_startSource);
    }

    public void SwitchToBattleTheme()
    {
        SwitchTheme(_startSource, _battleSource);
    }

    public void StopBattleTheme()
    {
        FadeOutTheme(_battleSource);
    }

    public Sequence FadeOutAllThemes()
    {
        _tween?.Kill();
        var seq = DOTween.Sequence()
            .Append(_battleSource.DOFade(0f, _fadeDuration))
            .Join(_startSource.DOFade(0f, _fadeDuration))
            .AppendCallback(() =>
            {
                _battleSource.Stop();
                _startSource.Stop();
            }).SetUpdate(true);
        _tween = seq;
        return seq;
    }

    private void FadeInTheme(AudioSource source)
    {
        source.volume = 0f;
        source.Play();
        
        _tween?.Kill();
        _tween = DOTween.Sequence()
            .Append(source.DOFade(1f, _fadeDuration))
            .SetUpdate(true);
    }   
    
    private void FadeOutTheme(AudioSource source)
    {
        source.volume = 1f;
        
        _tween?.Kill();
        _tween = DOTween.Sequence()
            .Append(source.DOFade(0f, _fadeDuration))
            .AppendCallback(source.Stop)
            .SetUpdate(true);
    }
    
    private void SwitchTheme(AudioSource from, AudioSource to)
    {
        from.volume = 1f;
        to.volume = 0f;
        
        _tween?.Kill();
        _tween = DOTween.Sequence()
            .AppendCallback(to.Play)
            .Join(from.DOFade(0f, _fadeDuration))
            .Join(to.DOFade(1f, _fadeDuration))
            .AppendCallback(from.Stop);
    }
}
