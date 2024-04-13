using UnityEngine;

namespace Game.Character
{
    public class CharacterFacade : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private CharacterStateMachine _stateMachine;

        private CharacterModel _characterModel;
        
        private ICharacterAnimation _currentAnimation;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public CharacterModel CharacterModel => _characterModel;
        
        private void OnEnable()
        {
            _stateMachine = new CharacterStateMachine(this);
            _stateMachine.SetState(new CharactersStateWalk());
        }

        private void Update()
        {
            _stateMachine.CurrentState.Update();
        }

        public void SetAnimation<TAnimation>() where TAnimation : ICharacterAnimation, new()
        {
            _currentAnimation = new TAnimation();
            _currentAnimation.Play(this);
        }
    }
}
