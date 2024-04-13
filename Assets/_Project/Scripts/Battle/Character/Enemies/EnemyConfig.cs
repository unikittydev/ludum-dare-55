using UnityEngine;

namespace Game.Battle.Character.Enemies
{
    [CreateAssetMenu]
    public class EnemyConfig : ScriptableObject
    {
        public CharacterView Prefab => _prefab;

        public float HealthMultiplier => _healthMultiplier;
        public float DamageMultiplier => _damageMultiplier;
        public float AttackSpeedMultiplier => _attackSpeedMultiplier;

        public AnimationCurve SpawnFrequencyCurve => _spawnFrequencyCurve;

        [SerializeField] private CharacterView _prefab;
        [Space]
        [SerializeField] private float _healthMultiplier;
        [SerializeField] private float _damageMultiplier;
        [SerializeField] private float _attackSpeedMultiplier;
        [Space]
        [SerializeField] private AnimationCurve _spawnFrequencyCurve;
    }
}