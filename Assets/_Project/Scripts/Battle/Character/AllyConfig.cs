using UnityEngine;

namespace Game.Battle.Character
{
    [CreateAssetMenu]
    public class AllyConfig : ScriptableObject
    {
        public CharacterView Prefab => _prefab;

        public float BaseHealth => _baseHealth;
        public float BaseAttackSpeed => _baseAttackSpeed;
        public float BaseDamage => _baseDamage;

        [SerializeField] private CharacterView _prefab;
        [Space]
        [SerializeField] private float _baseHealth;
        [SerializeField] private float _baseAttackSpeed;
        [SerializeField] private float _baseDamage;
    }
}