using UnityEngine;

namespace Game.Character
{
    public class AttackSwipeView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _ps;

        private void Start()
        {
            Emit(Vector3.one, 0f);
            Emit(-Vector3.one, 180f);
        }

        public void Emit(Vector3 position, float rotationY)
        {
            var emitParams = new ParticleSystem.EmitParams()
            {
                position = position,
                rotation3D = new Vector3(0f, rotationY, 0f),
            };
            _ps.Emit(emitParams, 1);
        }
    }

}