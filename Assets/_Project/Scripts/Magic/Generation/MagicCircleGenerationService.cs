using Game.Configs;
using Zenject;

namespace Game.Magic.Generation
{
	public class MagicCircleGenerationService
	{
		[Inject] private GameDifficultyService _difficulty;

		public MagicCircleGenerationEntry GenerateEntry() =>
			new MagicCircleGenerationEntry(_difficulty);
	}
}