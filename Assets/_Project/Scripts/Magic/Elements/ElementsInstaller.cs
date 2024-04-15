using System;
using UniOwl.Audio;
using UnityEngine;
using Zenject;

namespace Game.Magic.Elements
{
	public class ElementsInstaller : MonoInstaller
	{
		[Serializable]
		public struct AudioData
		{
			public AudioCue _beginDrag;
			public AudioCue _place;
		}
		
		[SerializeField] private MagicElementView _elementPrefab;
		[SerializeField] private MagicArrowView _arrowPrefab;
		[Space]
		[SerializeField] private LayerMask _elementsMask;
		[SerializeField] private LayerMask _elementsInputMask;
		[SerializeField] private LayerMask _slotsInputMask;
		[SerializeField] private LineRenderer _rotationLine;
		[SerializeField] private LineRenderer _linkPrefab;
		[SerializeField] private Camera _camera;

		[SerializeField] private AudioData _audioData;
		
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<ElementRotationHandler>()
				.AsSingle()
				.WithArguments(_elementsInputMask, _rotationLine, _camera);
			Container.BindInterfacesAndSelfTo<ElementDragHandler>()
				.AsSingle()
				.WithArguments(_elementsInputMask, _slotsInputMask, _camera, _audioData);
			Container.BindInterfacesAndSelfTo<ElementsLinkHandler>()
				.AsSingle()
				.WithArguments(_elementsMask, _linkPrefab);

			Container.Bind<MagicElementsFactory>()
				.AsSingle()
				.WithArguments(_elementPrefab, _arrowPrefab);
		}
	}
}