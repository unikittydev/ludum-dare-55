using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Scoring
{
	public class ScoreText : MonoBehaviour
	{
		[SerializeField] private TMP_Text _text;

		[Inject] private ScoreService _score;
		private CompositeDisposable _disposables;

		private void OnEnable()
		{
			_disposables = new();
			_score.Score.Subscribe(v => _text.text = v.ToString()).AddTo(_disposables);
		}
		private void OnDisable() =>
			_disposables?.Dispose();
	}
}