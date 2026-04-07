using System;
using Domain;

namespace Domain
{
    /// <summary>
    ///     ダメージ算出のドメインサービス実装。
    ///     四捨五入や最小防御値保護などのドメインルールを含む。
    /// </summary>
    public sealed class DamageDomainService : IDamageDomainService
    {
        /// <summary>
        ///     指定された攻撃者・防御者のステータスからダメージを計算する。
        ///     このメソッドは内部でクリティカル判定を行うため、ドメイン向け乱数プロバイダを受け取ります。
        /// </summary>
        /// <param name="attacker">攻撃者のステータス。</param>
        /// <param name="defender">防御者のステータス。</param>
        /// <param name="random">ドメイン向け乱数プロバイダ（クリティカル判定に使用）。</param>
        /// <returns>計算結果（DamageResult）。</returns>
        public DamageResult Calculate(AttackerStats attacker, DefenderStats defender, IRandomProvider random)
        {
            // 防御値が小さすぎる場合は保護する
            var defense = Math.Max(defender.Defense, MIN_DEFENSE);

            // ドメイン内でクリティカル判定を行う（0.0〜1.0 の乱数を用いる）
            var roll = random.NextDouble();
            var isCritical = roll < attacker.CriticalRate;

            // クリティカル倍率の適用
            var multiplier = isCritical ? attacker.CriticalMultiplier : 1f;

            // 基本ダメージ計算
            var raw = attacker.Attack * multiplier / defense;

            // 四捨五入して整数化（0.5は切り上げ）
            var rounded = (int)Math.Round(raw, MidpointRounding.AwayFromZero);

            return new DamageResult(rounded, isCritical);
        }

        private const float MIN_DEFENSE = 1f;
    }
}
