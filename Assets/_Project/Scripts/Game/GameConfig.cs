using Game.Battle.Character.Enemies;
using Game.Magic.Elements;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.Battle.Character.Allies;

namespace Game.Configs
{
	[CreateAssetMenu]
	public class GameConfig : ScriptableObject
	{
		public EnemiesConfig Enemies => _enemies;
		public ElementsConfig Elements => _elements;
		public MagicCircleConfig MagicCircle => _magicCircle;
		public AlliesConfig Allies => _allies;

		[Header("All Stats X is player score")]
		[SerializeField] private EnemiesConfig _enemies;
		[SerializeField] private ElementsConfig _elements;
		[SerializeField] private MagicCircleConfig _magicCircle;
		[SerializeField] private AlliesConfig _allies;
	}

	[System.Serializable]
	public class EnemiesConfig
	{
		[Serializable]
		public struct Condition
		{
			public int ScoreReached;
			public EnemyConfig Config;
		}

		public int SpawnAfterPlayerSummons => _spawnAfterPlayerSummons;

		public IReadOnlyList<EnemyConfig> Enemies => _enemies;
		public IReadOnlyList<Condition> ConditionalEnemies => _conditionalEnemies;

		public AnimationCurve SpawnRateCurve => _spawnRateCurve;
		public AnimationCurve HealthCurve => _healthCurve;
		public AnimationCurve DamageCurve => _damageCurve;
		public AnimationCurve AttackSpeedCurve => _attackSpeedCurve;
		public float HealthRandom => _healthRandom;
		public float DamageRandom => _damageRandom;
		public float AttackSpeedRandom => _attackSpeedRandom;

		[Min(1)]
		[SerializeField] private int _spawnAfterPlayerSummons = 1;
		[Space]
		[SerializeField] private AnimationCurve _spawnRateCurve;
		[SerializeField] private AnimationCurve _healthCurve;
		[SerializeField] private AnimationCurve _damageCurve;
		[SerializeField] private AnimationCurve _attackSpeedCurve;
		[SerializeField] private float _healthRandom;
		[SerializeField] private float _damageRandom;
		[SerializeField] private float _attackSpeedRandom;
		[Space]
		[SerializeField] private EnemyConfig[] _enemies;
		[SerializeField] private List<Condition> _conditionalEnemies;
	}

	[System.Serializable]
	public class AlliesConfig
	{
		public AnimationCurve BaseHealthMultiplierCurve => _baseHealthCurve;
		public AnimationCurve BaseDamageMultiplierCurve => _baseDamageCurve;
		public AnimationCurve BaseAttackSpeedMultiplierCurve => _baseAttackSpeedCurve;

		public IReadOnlyList<AllyConfig> Allies => _allies;

		[SerializeField] private AnimationCurve _baseHealthCurve;
		[SerializeField] private AnimationCurve _baseDamageCurve;
		[SerializeField] private AnimationCurve _baseAttackSpeedCurve;
		[Space]
		[SerializeField] private AllyConfig[] _allies;
	}

	[System.Serializable]
	public class ElementsConfig
	{
		public IReadOnlyList<MagicElementConfig> Elements => _elements;

		public AnimationCurve ArrowsColorsCountCurve => _arrowsColorsCountCurve;
		public AnimationCurve ArrowsCountCurve => _arrowsCountCurve;
		public float ArrowsColorsCountRandom => _arrowsColorsCountRandom;
		public float ArrowsCountRandom => _arrowsCountRandom;

		public float ArrowLength => _arrowLength;

		public AnimationCurve SpawnRateCurve => _spawnRateCurve;

		public IReadOnlyList<Color> ArrowsColors => _arrowsColors;

		[SerializeField] private MagicElementConfig[] _elements;
		[Space]
		[SerializeField] private AnimationCurve _arrowsColorsCountCurve;
		[SerializeField] private float _arrowsColorsCountRandom;
		[SerializeField] private AnimationCurve _arrowsCountCurve;
		[SerializeField] private float _arrowsCountRandom;
		[Space]
		[SerializeField] private Color[] _arrowsColors;
		[SerializeField] private float _arrowLength;
		[Space]
		[SerializeField] private AnimationCurve _spawnRateCurve;
	}

	[System.Serializable]
	public class MagicCircleConfig
	{
		public int SegmentsCount => _segmentsCount;
		public int SegmentsRotationCount => _segmentsRotationCount;

		public IReadOnlyList<OrbitConfig> Orbits => _orbits;

		public AnimationCurve OrbitsCountCurve => _orbitsCountCurve;
		public AnimationCurve SlotsCountCurve => _slotsCountCurve;
		public float OrbitsCountRandom => _orbitsCountRandom;
		public float SlotsCountRandom => _slotsCountRandom;

		[SerializeField] private int _segmentsCount = 8;
		[SerializeField] private int _segmentsRotationCount = 8;
		[SerializeField] private OrbitConfig[] _orbits;
		[Space]
		[SerializeField] private AnimationCurve _orbitsCountCurve;
		[SerializeField] private float _orbitsCountRandom;
		[SerializeField] private AnimationCurve _slotsCountCurve;
		[SerializeField] private float _slotsCountRandom;
	}

	[System.Serializable]
	public class OrbitConfig
	{
		public int SlotsCount => _slotsCount;
		public float Radius => _radius;

		[SerializeField] private int _slotsCount;
		[SerializeField] private float _radius;
	}
}