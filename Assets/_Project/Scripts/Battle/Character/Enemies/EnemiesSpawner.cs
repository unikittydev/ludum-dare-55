using Game.Configs;
using Game.Scoring;
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Battle.Character.Enemies
{
	public class EnemiesSpawner : IDisposable
	{
		[Inject] private EnemiesFactory _factory;
		[Inject] private GameDifficultyService _difficulty;
		[Inject] private ScoreService _score;

		private int _lastScore;
		private float _time;

		private CompositeDisposable _disposables;

		public void Start()
		{
			_disposables = new();
			_score.Score.Subscribe((s) =>
			{
				foreach (var c in _difficulty.ConditionalEnemies)
					if (c.ScoreReached > _lastScore &&
						c.ScoreReached < s)
						_factory.Create(c.Config);
			}).AddTo(_disposables);
			Observable.EveryUpdate().Subscribe(_ =>
			{
				_time += Time.deltaTime;
				if (_time >= 1f / _difficulty.GetEnemiesSpawnRate())
				{
					_time = 0;
					_factory.Create(_difficulty.GetRandomEnemy());
				}
			}).AddTo(_disposables);
		}

		public void Stop() =>
			_disposables?.Dispose();

		public void Dispose() =>
			Stop();
	}
}