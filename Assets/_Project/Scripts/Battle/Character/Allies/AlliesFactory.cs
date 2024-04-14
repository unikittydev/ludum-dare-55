using Game.Configs;
using Game.Magic.Elements;
using Game.Scoring;
using Zenject;

namespace Game.Battle.Character.Allies
{
	public class AlliesFactory
	{
		[Inject] private Battleground _battle;
		[Inject] private AlliesConfig _allies;
		[Inject] private CharacterFactory _factory;
		[Inject] private ScoreService _score;

		public void Create(MagicElementModel[] elements)
		{
			int hp = 0;
			int dmg = 0;
			float atkSpeed = 0;
			foreach (var el in elements)
			{
				foreach (var influence in el.Config.Influences)
				{
					if (influence.Parameter == EElementParam.Health)
						hp += (int) influence.Value;
					else if (influence.Parameter == EElementParam.Damage)
						dmg += (int) influence.Value;
					else
						atkSpeed += influence.Value;
				}
			}

			foreach (var cfg in _allies.Allies)
			{
				if (_score.Score.Value > cfg.Condition.ScoreMin &&
					cfg.Condition.HealthByCircleRange.x <= hp &&
					cfg.Condition.HealthByCircleRange.y >= hp &&
					cfg.Condition.DamageByCircleRange.x <= dmg &&
					cfg.Condition.DamageByCircleRange.y >= dmg &&
					cfg.Condition.AttackSpeedByCircleRange.x <= atkSpeed &&
					cfg.Condition.AttackSpeedByCircleRange.y >= atkSpeed)
				{
					Spawn(cfg, hp, dmg, atkSpeed);
					return;
				}
			}

			Spawn(_allies.Allies[0], hp, dmg, atkSpeed);
		}

		private void Spawn(AllyConfig config, int hp, int dmg, float atkSpeed)
		{
			hp += (int) (config.BaseHealth * _allies.BaseHealthMultiplierCurve.Evaluate(_score.Score.Value));
			dmg += (int) (config.BaseDamage * _allies.BaseDamageMultiplierCurve.Evaluate(_score.Score.Value));
			atkSpeed += config.BaseAttackSpeed * _allies.BaseAttackSpeedMultiplierCurve.Evaluate(_score.Score.Value);
			_battle.SendLeftSide(_factory.Create(config.Prefab, hp, dmg, atkSpeed));
		}
	}
}