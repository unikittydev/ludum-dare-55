using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Magic.Elements
{
	[CreateAssetMenu]
	public class MagicElementConfig : ScriptableObject
	{
		public Sprite RuneSprite => _runeSprite;
		public Sprite SymbolSprite => _symbolSprite;

		[SerializeField] private Sprite _runeSprite;
		[SerializeField] private Sprite _symbolSprite;
	}
}