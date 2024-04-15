using UniRx;
using UnityEngine;

namespace Game.Battle.Character
{
	public class CharacterModel
	{
		public int ScoreByKill = 0;

		public CharacterStateMachine StateMachine { get; set; }
		public CharacterView View { get; set; }

		public ReactiveCommand OnDie = new();
		public ReactiveCommand OnTakeDamage = new();

		public IReadOnlyReactiveProperty<float> Health => _health;
		public float MaxHealth => _maxHealth;

		public float Damage => _damage;
		public float AttackSpeed => _attackSpeed;

		private ReactiveProperty<float> _health;
		private float _maxHealth;
		private float _damage;
		private float _attackSpeed;

		public CharacterModel(float health, float damage, float attackSpeed)
		{
			_health = new ReactiveProperty<float>(health);
			_maxHealth = health;
			_damage = damage;
			_attackSpeed = attackSpeed;
		}

		public void TakeDamage(float damage, float speed)
		{
			View?.Animator.SetDamageSpeed(speed);
			_health.Value = Mathf.Max(0, _health.Value - damage);
			if (_health.Value <= 0)
				OnDie.Execute();
			else
				OnTakeDamage.Execute();
		}
	}
}