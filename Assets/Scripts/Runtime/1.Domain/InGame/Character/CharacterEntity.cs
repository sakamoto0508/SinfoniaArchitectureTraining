using System;

namespace Domain
{
    /// <summary>
    ///     キャラクターを表すエンティティ。健康値・攻撃力・防御力・クリティカル情報を保持し、
    ///     AttackerStats/DefenderStats へ変換するユーティリティを提供します。
    /// </summary>
    public sealed class CharacterEntity
    {
        public CharacterEntity(Guid id, string templateId, float maxHealth,
            float attackPower, float defense, float criticalRate, float criticalMultiplier)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            TemplateId = templateId ?? string.Empty;

            Health = new HealthEntity(maxHealth);

            _attackPower = new AttackPowerValueObject(attackPower);
            _defense = defense;
            _criticalRate = new CriticalRateValueObject(criticalRate);
            _criticalMultiplier = new CriticalMultiplierValueObject(criticalMultiplier);
        }

        public Guid Id { get; }

        /// <summary> テンプレートやアセットを指す識別子（表示名ではなく内部ID）。 </summary>
        public string TemplateId { get; }

        /// <summary>
        ///     CharacterEntity 自身は TemplateId を保持し、種別はテンプレート（IUnitTemplateRepository）から取得します。
        /// </summary>

        public HealthEntity Health { get; }

        /// <summary>
        ///     Template リポジトリから UnitType を解決して返します。見つからない場合は UnitType.Unknown を返します。
        /// </summary>
        public UnitType GetUnitType(IUnitTemplateRepository repository)
        {
            if (repository == null) return UnitType.Unknown;
            if (repository.TryGet(TemplateId, out var template)) return template.UnitType;
            return UnitType.Unknown;
        }

        /// <summary> 現在の状態から AttackerStats を作成します。 </summary>
        public AttackerStats ToAttackerStats()
        {
            return new AttackerStats(_attackPower.Value, _criticalRate.Value, _criticalMultiplier.Value);
        }

        /// <summary> 現在の状態から DefenderStats を作成します。 </summary>
        public DefenderStats ToDefenderStats()
        {
            return new DefenderStats(_defense);
        }

        /// <summary> 指定ダメージを適用します（0未満は無視）。 </summary>
        public void ApplyDamage(int damage)
        {
            if (damage <= 0) return;
            Health.ApplyDamage(damage);
        }

        /// <summary> 生存判定。 </summary>
        public bool IsAlive => Health.CurrentHealth.Value > 0f;

        private readonly AttackPowerValueObject _attackPower;
        private readonly float _defense;
        private readonly CriticalRateValueObject _criticalRate;
        private readonly CriticalMultiplierValueObject _criticalMultiplier;
    }
}
