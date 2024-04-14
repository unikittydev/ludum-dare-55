using UniRx;
using System;
using Dan.Main;

namespace Game.Scoring
{
	public class Leaderboard
	{
		public ReactiveProperty<bool> Signed = new();

		private const string PUBLIC_KEY = "0a2ea4fd08e29aff08e8b171c85780b199756c8fc0625326f9767d2ceec5fd9f";
		private const string PRIVATE_KEY = "3dc5a74590aff85a555d1fa560db130e40171e5f905c53e1458823bdded32bca7ff9bac66e291f0497d1e12962243190478c64e225d02c5b947dd98c4cc4480f1c736a986afde7bf0d49d66690ae577848d8ff5b84882f836cc1640c1d0d0881529b71d04b7b688a725f56c80c81be004c438fbc0c1c9db7eec3556935ad8403";

		public void AddScore(int score, string name) =>
			LeaderboardCreator.UploadNewEntry(PUBLIC_KEY, name, score);
		public void GetScores(Action<Dan.Models.Entry[]> callback) =>
			LeaderboardCreator.GetLeaderboard(PUBLIC_KEY, callback);
	}
}