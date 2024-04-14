using DG.Tweening;
using Game.Summoning;
using UnityEngine;
using Zenject;

namespace Game.Magic
{
    public class SummonAnimationSequenceView : MonoBehaviour
    {
        private static readonly int PLAY = Animator.StringToHash("PLAY");
        private static readonly int STOP = Animator.StringToHash("STOP");

        private static readonly int _EMISSION = Shader.PropertyToID("_Emission");
        private static readonly int _DISSOLVE_FACTOR = Shader.PropertyToID("_Dissolve_Factor");
        
        [SerializeField] private ParticleSystem _psSummon;
        [SerializeField] private Animator _lineAnimator;

        [SerializeField] private Material[] _materials;

        [SerializeField] private ParticleSystem _psSmoke;
        
        [SerializeField] private float _delay = 2f;
        
        [SerializeField, ColorUsage(false, true)] private Color _fromEmission;
        [SerializeField, ColorUsage(false, true)] private Color _toEmission;
        [SerializeField] private float _emissionFadeDuration = .1f;
        [SerializeField] private float _emissionDuration = .5f;
        [SerializeField] private float _dissolveDuration = .5f;
        [SerializeField] private float _respawnDelay = 2f;

        [Inject] private MagicCircleFactory _factory;
        [Inject] private SummonProvider _provider;
        
        private void OnDisable()
        {
            UpdateEmissionColor(_fromEmission);
            UpdateDissolveFactor(0f);
        }
        
        public void Summon()
        {
            _psSummon.Play();
            _lineAnimator.Play(PLAY);

            DOTween.Sequence()
                .AppendInterval(_delay)
                .Append(DOVirtual.Color(_fromEmission, _toEmission, _emissionFadeDuration, UpdateEmissionColor))
                .JoinCallback(_provider.Summon)
                .AppendInterval(_emissionDuration)
                .Append(DOVirtual.Color(_toEmission, _fromEmission, _emissionFadeDuration, UpdateEmissionColor))
                .Append(DOVirtual.Float(0f, 1f, _dissolveDuration, UpdateDissolveFactor))
                .AppendCallback(_factory.CreateNew)
                .Append(DOVirtual.Float(1f, 0f, _dissolveDuration, UpdateDissolveFactor))
                .AppendCallback(() => _lineAnimator.Play(STOP));
        }

        private void UpdateEmissionColor(Color value)
        {
            foreach (var material in _materials)
                material.SetColor(_EMISSION, value);
        }

        private void UpdateDissolveFactor(float value)
        {
            foreach (var material in _materials)
                material.SetFloat(_DISSOLVE_FACTOR, value);
        }
    }
}