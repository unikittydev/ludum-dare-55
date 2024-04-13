
namespace Game.Character
{
    public interface ICharacterState
    {
        public void Enter(CharacterStateMachine stateMachine);
        public void Update();
        public void Exit();
    }
}
