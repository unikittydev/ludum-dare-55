using System;
using UnityEngine;

namespace Game.Battle.Character.Allies
{
    [Serializable]
    public class AllySummonCondition
    {
        public int ScoreMin;
        public Vector2 HealthByCircleRange;
        public Vector2 DamageByCircleRange;
        public Vector2 AttackSpeedByCircleRange;
    }

    [CreateAssetMenu]
    public class AllyConfig : ScriptableObject
    {
        public CharacterView Prefab => _prefab;

        public AllySummonCondition Condition => _condition;

        public float BaseHealth => _baseHealth;
        public float BaseDamage => _baseDamage;
        public float BaseAttackSpeed => _baseAttackSpeed;

        [SerializeField] private CharacterView _prefab;
        [Space]
        [SerializeField] private AllySummonCondition _condition;
        [Space]
        [SerializeField] private float _baseHealth;
        [SerializeField] private float _baseDamage;
        [SerializeField] private float _baseAttackSpeed;
    }
}