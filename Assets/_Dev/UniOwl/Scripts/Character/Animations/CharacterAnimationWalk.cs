using DG.Tweening;
using UnityEngine;

namespace Game.Character
{
    public class CharacterAnimationWalk : ICharacterAnimation
    {
        private float _stretchSpeed = .18f;
        private Vector3 _stretchAmplitudeDown = new Vector3(1.1f, 0.9f, 0f);
        private Vector3 _stretchAmplitudeUp = new Vector3(0.9f, 1.1f, 0f);
        private float _rotateAngle = 5f;

        private Sequence _sequence;

        public void Play(CharacterFacade character)
        {
            _sequence = DOTween.Sequence()
                .Append(character.SpriteRenderer.transform.DOScale(_stretchAmplitudeDown, _stretchSpeed))
                .Join(character.SpriteRenderer.transform.DOLocalRotate(new Vector3(0f, 0f, -_rotateAngle), _stretchSpeed))
                .Append(character.SpriteRenderer.transform.DOScale(Vector3.one, _stretchSpeed))
                .Join(character.SpriteRenderer.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), _stretchSpeed))
                .Append(character.SpriteRenderer.transform.DOScale(_stretchAmplitudeUp, _stretchSpeed))
                .Join(character.SpriteRenderer.transform.DOLocalRotate(new Vector3(0f, 0f, _rotateAngle), _stretchSpeed))
                .Append(character.SpriteRenderer.transform.DOScale(Vector3.one, _stretchSpeed))
                .Join(character.SpriteRenderer.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), _stretchSpeed))
                .SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        }

        public void Stop()
        {
            _sequence.Kill();
        }
    }
}