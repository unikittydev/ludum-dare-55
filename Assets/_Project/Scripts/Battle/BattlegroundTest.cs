using Game.Battle.Character;
using UnityEngine;
using Zenject;

namespace Game.Battle
{
	public class BattlegroundTest : MonoBehaviour
	{
		[SerializeField] private CharacterView _leftPrefab;
		[SerializeField] private CharacterView _rightPrefab;

		[Inject] private Battleground _ground;
		[Inject] private CharacterFactory _factory;

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.D))
				_ground.SendRightSide(_factory.Create(_rightPrefab, 100, 10, 1));
			if (Input.GetKeyDown(KeyCode.A))
				_ground.SendLeftSide(_factory.Create(_leftPrefab, 100, 15, 2));
		}
	}
}