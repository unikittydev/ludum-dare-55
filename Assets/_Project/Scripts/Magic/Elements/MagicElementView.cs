using Game.Configs;
using Game.MathUtils;
using System.Collections.Generic;
using UniOwl.Audio;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Game.Magic.Elements
{
	public class MagicElementView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		public MagicElementModel Model => _model;
		public SpriteRenderer SpriteRenderer => _spriteRenderer;

		public IReadOnlyList<MagicArrowView> Arrows;

		[SerializeField] private SpriteRenderer _spriteRenderer;
		[SerializeField] private Material _initialMaterial;
		[SerializeField] private Material _inCircleMaterial;
		[SerializeField] private ElementTooltip _tooltip;
		[SerializeField] private GameObject _tooltipObj;
		[SerializeField] private AudioCue _rotateCue;
		
		[Inject] private MagicCircleConfig _config;
		[Inject] private MagicElementModel _model;

		private Vector2 _startRotationAxis;

		[Inject]
		private void Construct()
		{
			_tooltip.Set(_model);
			_spriteRenderer.sprite = _model.Config.RuneSprite;
			_spriteRenderer.material = _initialMaterial;
		}

		public void SetInCircle()
		{
			_spriteRenderer.material = _inCircleMaterial;
			foreach (var arrow in Arrows)
				arrow.SetInCircle();
		}

		internal void StartRotating(Vector2 point) =>
			_startRotationAxis = (point - (Vector2) transform.position).normalized;

		internal void ContinueRotating(Vector2 point)
		{
			var currentRotationAxis = (point - (Vector2) transform.position).normalized;
			var degreeStep = 360f / _config.SegmentsRotationCount;
			var angle = Vector2.SignedAngle(_startRotationAxis, currentRotationAxis);
			var signedDegree = Mathf.Sign(angle) * degreeStep;

			if (Mathf.Abs(angle) > degreeStep)
			{
				_startRotationAxis = _startRotationAxis.GetRotated(-signedDegree);
				_model.Rotation.Value += signedDegree;
				transform.eulerAngles = Vector3.forward * _model.Rotation.Value;
				
				AudioSFXSystem.PlayCue2D(_rotateCue);
			}
		}

		public void OnPointerEnter(PointerEventData eventData) =>
			_tooltipObj.SetActive(true);
		
		public void OnPointerExit(PointerEventData eventData) =>
			_tooltipObj?.SetActive(false);
	}
}