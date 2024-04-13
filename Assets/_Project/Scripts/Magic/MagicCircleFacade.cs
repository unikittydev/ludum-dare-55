using Game.Magic.Model;
using UnityEngine;

namespace Game.Magic
{
	public class MagicCircleFacade
	{
		public MagicCircleModel Model;
		public GameObject GeneratedCircle;

		public MagicCircleFacade(MagicCircleModel model, GameObject generatedCircle)
		{
			Model = model;
			GeneratedCircle = generatedCircle;
		}
	}
}