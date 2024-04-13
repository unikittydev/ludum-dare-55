using UnityEngine;

namespace Game.Battle.Character
{
    public class CharacterParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _psSwipe;
        [SerializeField] private ParticleSystem _psDamage;
        [SerializeField] private ParticleSystem _psFootsteps;

        public void EmitAttack() => _psSwipe.Play();
        public void EmitDamage() => _psDamage.Play();
        public void EmitFootsteps() => _psFootsteps.Play();
    }
}