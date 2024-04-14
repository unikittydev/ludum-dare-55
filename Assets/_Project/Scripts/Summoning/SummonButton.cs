using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Game.Summoning
{
	public class SummonButton : MonoBehaviour, IPointerClickHandler
	{
		[Inject] private SummonProvider _provider;

		public void OnPointerClick(PointerEventData eventData)
		{
			_provider.Summon();
		}
	}
}