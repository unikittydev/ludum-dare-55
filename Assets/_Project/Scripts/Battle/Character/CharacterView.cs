using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Battle.Character
{
	public class CharacterView : MonoBehaviour
	{
		public CharacterAnimator Animator => _animator;
		public float MoveSpeed => _moveSpeed;
		public float AttackDelay => _attackDelay;
		public float DestroyDelay => _destroyDelay;

		[SerializeField] private Slider _slider;
		[SerializeField] private TMP_Text _healthText;
		[SerializeField] private TMP_Text _attackSpeedText;
		[SerializeField] private TMP_Text _damageText;
		[Space]
		[SerializeField] private CharacterAnimator _animator;
		[SerializeField] private float _moveSpeed;
		[Space]
		[SerializeField] private float _destroyDelay;
		[SerializeField] private float _attackDelay;

		private CharacterModel _model;

		[Inject]
		private void Construct(CharacterModel model)
		{
			_model = model;
			_model.View = this;
			_attackSpeedText.text = _model.AttackSpeed.ToString("0.0");
			_damageText.text = _model.Damage.ToString();
			_slider.value = 1f * _model.Health.Value / _model.MaxHealth;
			_healthText.text = _model.Health.Value.ToString();
			_model.OnTakeDamage.Subscribe(_ => OnTakeDamage())
				.AddTo(this);
			_model.OnDie.Subscribe(_ => OnDie())
				.AddTo(this);
		}

		private void OnTakeDamage()
		{
			_slider.value = 1f * _model.Health.Value / _model.MaxHealth;
			_healthText.text = _model.Health.Value.ToString();
			_animator.PlayDamage();
		}

		private void OnDie()
		{
			_slider.gameObject.SetActive(false);
			_healthText.gameObject.SetActive(false);
			_attackSpeedText.gameObject.SetActive(false);
			_damageText.gameObject.SetActive(false);
			_animator.PlayDeath();
			Destroy(gameObject, _destroyDelay);
		}
	}
}