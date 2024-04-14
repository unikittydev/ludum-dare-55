using Game.Battle;
using Game.Summoning;
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Scoring
{
	public class ScoreProvider : IInitializable, IDisposable
	{
		[Serializable]
		public struct Config 
		{
			public AnimationCurve ScoreByLinksCountCurve;
			public AnimationCurve ScoreByKillCurve;
		}

		[Inject] private ScoreService _score;
		[Inject] private Config _config;
		[Inject] private SummonProvider _summon;
		[Inject] private Battleground _battle;

		private CompositeDisposable _disposables;

		public void Initialize()
		{
			_summon.OnSummon.Subscribe(_ =>
			{
				var add = _config.ScoreByLinksCountCurve.Evaluate(_score.Score.Value);
				_score.IncreaseScore((int) add);
			}).AddTo(_disposables);
			_battle.OnRightSideKilled.Subscribe(enemy =>
			{
				var score = _config.ScoreByKillCurve.Evaluate(_score.Score.Value) * enemy.ScoreByKill;
				_score.IncreaseScore((int) score);
			}).AddTo(_disposables);
		}

		public void Dispose() =>
			_disposables.Dispose();
	}
}