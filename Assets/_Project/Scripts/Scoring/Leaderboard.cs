using Unity.Services.Authentication;
using Unity.Services.Core;
using Zenject;
using UniRx;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Leaderboards;
using Newtonsoft.Json;
using Unity.Services.Leaderboards.Models;
using UnityEditor.VersionControl;
using TMPro;

namespace Game.Scoring
{
	public class Leaderboard : IInitializable
	{
		public ReactiveProperty<bool> Signed = new();

		private const string ID = "GameLD";
		
		async void IInitializable.Initialize()
		{
			if (UnityServices.State == ServicesInitializationState.Initialized)
				AuthenticationService.Instance.SignOut();
			else
				await UnityServices.InitializeAsync();

			AuthenticationService.Instance.SignedIn += () =>
			{
				Debug.Log("Signed: " + AuthenticationService.Instance.PlayerId);
				Signed.Value = true;
			};
			AuthenticationService.Instance.SignInFailed += s => 
				Debug.Log(s);

			await AuthenticationService.Instance.SignInAnonymouslyAsync(new SignInOptions() { CreateAccount = true });
		}

		public async void AddScore(int score)
		{
			var response = await LeaderboardsService.Instance.AddPlayerScoreAsync(ID, score,
				new AddPlayerScoreOptions() { VersionId = DateTime.Now.ToString() });
			Debug.Log(JsonConvert.SerializeObject(response));
		}

		public async Task<List<LeaderboardEntry>> GetScores()
		{
			var scoresResponse =
				await LeaderboardsService.Instance.GetScoresAsync(ID);
			return scoresResponse.Results;
		}

		public async void GetPlayerScore()
		{
			var scoreResponse =
				await LeaderboardsService.Instance.GetPlayerScoreAsync(ID);
			Debug.Log(JsonConvert.SerializeObject(scoreResponse));
		}
	}
}