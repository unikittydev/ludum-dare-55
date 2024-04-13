using Game.Magic.Generation;
using Game.Magic.Model;
using Game.Magic.View;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Magic
{
	public class MagicCircleFactory
	{
		public ReactiveProperty<MagicCircleFacade> CurrentCircle = new();

		[Inject] private MagicCircleGenerationService _generation;
		[Inject] private DiContainer _di;
		
		[Inject] private Transform _magicCirclePosition;
		[Inject] private MagicCircleOrbitView _orbitPrefab;
		[Inject] private MagicCircleSlotView _slotPrefab;

		public void Clear()
		{
			if (CurrentCircle.Value != null)
			{
				Object.Destroy(CurrentCircle.Value.GeneratedCircle);
				CurrentCircle.Value = null;
			}
		}

		public void CreateNew()
		{
			if (CurrentCircle.Value != null)
				Object.Destroy(CurrentCircle.Value.GeneratedCircle);
			
			var entry = _generation.GenerateEntry();
			var model = new MagicCircleModel(entry);
			CurrentCircle.Value = new MagicCircleFacade(model, GenerateObject(model, entry));
		}

		private GameObject GenerateObject(MagicCircleModel model, MagicCircleGenerationEntry entry)
		{
			var go = new GameObject("Magic Circle");
			go.transform.position = _magicCirclePosition.position;

			for (int i = 0; i < model.Orbits.Count; i++)
			{
				var orbitView = _di.InstantiatePrefabForComponent<MagicCircleOrbitView>(_orbitPrefab,
					_magicCirclePosition.position, Quaternion.identity, go.transform,
					new object[] { entry.Orbits[i].Config.Radius });
				model.Orbits[i].View = orbitView;

				foreach (var item in model.Orbits[i].Slots)
				{
					var slotView = _di.InstantiatePrefabForComponent<MagicCircleSlotView>(_slotPrefab,
						(Vector2)orbitView.transform.position + item.Axis * entry.Orbits[i].Config.Radius,
						Quaternion.identity, orbitView.transform,
						new object[] { item });
					item.View = slotView;
				}
			}

			return go;
		}
	}
}
