using Zenject;

namespace Game.Summoning
{
	public class SummonInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<SummonProvider>()
				.AsSingle();
		}
	}
}