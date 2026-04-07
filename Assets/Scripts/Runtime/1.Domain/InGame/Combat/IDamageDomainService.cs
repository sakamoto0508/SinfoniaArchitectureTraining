using System;

namespace Domain
{
    /// <summary>
    ///     ダメージ算出を行うドメインサービスのインターフェース。
    /// </summary>
    public interface IDamageDomainService
    {
        /// <summary> 指定された攻撃者・防御者のステータスとクリティカルフラグからダメージを計算する。 </summary>
        /// <param name="attacker">攻撃者のステータス。</param>
        /// <param name="defender">防御者のステータス。</param>
        /// <param name="isCritical">クリティカル（ヘッドショット）が発生したかどうか。</param>
        /// <returns>計算結果（DamageResult）。</returns>
        DamageResult Calculate(AttackerStats attacker, DefenderStats defender, bool isCritical);
    }
}
