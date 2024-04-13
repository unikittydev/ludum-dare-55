
namespace Game.Character
{
    public class CharactersStateWalk : ICharacterState
    {
        public void Enter(CharacterStateMachine stateMachine)
        {
            stateMachine.CharacterFacade.SetAnimation<CharacterAnimationWalk>();
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
        }
    }
}