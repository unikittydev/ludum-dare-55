using DG.Tweening;
using Game.Configs;
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Magic.Elements
{
	public class MagicElementsSpawner : MonoBehaviour
	{
		[SerializeField] private Vector2 _spawnViewportPoint;
		[SerializeField] private Vector2[] _elementsViewportPoints;
		[SerializeField] private Camera _camera;

		[Inject] private GameDifficultyService _difficulty;
		[Inject] private MagicElementsFactory _factory;

		private float _currentTime;
		private MagicElementView[] _elements;
		private IDisposable[] _disposables;

		private void Awake() 
		{
			_elements = new MagicElementView[_elementsViewportPoints.Length];
			_disposables = new IDisposable[_elementsViewportPoints.Length];
		} 

		private void Update()
		{
			_currentTime += Time.deltaTime;
			if (_currentTime >= 1 / _difficulty.GetElementsSpawnRate())
				Spawn();
		}

		private void OnDestroy()
		{
			if (_disposables != null)
				foreach (var d in _disposables)
					d?.Dispose();
		}

		private void Spawn()
		{
			for (int i = 0; i < _elements.Length; i++)
			{
				if (_elements[i] != null) continue;

				var spawnPoint = _camera.ViewportToWorldPoint(_spawnViewportPoint);
				spawnPoint.z = 0;
				var movePoint = _camera.ViewportToWorldPoint(_elementsViewportPoints[i]);
				movePoint.z = 0;

				_elements[i] = _factory.Create();
				_elements[i].transform.position = spawnPoint;
				_elements[i].transform.DOMove(movePoint, 0.3f);
				_disposables[i] = _elements[i].Model.InCircle.Subscribe((b) =>
				{
					if (b)
					{
						_disposables[i].Dispose();
						_elements[i] = null;
					}
				});
				_currentTime = 0;
				return;
			}
		}
	}
}