using Game.Configs;
using UnityEngine;
using Zenject;

namespace Game.Magic.Elements
{
	public class MagicArrowView : MonoBehaviour
	{
		public LineRenderer Line => _line;

		[SerializeField] private LineRenderer _line;
		[SerializeField] private Material _initialMaterial;
		[SerializeField] private Material _inCircleMat;

		[Inject] private ElementsConfig _config;
		
		[Inject] 
		private void Generate(MagicArrowModel model)
		{
			_line.material = _initialMaterial;
			_line.startColor = model.Color;
			_line.endColor = model.Color;
			_line.positionCount = 2;
			_line.SetPosition(0, transform.position);
			_line.SetPosition(1, (Vector2) transform.position + model.Axis * _config.ArrowLength);
		}

		public void SetInCircle() => _line.material = _inCircleMat;
	}
}
