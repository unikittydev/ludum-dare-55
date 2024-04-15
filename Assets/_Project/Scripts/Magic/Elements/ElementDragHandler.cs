using Game.Magic.View;
using UniOwl.Audio;
using UnityEngine;
using Zenject;

namespace Game.Magic.Elements
{
	public class ElementDragHandler : ITickable
	{
		private LayerMask _elementsInputLayer;
		private LayerMask _slotsInputLayer;
		private Camera _camera;

		private MagicElementView _currentElement;
		private Vector2 _point;
		private bool _enabled;

		private AudioCue _beginDragCue;
		private AudioCue _placeCue;
		
		public ElementDragHandler(LayerMask elementsLayer, LayerMask slotsLayer, Camera camera, ElementsInstaller.AudioData _audioData)
		{
			_elementsInputLayer = elementsLayer;
			_slotsInputLayer = slotsLayer;
			_camera = camera;
			_enabled = true;

			_beginDragCue = _audioData._beginDrag;
			_placeCue = _audioData._place;
		}

		public void Enable() => _enabled = true;
		public void Disable() => _enabled = false;

		public void Tick()
		{
			if (!_enabled) return;
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
			var collider = Physics2D.OverlapPoint(_point, _elementsInputLayer);
			if (collider == null)
				return;

			var element = collider.GetComponentInParent<MagicElementView>();
			if (element.Model.InCircle.Value)
				return;

			_currentElement = element;
			
			AudioSFXSystem.PlayCue2D(_beginDragCue);
		}

		private void Drag()
		{
			if (_currentElement == null) return;

			_point = _camera.ScreenToWorldPoint(Input.mousePosition);
			_currentElement.transform.position = 
				Vector2.Lerp((Vector2)_currentElement.transform.position, _point, Time.deltaTime * 15);
		}

		private void Drop()
		{
			if (_currentElement == null) return;

			_point = _camera.ScreenToWorldPoint(Input.mousePosition);
			var collider = Physics2D.OverlapPoint(_point, _slotsInputLayer);
			if (collider == null)
			{
				_currentElement = null;
				return;
			}

			var slot = collider.GetComponent<MagicCircleSlotView>();
			if (slot.IsEmpty)
			{
				slot.Place(_currentElement.Model);
				AudioSFXSystem.PlayCue2D(_placeCue);
			}

			_currentElement = null;
		}
	}
}
