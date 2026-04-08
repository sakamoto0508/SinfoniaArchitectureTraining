using Domain;
using UnityEngine;

namespace InfraStructure
{
    /// <summary>
    ///     キャラクターの基本的なデータを保持するScriptableObject。
    /// </summary>
    [CreateAssetMenu(fileName = "NewCharacterAsset", menuName = "Character/Asset")]
    public class CharacterAsset : ScriptableObject
    {
        public UnitType CharacterName => _characterName;
        public GameObject Character => _character;
        public float Health => _health;
        public float Defence => _defence;
        public float MoveSpeed => _moveSpeed;
        public float AttackPower => _attackPower;
        public float AttackRange => _attackRange;
        public float AttackSpeed => _attackSpeed;
        public float CriticalChance => _criticalChance;
        public float CriticalDamage => _criticalDamage;

        [SerializeField] private UnitType _characterName;
        [SerializeField] private GameObject _character;
        [SerializeField] private float _health;
        [SerializeField] private float _defence;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _attackPower;
        [SerializeField] private float _attackRange;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _criticalChance;
        [SerializeField] private float _criticalDamage;
    }
}
