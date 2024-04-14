using Game.Configs;
using Game.MathUtils;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Magic.Elements
{
	public class MagicElementView : MonoBehaviour
	{
		public MagicElementModel Model => _model;
		public SpriteRenderer SpriteRenderer => _spriteRenderer;

		public IReadOnlyList<MagicArrowView> Arrows;

		[SerializeField] private SpriteRenderer _spriteRenderer;
		[SerializeField] private Material _initialMaterial;
		[SerializeField] private Material _inCircleMaterial;

		[Inject] private MagicCircleConfig _config;
		[Inject] private MagicElementModel _model;

		private Vector2 _startRotationAxis;

		[Inject]
		private void Construct()
		{
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
			var degreeStep = 360f / _config.SegmentsCount;
			var angle = Vector2.SignedAngle(_startRotationAxis, currentRotationAxis);
			var signedDegree = Mathf.Sign(angle) * degreeStep;

			if (Mathf.Abs(angle) > degreeStep)
			{
				_startRotationAxis = _startRotationAxis.GetRotated(-signedDegree);
				_model.Rotation.Value += signedDegree;
				transform.eulerAngles = Vector3.forward * _model.Rotation.Value;
			}
		}
	}
}