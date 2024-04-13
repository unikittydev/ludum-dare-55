using Game.Configs;
using System.Linq;
using Zenject;

namespace Game.Magic.Elements
{
	public class MagicElementsFactory
	{
		[Inject] private MagicElementView _prefab;
		[Inject] private MagicArrowView _arrowPrefab;
		[Inject] private DiContainer _di;
		[Inject] private GameDifficultyService _difficulty;

		public MagicElementView Create()
		{
			var config = _difficulty.GetRandomRune();

			var arrows = _difficulty.GetRandomArrows();
			var model = new MagicElementModel(arrows);
			model.Config = config;

			var view = _di.InstantiatePrefabForComponent<MagicElementView>(_prefab,
				new object[] { model });
			model.View = view;
			
			for (int i = 0; i < arrows.Length; i++)
				arrows[i].View = _di.InstantiatePrefabForComponent<MagicArrowView>(_arrowPrefab,
					view.transform, new object[] { arrows[i] });
			view.Arrows = arrows.Select(a => a.View).ToList();
			return view;
		}
	}
}