using UniRx;
using System.Collections.Generic;

namespace Game.Magic.Elements
{
	public class MagicElementModel
	{
		public MagicElementView View;
		public MagicElementConfig Config;

		public IReadOnlyList<MagicArrowModel> Arrows => _arrows;

		public ReactiveProperty<float> Rotation = new();
		public ReactiveProperty<bool> InCircle = new();

		private readonly MagicArrowModel[] _arrows;

		public MagicElementModel(MagicArrowModel[] arrows) =>
			_arrows = arrows;
	}
}
