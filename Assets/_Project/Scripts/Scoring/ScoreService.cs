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
	}
}