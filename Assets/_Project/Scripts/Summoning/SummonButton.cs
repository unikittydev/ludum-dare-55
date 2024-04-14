using Game.Magic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Game.Summoning
{
	public class SummonButton : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] private SummonButtonView _view;
		[SerializeField] private Image _cooldownImage;
		[SerializeField] private float _cooldownDuration;

		private float _cooldownTime;

		public void OnPointerClick(PointerEventData eventData)
		{
			if (_cooldownTime > 0)
				return;
			_cooldownTime = _cooldownDuration;
		}

		private void Update()
		{
			_cooldownTime -= Time.deltaTime;
			_view.Lock.Value = _cooldownTime >= 0;
			_cooldownImage.gameObject.SetActive(_cooldownTime >= 0);
			if (_cooldownTime > 0)
				_cooldownImage.fillAmount = _cooldownTime / _cooldownDuration;
		}
	}
}