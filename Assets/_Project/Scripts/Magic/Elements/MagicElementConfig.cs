using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Magic.Elements
{
	public enum EElementParam
	{
		Health,
		Damage,
		AttackSpeed
	}
	[System.Serializable]
	public class ElementInfluence
	{
		public EElementParam Parameter;
		public float Value;
	}

	[CreateAssetMenu]
	public class MagicElementConfig : ScriptableObject
	{
		public Sprite RuneSprite => _runeSprite;
		public Sprite SymbolSprite => _symbolSprite;

		public IReadOnlyList<ElementInfluence> Influences => _influences;

		[SerializeField] private Sprite _runeSprite;
		[SerializeField] private Sprite _symbolSprite;
		[SerializeField] private ElementInfluence[] _influences;
	}
}