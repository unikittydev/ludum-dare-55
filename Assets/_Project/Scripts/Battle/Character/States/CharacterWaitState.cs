namespace Game.Battle.Character.States
{
	public class CharacterWaitState : CharacterState
	{
		public CharacterWaitState(CharacterStateMachine stateMachine) : base(stateMachine) { }

		public override void Enter() =>
			_stateMachine.Model.View.Animator.PlayMove(false);
		public override void Exit() { }
	}
}