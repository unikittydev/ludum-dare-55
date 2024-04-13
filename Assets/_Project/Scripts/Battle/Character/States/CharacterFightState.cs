using System;
using System.Threading.Tasks;
using UniRx;

namespace Game.Battle.Character.States
{
	public class CharacterFightState : CharacterState
	{
		private CharacterModel _opponent;
		private CompositeDisposable _disposables = new();

		public CharacterFightState(CharacterStateMachine stateMachine, CharacterModel opponent) : base(stateMachine)
		{
			_opponent = opponent;
		}

		public override void Enter()
		{
			Observable.Interval(TimeSpan.FromSeconds(1 / _stateMachine.Model.AttackSpeed))
				.Subscribe(async _ =>
				{
					_stateMachine.Model.View.Animator.PlayAttack();
					await Task.Delay(TimeSpan.FromSeconds(_stateMachine.Model.View.AttackDelay));
					_opponent.TakeDamage(_stateMachine.Model.Damage);
				}).AddTo(_disposables);
			_stateMachine.Model.View.Animator.PlayMove(false);
		}

		public override void Exit()
		{
			_disposables.Dispose();
		}
	}
}