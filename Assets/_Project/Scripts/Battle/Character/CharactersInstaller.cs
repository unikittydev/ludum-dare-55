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
		}
	}
}