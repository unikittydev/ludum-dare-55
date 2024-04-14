using DG.Tweening;
using UnityEngine;

namespace Game.Battle.Character.States
{
	public class CharacterWalkState : CharacterState
	{
		private Vector2 _point;
		private Tween _tween;

		public CharacterWalkState(CharacterStateMachine stateMachine, Vector2 point) : base(stateMachine)
		{
			_point = point;
		}

		public override void Enter()
		{
			_stateMachine.Model.View.Animator.PlayMove(true);
			var distance = Vector2.Distance(_stateMachine.Model.View.transform.position, _point);
			var duration = distance / _stateMachine.Model.View.MoveSpeed;
			_tween = _stateMachine.Model.View.transform.DOMove(_point, duration)
				.OnComplete(() => _stateMachine.SwitchState(new CharacterReadyState(_stateMachine)));
			Debug.Log("Walk");
		}

		public override void Exit()
		{
			_tween?.Kill();
		}
	}
}