
namespace Game.Character
{
    public interface ICharacterAnimation
    {
        public void Play(CharacterFacade character);
        public void Stop();
    }
}