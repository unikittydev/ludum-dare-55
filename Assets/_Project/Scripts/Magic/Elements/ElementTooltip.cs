using System.Collections;
using TMPro;
using UnityEngine;

namespace Game.Magic.Elements
{
	public class ElementTooltip : MonoBehaviour
	{
		[SerializeField] private RectTransform _container;
		[SerializeField] private TMP_Text _healthPrefab;
		[SerializeField] private TMP_Text _damagePrefab;
		[SerializeField] private TMP_Text _attackSpeedPrefab;
		[SerializeField] private Transform _constraintParent;

		private Vector3 _position;

		private void Awake()
		{
			_position = transform.position - _constraintParent.position;
		}

		private void Update()
		{
			transform.rotation = Quaternion.identity;
			transform.position = _constraintParent.position + _position;
		}

		public void Set(MagicElementModel model)
		{
			foreach (var i in model.Config.Influences)
			{
				if (i.Parameter == EElementParam.AttackSpeed)
					Instantiate(_attackSpeedPrefab, _container).text = i.Value.ToString("0.0");
				else if (i.Parameter == EElementParam.Damage)
					Instantiate(_damagePrefab, _container).text = i.Value.ToString();
				else
					Instantiate(_healthPrefab, _container).text = i.Value.ToString();
			}
		}
	}
}