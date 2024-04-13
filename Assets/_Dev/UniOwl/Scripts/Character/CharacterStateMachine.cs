
namespace Game.Character
{
    public class CharacterStateMachine
    {
        private ICharacterState _currentState;

        public ICharacterState CurrentState => _currentState;

        private CharacterFacade _characterFacade;
        public CharacterFacade CharacterFacade => _characterFacade;

        public CharacterStateMachine(CharacterFacade facade)
        {
            _characterFacade = facade;
        }
        
        public void SetState(ICharacterState state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState?.Enter(this);
        }
    }
}