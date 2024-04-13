using Game.Configs;
using Game.MathUtils;
using System.Linq;
using UnityEngine;

namespace Game.Magic.Generation
{
	public struct MagicCircleGenerationEntry
	{
		public MagicCircleOrbitGenerationEntry[] Orbits;

		public MagicCircleGenerationEntry(GameDifficultyService difficulty)
		{
			var configs = difficulty.GetOrbits();
			var slots = GetSlots(difficulty.GetSlotsCount(), configs);
			Orbits = new MagicCircleOrbitGenerationEntry[configs.Length];
			for (int i = 0; i < configs.Length; i++)
				Orbits[i] = new MagicCircleOrbitGenerationEntry(configs[i], slots[i]);
		}

		public static bool[][] GetSlots(int slotsCount, OrbitConfig[] configs)
		{
			var all = new bool[configs.Sum(c => c.SlotsCount)];
			var count = Mathf.Min(all.Length, slotsCount);
			for (int i = 0; i < count; i++)
				all[i] = true;
			all.Shuffle();

			var result = new bool[configs.Length][];
			int skip = 0;
			for (int i = 0; i < configs.Length; i++)
			{
				result[i] = all.Skip(skip).Take(configs[i].SlotsCount).ToArray();
				skip += configs[i].SlotsCount;
			}
			return result;
		}
	}
}