using UnityEngine;

namespace Game.Magic.Elements
{
	public class MagicArrowModel
	{
		public MagicArrowView View;
		public Color Color;
		public Vector2 Axis => _axis.normalized;

		private Vector2 _axis;

		public MagicArrowModel(Color color, Vector2 axis) 
		{
			Color = color;
			_axis = axis;
		}
	}
}