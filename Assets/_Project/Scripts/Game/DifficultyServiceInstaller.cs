using Zenject;

namespace Game.Configs
{
	public class DifficultyServiceInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<GameDifficultyService>()
				.AsSingle();
		}
	}
}