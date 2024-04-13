using UniRx;
using UnityEngine;

namespace Game.Battle.Character
{
	public class CharacterModel
	{
		public ReactiveCommand OnDie = new();

		public IReadOnlyReactiveProperty<int> Health => _health;
		public int MaxHealth => _maxHealth;

		public int Damage => _damage;
		public float AttackSpeed => _attackSpeed;

		private ReactiveProperty<int> _health;
		private int _maxHealth;
		private int _damage;
		private float _attackSpeed;

		public CharacterModel(int health, int damage, int attackSpeed)
		{
			_health = new ReactiveProperty<int>(health);
			_maxHealth = health;
			_damage = damage;
			_attackSpeed = attackSpeed;
		}

		public void TakeDamage(int damage)
		{
			_health.Value = Mathf.Max(0, _health.Value - damage);
			if (_health.Value <= 0)
				OnDie.Execute();
		}
	}
}