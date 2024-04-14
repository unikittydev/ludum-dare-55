using Game.Configs;
using Game.Summoning;
using System;
using UniRx;
using Zenject;

namespace Game.Battle.Character.Enemies
{
	public class EnemiesSpawnerBootstrap : IInitializable, IDisposable
	{
		[Inject] private EnemiesConfig _config;
		[Inject] private SummonProvider _summonProvider;
		[Inject] private EnemiesSpawner _spawner;

		private int _count;
		private CompositeDisposable _disposables;

		public void Initialize()
		{
			_disposables = new();
			_summonProvider.OnSummon
				.Subscribe(_ =>
				{
					_count++;
					if (_count >= _config.SpawnAfterPlayerSummons)
					{
						_spawner.Start();
						_disposables.Dispose();
					}
				}).AddTo(_disposables);
		}
		
		public void Dispose()
		{
			_disposables?.Dispose();
		}
	}
}