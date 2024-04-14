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
		[SerializeField] private LayerMask _elementsInputMask;
		[SerializeField] private LayerMask _slotsInputMask;
		[SerializeField] private LineRenderer _rotationLine;
		[SerializeField] private LineRenderer _linkPrefab;
		[SerializeField] private Camera _camera;

		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<ElementRotationHandler>()
				.AsSingle()
				.WithArguments(_elementsInputMask, _rotationLine, _camera);
			Container.BindInterfacesAndSelfTo<ElementDragHandler>()
				.AsSingle()
				.WithArguments(_elementsInputMask, _slotsInputMask, _camera);
			Container.BindInterfacesAndSelfTo<ElementsLinkHandler>()
				.AsSingle()
				.WithArguments(_elementsMask, _linkPrefab);

			Container.Bind<MagicElementsFactory>()
				.AsSingle()
				.WithArguments(_elementPrefab, _arrowPrefab);
		}
	}
}