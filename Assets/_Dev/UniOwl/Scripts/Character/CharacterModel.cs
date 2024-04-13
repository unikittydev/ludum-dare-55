using System;
using UnityEngine;

namespace Game.Character
{
    public class CharacterModel
    {
        public float CurrentHealth { get; private set; }
        public float MaxHealth { get; }
        public float AttackDamage { get; }
        public float AttackSpeed { get; }
        public float WalkSpeed { get; }

        public event Action<CharacterModel> OnHealthZero; 
        
        public void DealDamage(float damage)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0f, MaxHealth);
            
            if (CurrentHealth <= 0f)
                OnHealthZero?.Invoke(this);
        }
    }
}