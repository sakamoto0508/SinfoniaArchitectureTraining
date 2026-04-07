using System;

namespace Domain
{
    /// <summary>
    ///     ユニットテンプレート（プロトタイプ）を表すオブジェクト。
    ///     バランス調整データやアセット参照のキーを保持します。
    /// </summary>
    public sealed class UnitTemplate
    {
        public UnitTemplate(string templateId, UnitType unitType, float maxHealth,
            float attackPower, float defense, float criticalRate, float criticalMultiplier)
        {
            TemplateId = templateId ?? string.Empty;
            UnitType = unitType;
            MaxHealth = maxHealth;
            AttackPower = attackPower;
            Defense = defense;
            CriticalRate = criticalRate;
            CriticalMultiplier = criticalMultiplier;
        }

        public string TemplateId { get; }
        public UnitType UnitType { get; }
        public float MaxHealth { get; }
        public float AttackPower { get; }
        public float Defense { get; }
        public float CriticalRate { get; }
        public float CriticalMultiplier { get; }
    }
}
