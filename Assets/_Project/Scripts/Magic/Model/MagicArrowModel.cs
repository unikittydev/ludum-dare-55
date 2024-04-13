using UnityEngine;

namespace Game.Magic.Elements
{
	public class MagicArrowModel
	{
		public MagicArrowView View;
		public Color Color;
		public Vector2 Axis;

		public MagicArrowModel(Color color, Vector2 axis) 
		{
			Color = color;
			Axis = axis;
		}
	}
}