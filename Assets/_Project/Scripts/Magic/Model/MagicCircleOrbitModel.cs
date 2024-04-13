using Game.Magic.Elements;
using Game.Magic.View;
using Game.MathUtils;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Game.Magic.Model
{
	public class MagicCircleOrbitModel
	{
		public class OrbitSlot
		{
			public MagicCircleSlotView View;
			public Vector2 Axis;
			public ReactiveProperty<MagicElementModel> Element = new();
		}

		public MagicCircleOrbitView View;

		public IReadOnlyList<OrbitSlot> Slots => _elements;

		private List<OrbitSlot> _elements = new();

		public MagicCircleOrbitModel(bool[] slotStates)
		{
			for (int i = 0; i < slotStates.Length; i++)
				if (slotStates[i])
					_elements.Add(new OrbitSlot() { Axis = Utils.GetVector(i, 360f / slotStates.Length) });
		}
	}
}