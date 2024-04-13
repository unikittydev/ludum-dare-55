using Unity.Services.Authentication;
using Unity.Services.Core;
using Zenject;
using UniRx;
using UnityEngine;
using System;
using Unity.Services.Leaderboards;
using Newtonsoft.Json;

namespace Game.Scoring
{
	public class Leaderboard : IInitializable
	{
		public ReactiveProperty<bool> Signed = new();

		private const string ID = "GameLD";
		
		async void IInitializable.Initialize()
		{
			await UnityServices.InitializeAsync();

			AuthenticationService.Instance.SignedIn += () =>
			{
				Debug.Log("Signed: " + AuthenticationService.Instance.PlayerId);
				Signed.Value = true;
			};
			AuthenticationService.Instance.SignInFailed += s => 
				Debug.Log(s);

			await AuthenticationService.Instance.SignInAnonymouslyAsync();
		}

		public async void AddScore(int score)
		{
			var response = await LeaderboardsService.Instance.AddPlayerScoreAsync(ID, score);
			Debug.Log(JsonConvert.SerializeObject(response));
		}

		public async void GetScores()
		{
			var scoresResponse =
				await LeaderboardsService.Instance.GetScoresAsync(ID);
			Debug.Log(JsonConvert.SerializeObject(scoresResponse));
		}

		public async void GetPlayerScore()
		{
			var scoreResponse =
				await LeaderboardsService.Instance.GetPlayerScoreAsync(ID);
			Debug.Log(JsonConvert.SerializeObject(scoreResponse));
		}
	}
}