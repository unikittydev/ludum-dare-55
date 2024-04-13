using Game.Magic.Generation;
using System.Collections.Generic;

namespace Game.Magic.Model
{
	public class MagicCircleModel
	{
		public IReadOnlyList<MagicCircleOrbitModel> Orbits => _orbits;

		private MagicCircleOrbitModel[] _orbits;

		public MagicCircleModel(MagicCircleGenerationEntry entry)
		{
			_orbits = new MagicCircleOrbitModel[entry.Orbits.Length];
			for (int i = 0; i < _orbits.Length; i++)
				_orbits[i] = new MagicCircleOrbitModel(entry.Orbits[i].SlotStates);
		}
	}
}
