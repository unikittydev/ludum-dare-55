using Zenject;

namespace Game.Scoring
{
	public class ScoreServiceInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<Leaderboard>()
				.AsSingle()
				.WhenInjectedInto<ScoreService>();

			Container.Bind<ScoreService>()
				.AsSingle();
		}
	}
}