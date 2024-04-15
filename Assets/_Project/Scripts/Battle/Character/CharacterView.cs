using DG.Tweening;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
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

		[SerializeField] private SpriteRenderer _sprite, _shadow;
		[SerializeField] private int defaultSortOrder, topSortOrder;
		
		[SerializeField] private RectTransform _statsCanvas;
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
		[SerializeField] private GameObject _disableWhenDie;
		[Space]
		[SerializeField] private ParticleSystem _soul;
		[SerializeField] private GameObject _character;
		[SerializeField] private float _soulTransitionDuration;
		[SerializeField] private float _bigCharacterTransitionDuration;

		private CharacterModel _model;
		private Tween _soulTween;

		public RectTransform StatsCanvas => _statsCanvas;

		private void Awake()
		{
			if (!_soul)
				return;
			_soul.gameObject.SetActive(false);
		}

		private void OnDisable()
		{
			_soulTween?.Kill();
		}

		[Inject]
		private void Construct(CharacterModel model)
		{
			_model = model;
			_model.View = this;
			_attackSpeedText.text = _model.AttackSpeed.ToString("0.0");
			_damageText.text = _model.Damage.ToString("0.#");
			_slider.value = 1f * _model.Health.Value / _model.MaxHealth;
			_healthText.text = _model.Health.Value.ToString("0");
			_model.OnTakeDamage.Subscribe(_ => OnTakeDamage())
				.AddTo(this);
			_model.OnDie.Subscribe(_ => OnDie())
				.AddTo(this);
		}

		public Tween SetLikeSoul(Vector2 position)
		{
			_soulTween?.Kill();
			_soulTween = DOTween.Sequence()
				.Append(transform.DOMove(position, _soulTransitionDuration))
				.Join(transform.DOScale(0, _soulTransitionDuration))
				.AppendCallback(() =>
				{
					_sprite.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
					_shadow.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
					_sprite.sortingOrder = topSortOrder;
					_shadow.sortingOrder = topSortOrder - 1;
					_statsCanvas.gameObject.SetActive(false);
					_character.SetActive(false);
					_soul.gameObject.SetActive(true);
					_soul.Play();
				});
			return _soulTween;
		}

		public Tween SetLikeCharacter()
		{
			_soulTween?.Kill();
			_soulTween = DOTween.Sequence()
				.AppendCallback(() =>
				{
					_sprite.sortingOrder = defaultSortOrder;
					_shadow.sortingOrder = defaultSortOrder - 1;
					_statsCanvas.gameObject.SetActive(true);
					_soul.gameObject.SetActive(false);
					_character.SetActive(true);
				})
				.Append(transform.DOScale(1, _soulTransitionDuration));
			return _soulTween;
		}

		public Tween SetLikeBigCharacter(float scale)
		{
			_soulTween?.Kill();
			_soulTween = DOTween.Sequence()
				.AppendCallback(() =>
				{
					_statsCanvas.gameObject.SetActive(false);
					_soul.gameObject.SetActive(false);
					_character.SetActive(true);
				})
				.Append(transform.DOScale(scale, _bigCharacterTransitionDuration));
			return _soulTween;
		}

		private void OnTakeDamage()
		{
			_slider.value = 1f * _model.Health.Value / _model.MaxHealth;
			_healthText.text = _model.Health.Value.ToString("0");
			_animator.PlayDamage();
		}

		private void OnDie()
		{
			_disableWhenDie.SetActive(false);
			_animator.PlayDeath();
			Destroy(gameObject, _destroyDelay);
		}
	}
}