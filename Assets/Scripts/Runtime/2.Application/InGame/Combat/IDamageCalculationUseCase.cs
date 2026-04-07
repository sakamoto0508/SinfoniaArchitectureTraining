using Domain;

namespace Application
{
    /// <summary>
    ///     ダメージ計算ユースケースのインターフェース。
    /// </summary>
    public interface IDamageCalculationUseCase
    {
        /// <summary> 攻撃者・防御者のステータスからダメージを計算して返す。 </summary>
        /// <param name="attacker">攻撃者のステータス。</param>
        /// <param name="defender">防御者のステータス。</param>
        /// <returns>ダメージ計算結果。</returns>
        DamageResult Calculate(AttackerStats attacker, DefenderStats defender);
    }
}
