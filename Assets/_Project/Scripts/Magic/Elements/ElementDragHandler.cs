using Game.Magic.View;
using UnityEngine;
using Zenject;

namespace Game.Magic.Elements
{
	public class ElementDragHandler : ITickable
	{
		private LayerMask _elementsLayer;
		private LayerMask _slotsLayer;
		private Camera _camera;

		private MagicElementView _currentElement;
		private Vector2 _point;

		public ElementDragHandler(LayerMask elementsLayer, LayerMask slotsLayer, Camera camera)
		{
			_elementsLayer = elementsLayer;
			_slotsLayer = slotsLayer;
			_camera = camera;
		}

		public void Tick()
		{
			if (Input.GetMouseButtonDown(0))
				StartDrag();
			else if (Input.GetMouseButton(0))
				Drag();
			else if (Input.GetMouseButtonUp(0))
				Drop();
		}

		private void StartDrag()
		{
			_point = _camera.ScreenToWorldPoint(Input.mousePosition);
			var collider = Physics2D.OverlapPoint(_point, _elementsLayer);
			if (collider == null)
				return;

			var element = collider.GetComponent<MagicElementView>();
			if (element.Model.InCircle.Value)
				return;

			_currentElement = element;
		}

		private void Drag()
		{
			if (_currentElement == null) return;

			_point = _camera.ScreenToWorldPoint(Input.mousePosition);
			_currentElement.transform.position = Vector2.Lerp((Vector2)_currentElement.transform.position, _point, 0.1f);
		}

		private void Drop()
		{
			if (_currentElement == null) return;

			_point = _camera.ScreenToWorldPoint(Input.mousePosition);
			var collider = Physics2D.OverlapPoint(_point, _slotsLayer);
			if (collider == null)
			{
				_currentElement = null;
				return;
			}

			var slot = collider.GetComponent<MagicCircleSlotView>();
			if (slot.IsEmpty)
				slot.Place(_currentElement.Model);

			_currentElement = null;
		}
	}
}
