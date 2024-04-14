using System.Linq;
using TMPro;
using UnityEngine;

namespace Game.Magic.Elements
{
	public class ElementTooltip : MonoBehaviour
	{
		[SerializeField] private TMP_Text _text;

		public void Set(MagicElementModel model)
		{
			_text.text = string.Join("\n", model.Config.Influences.Select(i =>
			{
				if (i.Parameter == EElementParam.AttackSpeed)
					return "ATKSPD " + i.Value.ToString("0.0");
				
				string p = "HP";
				if (i.Parameter == EElementParam.Damage)
					p = "DMG";
				return p + " " + i.Value.ToString("");
			}));
		}
	}
}