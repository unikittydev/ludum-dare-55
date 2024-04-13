using UniRx;
using Unity.Services.Authentication;
using Zenject;

namespace Game.Scoring
{
	public class ScoreService
	{
		public IReadOnlyReactiveProperty<int> Score => _score;

		[Inject] private Leaderboard _leaderboard;

		private ReactiveProperty<int> _score = new();

		public void IncreaseScore(int amount) => _score.Value += amount;
		public void ResetScore() => _score.Value = 0;

		public async void SubmitScore(string userName)
		{
			if (!_leaderboard.Signed.Value)
				await _leaderboard.Signed.WaitUntilValueChangedAsync();

			await AuthenticationService.Instance.UpdatePlayerNameAsync(userName);
			_leaderboard.AddScore(_score.Value);
		}
	}
}