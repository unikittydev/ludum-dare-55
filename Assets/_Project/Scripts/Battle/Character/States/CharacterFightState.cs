using DG.Tweening;
using System;
using UniRx;

namespace Game.Battle.Character.States
{
	public class CharacterFightState : CharacterState
	{
		private CharacterModel _opponent;
		private CompositeDisposable _disposables = new();

		private Tween _attackTween;

		public CharacterFightState(CharacterStateMachine stateMachine, CharacterModel opponent) : base(stateMachine)
		{
			_opponent = opponent;
		}

		public override void Enter()
		{
			Observable.Interval(TimeSpan.FromSeconds(1 / _stateMachine.Model.AttackSpeed))
				.Subscribe(_ =>
				{
					var attackSpeed = _stateMachine.Model.AttackSpeed;
					_stateMachine.Model.View.Animator.SetAttackSpeed(attackSpeed);
					_attackTween = DOTween.Sequence()
						.AppendCallback(_stateMachine.Model.View.Animator.PlayAttack)
						.AppendInterval(_stateMachine.Model.View.AttackDelay / attackSpeed)
						.AppendCallback(() => _opponent.TakeDamage(_stateMachine.Model.Damage, attackSpeed));
				}).AddTo(_disposables);
			_stateMachine.Model.View.Animator.PlayMove(false);
		}

		public override void Exit()
		{
			_attackTween?.Kill();
			_disposables.Dispose();
		}
	}
}