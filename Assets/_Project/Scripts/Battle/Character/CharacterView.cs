using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Battle.Character
{
	public class CharacterView : MonoBehaviour
	{
		[SerializeField] private Slider _slider;
		[SerializeField] private TMP_Text _healthText;
		[SerializeField] private TMP_Text _attackSpeedText;
		[SerializeField] private TMP_Text _damageText;

		[Inject]
		private void Construct(CharacterModel model)
		{

		}
	}
}