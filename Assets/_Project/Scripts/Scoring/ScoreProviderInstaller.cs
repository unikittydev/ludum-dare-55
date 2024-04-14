using UnityEngine;
using Zenject;

namespace Game.Scoring
{
	public class ScoreProviderInstaller : MonoInstaller
	{
		[SerializeField] private ScoreProvider.Config _config;

		public override void InstallBindings()
		{
			Container.BindInterfacesTo<ScoreProvider>()
				.AsSingle()
				.WithArguments(_config)
				.NonLazy();
		}
	}
}