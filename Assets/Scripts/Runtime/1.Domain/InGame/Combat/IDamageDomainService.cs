using System;

namespace Domain
{
    /// <summary>
    ///     ダメージ算出を行うドメインサービスのインターフェース。
    /// </summary>
    public interface IDamageDomainService
    {
    /// <summary> 指定された攻撃者・防御者のステータスからダメージを計算する。内部でクリティカル判定を行う。
    /// </summary>
    /// <param name="attacker">攻撃者のステータス。</param>
    /// <param name="defender">防御者のステータス。</param>
    /// <param name="random">ドメイン向け乱数プロバイダ（クリティカル判定に使用）。</param>
    /// <returns>計算結果（DamageResult）。</returns>
    DamageResult Calculate(AttackerStats attacker, DefenderStats defender, Domain.IRandomProvider random);
    }
}
