using System;

namespace Domain
{
    /// <summary>
    ///     ユニットテンプレート（プロトタイプ）を表すオブジェクト。
    ///     バランス調整データやアセット参照のキーを保持します。
    /// </summary>
    public sealed class UnitTemplate
    {
        /// <summary>
        ///     コンストラクタ。ユニットテンプレートの各種パラメータを初期化する。
        /// </summary>
        public UnitTemplate(string templateId, UnitType unitType, float maxHealth, float moveSpeed,
            float attackPower, float attackRange, float defense, float criticalRate, float criticalMultiplier)
        {
            TemplateId = templateId ?? string.Empty;
            UnitType = unitType;
            MaxHealth = maxHealth;
            MoveSpeed = moveSpeed;
            AttackPower = attackPower;
            AttackRange = attackRange;
            Defense = defense;
            CriticalRate = criticalRate;
            CriticalMultiplier = criticalMultiplier;
        }

        /// <summary> テンプレート識別子。 </summary>
        public string TemplateId { get; }

        /// <summary> ユニットの種類。 </summary>
        public UnitType UnitType { get; }

        /// <summary> 最大体力。 </summary>
        public float MaxHealth { get; }

        /// <summary> 移動速度。 </summary>
        public float MoveSpeed { get; }

        /// <summary> 攻撃力。 </summary>
        public float AttackPower { get; }

        /// <summary> 攻撃射程。 </summary>
        public float AttackRange { get; }   

        /// <summary> 防御力。 </summary>
        public float Defense { get; }

        /// <summary> クリティカル発生率。 </summary>
        public float CriticalRate { get; }

        /// <summary> クリティカルダメージ倍率。 </summary>
        public float CriticalMultiplier { get; }
    }
}
