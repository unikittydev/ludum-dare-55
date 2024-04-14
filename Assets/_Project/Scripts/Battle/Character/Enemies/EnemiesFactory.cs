using Game.Configs;
using Zenject;

namespace Game.Battle.Character.Enemies
{
	public class EnemiesFactory
	{
		[Inject] private Battleground _battle;
		[Inject] private GameDifficultyService _difficulty;
		[Inject] private CharacterFactory _factory;

		public void Create(EnemyConfig config)
		{
			var atkSpeed = _difficulty.GetEnemyAttackSpeed();
			var hp = _difficulty.GetEnemyHealth();
			var dmg = _difficulty.GetEnemyDamage();
			var enemy = _factory.Create(config.Prefab, 
				(int) (hp * config.HealthMultiplier), 
				(int) (dmg * config.DamageMultiplier), 
				atkSpeed * config.AttackSpeedMultiplier);
			_battle.SendRightSide(enemy);
		}
	}
}