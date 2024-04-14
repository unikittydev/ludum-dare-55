using UnityEngine;
using Zenject;

namespace Game.Magic.Elements
{
	public class ElementRotationHandler : ITickable
	{
		[Inject] private LineRenderer _rotationLine;
		[Inject] private LayerMask _elementsInputLayer;
		[Inject] private Camera _camera;

		private MagicElementView _currentElement;
		private Vector2 _point;
		private bool _enabled;

		public void Enable() => _enabled = true;
		public void Disable() => _enabled = false;
		
		public void Tick()
		{
			if (!_enabled)
				return;

			if (Input.GetMouseButtonDown(0))
				StartRotation();
			else if (Input.GetMouseButton(0))
				ContinueRotation();
			else if (Input.GetMouseButtonUp(0))
				StopRotation();
		}

		private void StartRotation()
		{
			_point = _camera.ScreenToWorldPoint(Input.mousePosition);
			var collider = Physics2D.OverlapPoint(_point, _elementsInputLayer);
			if (collider == null)
				return;

			var element = collider.GetComponentInParent<MagicElementView>();
			if (!element.Model.InCircle.Value)
				return;

			_currentElement = element;
			_rotationLine.SetPosition(0, _currentElement.transform.position);
			_rotationLine.SetPosition(1, _point);
			_currentElement.StartRotating(_point);
			_rotationLine.gameObject.SetActive(true);
		}

		private void ContinueRotation()
		{
			if (_currentElement == null) return;

			_point = _camera.ScreenToWorldPoint(Input.mousePosition);
			_rotationLine.SetPosition(1, _point);
			_currentElement.ContinueRotating(_point);
		}

		private void StopRotation()
		{
			if (_currentElement == null) return;

			_currentElement = null;
			_rotationLine.gameObject.SetActive(false);
		}
	}
}