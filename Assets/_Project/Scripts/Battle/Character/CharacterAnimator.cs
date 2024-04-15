using UniOwl.Audio;
using UnityEngine;

namespace Game.Battle.Character
{
	[RequireComponent(typeof(Animator))]
	public class CharacterAnimator : MonoBehaviour
	{
		private static readonly int ATTACK_KEY = Animator.StringToHash("ATTACK");
		private static readonly int DAMAGE_KEY = Animator.StringToHash("DAMAGE");
		private static readonly int DEATH_KEY = Animator.StringToHash("DEATH");
		private static readonly int MOVE_KEY = Animator.StringToHash("MOVE");

		private static readonly int ATTACK_ANIM_SPEED = Animator.StringToHash("ATTACK_SPEED");
		private static readonly int DAMAGE_ANIM_SPEED = Animator.StringToHash("DAMAGE_SPEED");

		[SerializeField] private Animator _animator;
		[SerializeField] private AudioCue _attackCue;
		[SerializeField] private AudioCue _deathCue;
		
		public void SetAttackSpeed(float speed) =>
			_animator.SetFloat(ATTACK_ANIM_SPEED, speed);
		public void SetDamageSpeed(float speed) =>
			_animator.SetFloat(DAMAGE_ANIM_SPEED, speed);

		public void PlayAttack()
		{
			_animator.SetTrigger(ATTACK_KEY);
			AudioSFXSystem.PlayCue2D(_attackCue);
		}
		public void PlayDamage() =>
			_animator.SetTrigger(DAMAGE_KEY);
		
		public void PlayDeath()
		{
			_animator.SetTrigger(DEATH_KEY);
			AudioSFXSystem.PlayCue2D(_deathCue);
		}

		public void PlayMove(bool move) =>
			_animator.SetBool(MOVE_KEY, move);
	}
}