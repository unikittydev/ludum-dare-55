using Game.Battle.Character.Allies;
using Game.Battle.Character.Enemies;
using UnityEngine;
using Zenject;

namespace Game.Battle.Character
{
	public class CharactersInstaller : MonoInstaller
	{
		[SerializeField] private Battleground _battleground;

		public override void InstallBindings()
		{
			Container.Bind<Battleground>()
				.FromInstance(_battleground)
				.AsSingle();

			Container.Bind<CharacterFactory>()
				.AsSingle();

			Container.Bind<EnemiesFactory>()
				.AsSingle();

			Container.Bind<AlliesFactory>()
				.AsSingle();

			Container.BindInterfacesAndSelfTo<EnemiesSpawner>()
				.AsSingle();
			Container.BindInterfacesTo<EnemiesSpawnerBootstrap>()
				.AsSingle();
		}
	}
}