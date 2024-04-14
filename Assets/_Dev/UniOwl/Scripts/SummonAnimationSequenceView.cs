using DG.Tweening;
using Game.Magic.Elements;
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
        private static readonly int _THRESHOLD = Shader.PropertyToID("_Threshold");
        private static readonly int _CHANNELS_OFFSET = Shader.PropertyToID("_Channels_Offset");
        
        [Header("Components")]
        [SerializeField] private ParticleSystem _psSummon;
        [SerializeField] private Animator _lineAnimator;
        
        [Header("Aberration")]
        [SerializeField] private Material _ppAberrationMaterial;
        [SerializeField] private Vector3 _aberrationOffset;
        [SerializeField] private float _aberrationDuration = .25f;
        
        [Header("Camera Shaking")]
        [SerializeField] private Transform _camera;
        [SerializeField] private float _cameraShakeDuration = 1f;
        [SerializeField] private float _cameraShakeStrength = 1f;

        [Header("Delay")]
        [SerializeField] private float _delay = 2f;
        
        [Header("Emission")]
        [SerializeField] private Material[] _materials;
        [SerializeField, ColorUsage(false, true)] private Color _fromEmission;
        [SerializeField, ColorUsage(false, true)] private Color _toEmission;
        [SerializeField] private float _emissionFadeDuration = .1f;
        [SerializeField] private float _emissionDuration = .5f;
        
        [Header("Dissolve")]
        [SerializeField] private float _dissolveDuration = .5f;

        [Inject] private MagicCircleFactory _factory;
        [Inject] private SummonProvider _provider;
		[Inject] private ElementDragHandler _drag;
		[Inject] private ElementRotationHandler _rotation;
		[Inject] private MagicCircleFactory _circleFactory;

        private Tween _tween;

		private void OnDisable()
        {
            UpdateEmissionColor(_fromEmission);
            UpdateDissolveFactor(0f);
            _tween?.Kill();
        }
        
        public void Summon()
        {
            _tween = DOTween.Sequence()
                // Camera shaking
                .Join(_camera.DOShakePosition(_cameraShakeDuration, _cameraShakeStrength))
                // Prepare
                .JoinCallback(_psSummon.Play)
                .JoinCallback(() => _lineAnimator.Play(PLAY))
                .JoinCallback(_drag.Disable)
                .JoinCallback(_rotation.Disable)
                .JoinCallback(() =>
                {
					foreach (var or in _circleFactory.CurrentCircle.Value.Model.Orbits)
						foreach (var sl in or.Slots)
							if (sl.Element.Value != null)
								foreach (var ar in sl.Element.Value.Arrows)
									ar.View.gameObject.SetActive(false);
				})
                .AppendInterval(_delay)
                // Emission on
                .Append(DOVirtual.Color(_fromEmission, _toEmission, _emissionFadeDuration, UpdateEmissionColor))
                // Aberration on
                .Join(_ppAberrationMaterial.DOFloat(1f, _THRESHOLD, _emissionFadeDuration))
                .Join(_ppAberrationMaterial.DOVector(new Vector4(_aberrationOffset.x, 0f, 0f, 0f), _CHANNELS_OFFSET, _aberrationDuration))
                .Join(_ppAberrationMaterial.DOVector(new Vector4(_aberrationOffset.x, _aberrationOffset.y, 0f, 0f), _CHANNELS_OFFSET, _aberrationDuration))
                .Join(_ppAberrationMaterial.DOVector(new Vector4(_aberrationOffset.x, _aberrationOffset.y, _aberrationOffset.z, 0f), _CHANNELS_OFFSET, _aberrationDuration))
                // Summon
                .JoinCallback(_provider.Summon)
                .AppendInterval(_emissionDuration)
                // Aberration off
                .Append(_ppAberrationMaterial.DOFloat(0f, _THRESHOLD, _aberrationDuration))
                .Join(_ppAberrationMaterial.DOVector(Vector4.zero, _CHANNELS_OFFSET, _aberrationDuration))
                // Emission off
                .Join(DOVirtual.Color(_toEmission, _fromEmission, _emissionFadeDuration, UpdateEmissionColor))
                // Remake circle
                .Append(DOVirtual.Float(0f, 1f, _dissolveDuration, UpdateDissolveFactor))
                .AppendCallback(_factory.CreateNew)
                .Append(DOVirtual.Float(1f, 0f, _dissolveDuration, UpdateDissolveFactor))
                // Cleanup
                .AppendCallback(_drag.Enable)
                .JoinCallback(_rotation.Enable)
                .JoinCallback(() => _lineAnimator.Play(STOP));
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
