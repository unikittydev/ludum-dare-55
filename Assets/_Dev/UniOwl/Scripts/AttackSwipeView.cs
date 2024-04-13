using UnityEngine;

namespace Game.Character
{
    public class AttackSwipeView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _psSwipe;
        [SerializeField] private ParticleSystem _psImpact;

        public void Emit(Vector3 position, float rotationY)
        {
            var emitParamsSwipe = new ParticleSystem.EmitParams()
            {
                position = position,
                rotation3D = new Vector3(0f, rotationY, 0f),
            };
            _psSwipe.Emit(emitParamsSwipe, 1);

            var shape = _psImpact.shape;
            shape.position = position;
            shape.rotation = new Vector3(rotationY, 90f + Mathf.Lerp(-30f, 30f, rotationY / 180f), 0f);
            
            var emitParamsImpact = new ParticleSystem.EmitParams();
            _psImpact.Emit(emitParamsImpact, 30);
        }
    }

}