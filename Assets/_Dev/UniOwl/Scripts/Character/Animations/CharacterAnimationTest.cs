using UnityEngine;

namespace Game.Character
{
    public class CharacterAnimationTest : MonoBehaviour
    {
        private static readonly int ATTACK = Animator.StringToHash("ATTACK");
        private static readonly int DAMAGE = Animator.StringToHash("DAMAGE");
        private static readonly int DEATH = Animator.StringToHash("DEATH");

        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterParticlesView characterParticlesView;

        [SerializeField] private Transform _attackPivot;
        [SerializeField] private Transform _impactPivot;
        [SerializeField] private Transform _footstepPivot;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                _animator.SetTrigger(ATTACK);
            if (Input.GetKeyDown(KeyCode.E))
                _animator.SetTrigger(DAMAGE);
            if (Input.GetKeyDown(KeyCode.Space))
                _animator.SetTrigger(DEATH);
        }

        public void AttackEvent(int value)
        {
            characterParticlesView.EmitSwipe(_attackPivot.position, transform.eulerAngles.y);
        }
        
        public void DamageEvent(int value)
        {
            characterParticlesView.EmitImpact(_impactPivot.position, 180f + transform.eulerAngles.y);
        }
        
        public void FootstepsEvent(float offsetX)
        {
            characterParticlesView.EmitFootsteps(_footstepPivot.position + new Vector3(offsetX, 0f, 0f), 180f + transform.eulerAngles.y);
        }
    }
}
