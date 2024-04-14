using Game.Battle.Character.States;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Battle.Character
{
	public class CharacterStateMachine : MonoBehaviour
	{
		public IReadOnlyReactiveProperty<CharacterState> CurrentState => _currentState;

		public CharacterModel Model => _model;

		private ReactiveProperty<CharacterState> _currentState = new();
		private CharacterModel _model;

		[Inject]
		private void Construct(CharacterModel model)
		{
			_model = model;
			_model.StateMachine = this;
			Model.Health.Where(h => h == 0).Subscribe(_ => SwitchState(null))
				.AddTo(this);
		}

		private void OnDisable() =>
			_currentState.Value?.Exit();

		public void SwitchState(CharacterState state)
		{
			if (this.isActiveAndEnabled)
			{
				_currentState.Value?.Exit();
				_currentState.SetValueAndForceNotify(state);
				_currentState.Value?.Enter();
			}
		}
	}
}