using UnityEngine;
using Zenject;

namespace Game.Configs
{
	public class GameConfigInstaller : MonoInstaller
	{
		[SerializeField] private GameConfig _config;

		public override void InstallBindings()
		{
			Container.Bind<EnemiesConfig>()
				.FromInstance(_config.Enemies)
				.AsSingle();

			Container.Bind<ElementsConfig>()
				.FromInstance(_config.Elements)
				.AsSingle();

			Container.Bind<MagicCircleConfig>()
				.FromInstance(_config.MagicCircle)
				.AsSingle();

			Container.Bind<AlliesConfig>()
				.FromInstance(_config.Allies)
				.AsSingle();
		}
	}
}