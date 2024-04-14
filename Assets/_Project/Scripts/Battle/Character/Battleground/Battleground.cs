using DG.Tweening;
using Game.Battle.Character;
using Game.Battle.Character.States;
using UniRx;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Battle
{
	public class Battleground : MonoBehaviour
	{
		public ReactiveCommand OnLeftSideDie = new();
		public ReactiveCommand<CharacterModel> OnRightSideKilled = new();

		[SerializeField] private Transform _battlePoint;
		[SerializeField] private float _distanceBetweenSides;
		[SerializeField] private float _characterWidth;
		[SerializeField] private float _spawnHeight;
		[SerializeField] private float _spawnDuration = 0.2f;

		private Queue<CharacterModel> _leftSide = new();
		private Queue<CharacterModel> _rightSide = new();

		private List<Tween> _spawnTweens = new();

		private void OnDisable()
		{
			foreach (var t in _spawnTweens)
				t?.Kill();
			_spawnTweens.Clear();
		}

		public void SendLeftSide(CharacterModel character)
		{
			Spawn(character, true);
			RegisterCharacter(character);
		}

		public void SendRightSide(CharacterModel character) 
		{
			Spawn(character, false);
			RegisterCharacter(character);
		}

		private void Spawn(CharacterModel character, bool left)
		{
			Vector2 spawnPoint;
			Vector2 walkPoint;
			if (left)
			{
				_leftSide.Enqueue(character);
				spawnPoint = GetLeftPoint(_leftSide.Count);
				walkPoint = GetLeftPoint(_leftSide.Count - 1);
			}
			else
			{
				_rightSide.Enqueue(character);
				spawnPoint = GetRightPoint(_rightSide.Count);
				walkPoint = GetRightPoint(_rightSide.Count - 1);
			}

			spawnPoint.y += _spawnHeight;
			
			character.View.transform.position = spawnPoint;
			character.View.transform.DOMoveY(_battlePoint.position.y, _spawnDuration);
			_spawnTweens.Add(DOTween.Sequence()
				.AppendInterval(_spawnDuration)
				.AppendCallback(() => character.StateMachine.SwitchState(new CharacterWalkState(character.StateMachine, walkPoint))));
		}

		private void RegisterCharacter(CharacterModel character)
		{
			character.StateMachine.CurrentState.Subscribe(OnCharacterStateChanged)
				.AddTo(character.View);
			character.Health
				.Where(h => h == 0)
				.Subscribe(_ => OnCharacterDie(character))
				.AddTo(character.View);
		}

		private void OnCharacterStateChanged(CharacterState state)
		{
			if (_leftSide.Count == 0 || _rightSide.Count == 0)
				return;
			
			var left = _leftSide.Peek();
			var right = _rightSide.Peek();
			if (right.StateMachine.CurrentState.Value is CharacterReadyState &&
				left.StateMachine.CurrentState.Value is CharacterReadyState)
			{
				left.StateMachine.SwitchState(new CharacterFightState(left.StateMachine, right));
				right.StateMachine.SwitchState(new CharacterFightState(right.StateMachine, left));
			}
		}

		private void OnCharacterDie(CharacterModel character)
		{
			var hasLeft = _leftSide.TryPeek(out var left);
			var hasRight = _rightSide.TryPeek(out var right);
			
			if (character == left)
			{
				_leftSide.Dequeue();
				if (_leftSide.Count == 0)
					OnLeftSideDie.Execute();
				MoveSide(_leftSide.ToArray(), true);
				if (hasRight)
					right.StateMachine.SwitchState(new CharacterReadyState(right.StateMachine));
			}
			else if (character == right)
			{
				OnRightSideKilled.Execute(_rightSide.Dequeue());
				MoveSide(_rightSide.ToArray(), false);
				if (hasLeft)
					left.StateMachine.SwitchState(new CharacterReadyState(left.StateMachine));
			}
		}

		private void MoveSide(CharacterModel[] characters, bool left)
		{
			for (int i = 0; i < characters.Length; i++)
			{
				Vector2 point = left ? GetLeftPoint(i) : GetRightPoint(i);
				characters[i].StateMachine.SwitchState(new CharacterWalkState(characters[i].StateMachine, point));
			}
		}

		private Vector2 GetLeftPoint(int index) =>
			new Vector2(_battlePoint.position.x - _distanceBetweenSides / 2 - index * _characterWidth,
				_battlePoint.position.y);
		private Vector2 GetRightPoint(int index) =>
			new Vector2(_battlePoint.position.x + _distanceBetweenSides / 2 + index * _characterWidth,
				_battlePoint.position.y);
	}
}