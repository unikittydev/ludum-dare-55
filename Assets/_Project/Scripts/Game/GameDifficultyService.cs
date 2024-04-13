using Game.Magic.Elements;
using Game.MathUtils;
using Game.Scoring;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Game.Configs
{
	public class GameDifficultyService
	{
		[Inject] private EnemiesConfig _enemiesConfig;
		[Inject] private ElementsConfig _elementsConfig;
		[Inject] private MagicCircleConfig _magicCircleConfig;
		[Inject] private AlliesConfig _alliesConfig;
		[Inject] private ScoreService _score;

		public MagicElementConfig GetRandomRune() =>
			_elementsConfig.Elements[Random.Range(0, _elementsConfig.Elements.Count)];
		public float GetEnemiesSpawnRate() =>
			_enemiesConfig.SpawnRateCurve.Evaluate(_score.Score.Value);

		public float GetElementsSpawnRate() =>
			_elementsConfig.SpawnRateCurve.Evaluate(_score.Score.Value);

		public int GetEnemyHealth() =>
			Randomize(_enemiesConfig.HealthCurve.Evaluate(_score.Score.Value),
				_enemiesConfig.HealthRandom, 1, int.MaxValue);
		public int GetEnemyDamage() =>
			Randomize(_enemiesConfig.DamageCurve.Evaluate(_score.Score.Value),
				_enemiesConfig.DamageRandom, 1, int.MaxValue);
		public float GetEnemyAttackSpeed() =>
			Randomize(_enemiesConfig.AttackSpeedCurve.Evaluate(_score.Score.Value),
				_enemiesConfig.AttackSpeedRandom, 0.1f);


		public MagicArrowModel[] GetRandomArrows()
		{
			var colorsCount = Randomize(_elementsConfig.ArrowsColorsCountCurve.Evaluate(_score.Score.Value),
				_elementsConfig.ArrowsColorsCountRandom, 1, _elementsConfig.ArrowsColors.Count);
			var count = Randomize(_elementsConfig.ArrowsCountCurve.Evaluate(_score.Score.Value),
				_elementsConfig.ArrowsCountRandom, 1, int.MaxValue);

			var allArrows = new Vector2[_magicCircleConfig.SegmentsCount];
			for (int i = 0; i < allArrows.Length; i++)
				allArrows[i] = Vector2.up.GetRotated(i * 360f / _magicCircleConfig.SegmentsCount);
			
			allArrows.Shuffle();

			return allArrows.Take(count).Select(axis =>
				new MagicArrowModel(_elementsConfig.ArrowsColors[Random.Range(0, colorsCount)], axis)).ToArray();
		}

		public OrbitConfig[] GetOrbits()
		{
			var count = Randomize(_magicCircleConfig.OrbitsCountCurve.Evaluate(_score.Score.Value),
				_magicCircleConfig.OrbitsCountRandom, 2, _magicCircleConfig.Orbits.Count);
			return _magicCircleConfig.Orbits.Take(count).ToArray();
		}

		public int GetSlotsCount() =>
			Randomize(_magicCircleConfig.SlotsCountCurve.Evaluate(_score.Score.Value),
				_magicCircleConfig.SlotsCountRandom, 2, int.MaxValue);

		private int Randomize(float evaluated, float random, int clampMin, int clampMax)
		{
			var r = Random.Range(-random, random);
			return (int) Mathf.Clamp(evaluated + r, clampMin, clampMax);
		}
		private float Randomize(float evaluated, float random, float clampMin)
		{
			var r = Random.Range(-random, random);
			return Mathf.Max(evaluated + r, clampMin);
		}
	}
}