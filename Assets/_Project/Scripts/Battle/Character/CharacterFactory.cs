using Zenject;

namespace Game.Battle.Character
{
	public class CharacterFactory
	{
		[Inject] private DiContainer _di;

		public CharacterModel Create(CharacterView prefab, int health, int damage, float attackSpeed)
		{
			var model = new CharacterModel(health, damage, attackSpeed);
			var view = _di.InstantiatePrefabForComponent<CharacterView>(prefab, new object[] { model });
			var stateMachine = _di.InstantiateComponent<CharacterStateMachine>(view.gameObject, new object[] { model });
			return model;
		}
	}
}