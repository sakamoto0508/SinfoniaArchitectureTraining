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
        /// <summary> キャラクターのテンプレート識別子。 </summary>
        public UnitType CharacterName => _characterName;

        /// <summary> キャラクター表示用のプレハブ。 </summary>
        public GameObject Character => _character;

        /// <summary> 初期体力。 </summary>
        public float Health => _health;

        /// <summary> 防御力。 </summary>
        public float Defence => _defence;

        /// <summary> 移動速度。 </summary>
        public float MoveSpeed => _moveSpeed;

        /// <summary> 攻撃力。 </summary>
        public float AttackPower => _attackPower;

        /// <summary> 攻撃射程。 </summary>
        public float AttackRange => _attackRange;

        /// <summary> 攻撃速度。 </summary>
        public float AttackSpeed => _attackSpeed;

        /// <summary> クリティカル発生率。 </summary>
        public float CriticalChance => _criticalChance;

        /// <summary> クリティカルダメージ。 </summary>
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
