using DG.Tweening;
using Game.Battle.Character;
using Game.Battle.Character.States;
using UniRx;
using System.Collections.Generic;
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
		
		[Header("Spawn Animation")]
		[SerializeField] private Transform _spawnTarget;
		[SerializeField] private float _spawnHeight;
		[SerializeField] private float _spawnDuration = 0.2f;
		[SerializeField] float _scaleTime = .4f;
		[SerializeField] float _delay = 1.7f;
		[SerializeField] float _moveUpTime = 1f;
		[SerializeField] float _showcaseScale = 4f;
		[SerializeField] int flips = 10;

		private Queue<CharacterModel> _leftSide = new();
		private Queue<CharacterModel> _rightSide = new();

		private List<Tween> _tweens = new();

		private void OnDisable()
		{
			foreach (var t in _tweens)
				t?.Kill();
			_tweens.Clear();
		}

		public void SendLeftSide(CharacterModel character)
		{
			SpawnLeft(character);
			RegisterCharacter(character);
		}

		public void SendRightSide(CharacterModel character) 
		{
			SpawnRight(character);
			RegisterCharacter(character);
		}

		private void SpawnLeft(CharacterModel character)
		{
			_leftSide.Enqueue(character);
			
			Vector2 spawnPoint = GetLeftPoint(_leftSide.Count);
			Vector2 walkPoint = GetLeftPoint(_leftSide.Count - 1);

			Vector2 circlePosition = _spawnTarget.position;
			
			spawnPoint.y += _spawnHeight;
			
			character.View.transform.position = circlePosition - Vector2.up * (_showcaseScale * .5f);
			character.View.transform.localScale = Vector3.zero;
			
			_tweens.Add(DOTween.Sequence()
				.AppendCallback(() => character.View.StatsCanvas.gameObject.SetActive(false))
				.Append(character.View.transform.DOScale(_showcaseScale, _scaleTime))
				.AppendInterval(_delay)
				.Append(character.View.transform.DOScale(new Vector3(0.8f, 1.2f, 1f), _moveUpTime))
				.Join(character.View.transform.DOMoveY(spawnPoint.y, _moveUpTime))
				.Join(character.View.transform.DORotate(new Vector3(0f, 0f, 360f * flips), _moveUpTime, RotateMode.FastBeyond360))
				.AppendCallback(() => character.View.transform.SetPositionAndRotation(spawnPoint, Quaternion.identity))
				.AppendCallback(() => character.View.StatsCanvas.gameObject.SetActive(true))
				.Append(character.View.transform.DOMoveY(_battlePoint.position.y, _spawnDuration))
				.Join(character.View.transform.DOScale(1f, _spawnDuration))
				.AppendCallback(() => character.StateMachine.SwitchState(new CharacterWalkState(character.StateMachine, walkPoint))));
		}

		private void SpawnRight(CharacterModel character)
		{
			_rightSide.Enqueue(character);
			
			Vector2 spawnPoint = GetRightPoint(_rightSide.Count);
			Vector2 walkPoint = GetRightPoint(_rightSide.Count - 1);
			
			spawnPoint.y += _spawnHeight;
			
			character.View.transform.position = spawnPoint;
	
			_tweens.Add(DOTween.Sequence()
				.Append(character.View.transform.DOMoveY(_battlePoint.position.y, _spawnDuration))
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