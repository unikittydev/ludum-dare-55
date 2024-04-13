namespace Game.Battle.Character.States
{
	public abstract class CharacterState
	{
		protected CharacterStateMachine _stateMachine;
		public CharacterState(CharacterStateMachine stateMachine) => _stateMachine = stateMachine;

		public abstract void Enter();
		public abstract void Exit();
	}
}