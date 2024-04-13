using UnityEngine;

namespace Game.Character
{
    public class CharacterAnimationTest : MonoBehaviour
    {
        private static readonly int ATTACK = Animator.StringToHash("ATTACK");
        private static readonly int DAMAGE = Animator.StringToHash("DAMAGE");
        private static readonly int DEATH = Animator.StringToHash("DEATH");

        [SerializeField] private Animator _animator;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                _animator.SetTrigger(ATTACK);
            if (Input.GetKeyDown(KeyCode.E))
                _animator.SetTrigger(DAMAGE);
            if (Input.GetKeyDown(KeyCode.Space))
                _animator.SetTrigger(DEATH);
        }
    }
}
