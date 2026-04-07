using System;
using Domain;

namespace Application
{
    /// <summary>
    ///     ダメージ計算のユースケース実装。クリティカル判定はドメイン側で行われるため、
    ///     ドメイン向け乱数プロバイダをドメインサービスへ渡します。
    /// </summary>
    public sealed class DamageCalculationUseCase : IDamageCalculationUseCase
    {
        /// <summary> コンストラクタ。 </summary>
        /// <param name="random">乱数プロバイダ。</param>
        /// <param name="damageService">ダメージドメインサービス。</param>
        public DamageCalculationUseCase(Domain.IRandomProvider random, IDamageDomainService damageService)
        {
            _random = random ?? throw new ArgumentNullException(nameof(random));
            _damageService = damageService ?? throw new ArgumentNullException(nameof(damageService));
        }        

        /// <summary>
        ///     攻撃者・防御者のステータスからダメージを計算して返す。
        ///     弓兵などクリティカル対象のユニットは AttackerStats.CriticalRate を設定してください。
        /// </summary>
        /// <param name="attacker">攻撃者のステータス。</param>
        /// <param name="defender">防御者のステータス。</param>
        /// <returns>ダメージ計算結果。</returns>
        public DamageResult Calculate(AttackerStats attacker, DefenderStats defender)
        {
            // ドメインサービスに計算を委譲する（クリティカル判定はドメイン内で行われる）
            return _damageService.Calculate(attacker, defender, _random);
        }

        private readonly Domain.IRandomProvider _random;
        private readonly IDamageDomainService _damageService;
    }
}
