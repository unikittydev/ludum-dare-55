using UnityEngine;
using Zenject;

namespace Game.Magic.Elements
{
	public class ElementsInstaller : MonoInstaller
	{
		[SerializeField] private MagicElementView _elementPrefab;
		[SerializeField] private MagicArrowView _arrowPrefab;
		[Space]
		[SerializeField] private LayerMask _elementsMask;
		[SerializeField] private LayerMask _slotsMask;
		[SerializeField] private LineRenderer _rotationLine;
		[SerializeField] private LineRenderer _linkPrefab;
		[SerializeField] private Camera _camera;

		public override void InstallBindings()
		{
			Container.BindInterfacesTo<ElementRotationHandler>()
				.AsSingle()
				.WithArguments(_elementsMask, _rotationLine, _camera);
			Container.BindInterfacesTo<ElementDragHandler>()
				.AsSingle()
				.WithArguments(_elementsMask, _slotsMask, _camera);
			Container.BindInterfacesAndSelfTo<ElementsLinkHandler>()
				.AsSingle()
				.WithArguments(_elementsMask, _linkPrefab);

			Container.Bind<MagicElementsFactory>()
				.AsSingle()
				.WithArguments(_elementPrefab, _arrowPrefab);
		}
	}
}