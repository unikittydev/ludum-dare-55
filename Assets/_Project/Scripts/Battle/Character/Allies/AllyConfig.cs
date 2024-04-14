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

        public int BaseHealth => _baseHealth;
        public int BaseDamage => _baseDamage;
        public float BaseAttackSpeed => _baseAttackSpeed;

        [SerializeField] private CharacterView _prefab;
        [Space]
        [SerializeField] private AllySummonCondition _condition;
        [Space]
        [SerializeField] private int _baseHealth;
        [SerializeField] private int _baseDamage;
        [SerializeField] private float _baseAttackSpeed;
    }
}