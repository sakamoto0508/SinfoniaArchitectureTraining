using Adaptor;
using Application;
using Domain;
using InfraStructure;
using System;
using UnityEngine;

namespace Composition
{
    /// <summary>
    ///     Composition 層の初期化コンポーネント。
    ///     シーンの最初の GameObject にアタッチしておくことで、アプリ起動時に依存性をワイヤリングします。
    /// </summary>
    [DefaultExecutionOrder(-1000)]
    public sealed class CompositionInitializer : MonoBehaviour
    {
        /// <summary> Inspector から渡す CharacterAsset (ScriptableObject) の配列。CompositionInitializer がリポジトリ作成時に利用します。 </summary>
        [SerializeField]
        [Tooltip("UnitTemplateRepository に渡す CharacterAsset (ScriptableObject) 配列。空の場合はリポジトリは空になります。")]
        private ScriptableObject[] _characterAssets = Array.Empty<ScriptableObject>();

        /// <summary> Awake で依存性を生成して CompositionRoot に注入します。 </summary>
        private void Awake()
        {
            // 乱数プロバイダ生成（Composition ローカル実装を使用）。
            IRandomProvider randomProvider = new LocalRandomProvider();

            // ドメインサービス生成。
            IDamageDomainService damageService = new DamageDomainService();

            // UnitTemplateRepository を Factory で生成して初期化する。
            var factory = new UnitTemplateRepositoryFactory();
            // pass ScriptableObject[] directly; casting to CharacterAsset[] with 'as' returns null
            var unitRepo = factory.Create(_characterAssets);

            // CompositionRoot に注入して共有ユースケースを初期化。
            CompositionRoot.Initialize(randomProvider, damageService, unitRepo);

            // Movement 系サービスと UseCase を生成して CompositionRoot にセット
            var moveService = new InfraStructure.NavMeshMoveService();
            var queryService = new InfraStructure.UnitQueryService();

            // Application 層の型で受け取るため、インターフェースにキャストして渡す
            Application.IMoveService moveServiceIface = moveService as Application.IMoveService;
            Application.IUnitQueryService queryServiceIface = queryService as Application.IUnitQueryService;

            var chaseUseCase = new Application.FindNearestAndChaseUseCase(moveServiceIface, queryServiceIface);

            // これらは Composition 層で保持する
            CompositionRoot.MoveService = moveServiceIface;
            CompositionRoot.UnitQueryService = queryServiceIface;
            CompositionRoot.ChaseUseCase = chaseUseCase;

            // シーン切り替えで破棄されないようにする。
            DontDestroyOnLoad(gameObject);
        }
    }
}
