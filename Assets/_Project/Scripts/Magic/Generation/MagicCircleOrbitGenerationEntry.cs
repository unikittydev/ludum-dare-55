using Game.Configs;

namespace Game.Magic.Generation
{
	public struct MagicCircleOrbitGenerationEntry
	{
		public OrbitConfig Config;
		public bool[] SlotStates;

		public MagicCircleOrbitGenerationEntry(OrbitConfig config, bool[] slotsStates)
		{
			Config = config;
			SlotStates = slotsStates;
		}
	}
}