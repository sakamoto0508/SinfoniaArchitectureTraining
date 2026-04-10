using System;
using Domain;

namespace Composition
{
    /// <summary>
    ///     Composition 層のルート静的コンテナ。
    ///     アプリ起動時に CompositionInitializer から初期化され、アプリ内で共有されるインスタンスを保持します。
    /// </summary>
    public static class CompositionRoot
    {
        /// <summary> ドメイン向け乱数プロバイダ。 </summary>
        public static IRandomProvider RandomProvider { get; private set; }

        /// <summary> ダメージ計算のドメインサービス。 </summary>
        public static IDamageDomainService DamageDomainService { get; private set; }

        /// <summary> ユニットテンプレートを取得するリポジトリ。 </summary>
        public static Domain.IUnitTemplateRepository UnitTemplateRepository { get; private set; }

        /// <summary>
        ///     Composition 層の依存性を初期化します。
        /// </summary>
        /// <param name="randomProvider">乱数プロバイダ。</param>
        /// <param name="damageDomainService">ダメージドメインサービス。</param>
        /// <param name="unitTemplateRepository">ユニットテンプレートリポジトリ。</param>
        public static void Initialize(IRandomProvider randomProvider, IDamageDomainService damageDomainService, Domain.IUnitTemplateRepository unitTemplateRepository)
        {
            RandomProvider = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));
            DamageDomainService = damageDomainService ?? throw new ArgumentNullException(nameof(damageDomainService));
            UnitTemplateRepository = unitTemplateRepository ?? throw new ArgumentNullException(nameof(unitTemplateRepository));
        }

        // Movement 系サービスと UseCase（CompositionInitializer で設定される）
        public static Application.IMoveService MoveService;
        public static Application.IUnitQueryService UnitQueryService;
        public static Application.FindNearestAndChaseUseCase ChaseUseCase;

        // UnitMovementUseCase は廃止しました。Adaptor 側の MovementAdapter を直接利用します。
    }

}
