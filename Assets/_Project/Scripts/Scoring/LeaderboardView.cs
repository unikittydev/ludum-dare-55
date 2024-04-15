using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;
using UniRx;
using System.Linq;
using UnityEngine.UI;
using Game.UI;

namespace Game.Scoring
{
	public class LeaderboardView : MonoBehaviour
	{
		[Inject] private Leaderboard _leaderboard;
		[Inject] private ScoreService _score;

		[SerializeField] private TMP_Text[] _names;
		[SerializeField] private TMP_Text[] _scores;
		[SerializeField] private TMP_InputField _nameField;
		[SerializeField] private Button _submitButton;
		[Space]
		[SerializeField] private Color _defaultTextColor;
		[SerializeField] private Color _myTextColor;
		[Space]
		[SerializeField] private SceneLoader _forRestart;

		private Dan.Models.Entry _myEntry;
		private int _myEntryIndex;
		private int _myEntryPlace;

		private CompositeDisposable _disposable;
		private bool _loaded;

		private void OnEnable()
		{
			_disposable = new CompositeDisposable();
			_loaded = false;

			foreach (var name in _names)
				name.text = "...";
			foreach (var score in _scores)
				score.text = "...";

            _nameField.SetTextWithoutNotify(StaticStatsSaver.Nickname);
            
			_submitButton.OnClickAsObservable()
				.Subscribe(_ => OnClickSubmit())
				.AddTo(_disposable);

			_nameField.onValueChanged.AsObservable()
				.Subscribe(OnNameChanged)
				.AddTo(_disposable);

			_leaderboard.GetScores(r => Initialize(r.ToList()));
		}

		private void OnDisable() =>
			_disposable.Dispose();
		
		private void Initialize(List<Dan.Models.Entry> entries)
		{
			_loaded = true;
			_myEntry = new Dan.Models.Entry() { Score = _score.Score.Value, Username = "" };
			entries.Add(_myEntry);
			
			var sorted = entries.OrderByDescending(e => e.Score).ToArray();
			var len = Mathf.Min(_names.Length, sorted.Length);
			
			bool myFinded = false;
			for (int i = 0; i < len; i++)
			{
				var entry = sorted[i];
				if (entry.Username == "")
				{
					myFinded = true;
					_names[i].color = _myTextColor;
					_scores[i].color = _myTextColor;
					_myEntryIndex = i;
					_myEntryPlace = i + 1;
				}
                else
                {
					_names[i].color = _defaultTextColor;
					_scores[i].color = _defaultTextColor;
                }

				_names[i].text = (i + 1).ToString() + ". " + entry.Username;
				_scores[i].text = entry.Score.ToString("0");
            }

			if (!myFinded)
			{
				_myEntryPlace = entries.IndexOf(_myEntry) + 1;
				_myEntryIndex = _names.Length - 1;

				_names[^1].color = _myTextColor;
				_scores[^1].color = _myTextColor;
				_names[^1].text = _myEntryPlace + ". " + _nameField.text;
				_scores[^1].text = _myEntry.Score.ToString("0");

				_names[^2].text = "...";
				_scores[^2].text = "...";
			}
		}
		
		private void OnClickSubmit()
		{
			if (!_loaded)
				return;
			if (_nameField.text.Length < 3)
				return;

			StaticStatsSaver.Nickname = _nameField.text;

			_leaderboard.AddScore(_score.Score.Value, _nameField.text);
			_forRestart.RestartGame();
		}

		private void OnNameChanged(string name)
		{
			if (!_loaded) return;
			_names[_myEntryIndex].text = _myEntryPlace.ToString() + ". " + name;
		}
	}
}