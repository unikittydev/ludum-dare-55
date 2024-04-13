using Game.MathUtils;
using UnityEngine;
using Zenject;

namespace Game.Magic.View
{
	public class MagicCircleOrbitView : MonoBehaviour
	{
		[SerializeField] private LineRenderer _circleLine;
		[SerializeField] private Material[] _lineMaterials;

		[Inject]
		private void Generate(float radius)
		{
			_circleLine.material = _lineMaterials[Random.Range(0, _lineMaterials.Length)];
			_circleLine.positionCount = 720;
			for (float degree = 0; degree < 360; degree += 0.5f)
				_circleLine.SetPosition((int)(degree * 2), 
					(Vector2) transform.position + Utils.GetVector(degree) * radius);
		}
	}
}