using UnityEngine;

namespace Game.Character
{
    public class CharacterParticlesView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _psSwipe;
        [SerializeField] private ParticleSystem _psImpact;
        [SerializeField] private ParticleSystem _psFootsteps;

        [SerializeField] private float _impactAngle = 30f;
        [SerializeField] private int _impactParticleCount = 20;
        
        [SerializeField] private float _footstepsAngle = 10f;
        [SerializeField] private int _footstepsParticleCount = 5;
        
        public void EmitSwipe(Vector3 position, float rotationY)
        {
            var emitParams = new ParticleSystem.EmitParams()
            {
                position = position,
                rotation3D = new Vector3(0f, rotationY, 0f),
            };
            _psSwipe.Emit(emitParams, 1);
        }

        public void EmitImpact(Vector3 position, float rotationY)
        {
            _psImpact.transform.position = position;
            var shape = _psImpact.shape;
            shape.rotation = new Vector3(rotationY, 90f + Mathf.Lerp(-_impactAngle, _impactAngle, rotationY / 180f), 0f);
            
            var emitParams = new ParticleSystem.EmitParams();
            _psImpact.Emit(emitParams, _impactParticleCount);
        }

        public void EmitFootsteps(Vector3 position, float rotationY)
        {
            _psFootsteps.transform.position = position;
            var shape = _psFootsteps.shape;
            shape.rotation = new Vector3(rotationY, 90f + Mathf.Lerp(-_footstepsAngle, _footstepsAngle, rotationY / 180f), 0f);
            
            var emitParams = new ParticleSystem.EmitParams();
            _psFootsteps.Emit(emitParams, _footstepsParticleCount);
        }
    }
}