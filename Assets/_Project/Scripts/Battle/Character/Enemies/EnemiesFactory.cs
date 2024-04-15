using Game.Configs;
using Game.Scoring;
using Zenject;

namespace Game.Battle.Character.Enemies
{
	public class EnemiesFactory
	{
		[Inject] private Battleground _battle;
		[Inject] private GameDifficultyService _difficulty;
		[Inject] private CharacterFactory _factory;
		[Inject] private ScoreService _score;

		public void Create(EnemyConfig config)
		{
			var atkSpeed = _difficulty.GetEnemyAttackSpeed();
			var hp = _difficulty.GetEnemyHealth();
			var dmg = _difficulty.GetEnemyDamage();
			var enemy = _factory.Create(config.Prefab, 
				hp * config.HealthMultiplier, 
				dmg * config.DamageMultiplier, 
				atkSpeed * config.AttackSpeedMultiplier);
			enemy.ScoreByKill = (int) config.ScoreByKillCurve.Evaluate(_score.Score.Value); 
			_battle.SendRightSide(enemy);
		}
	}
}