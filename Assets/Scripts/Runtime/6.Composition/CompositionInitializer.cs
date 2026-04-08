using UnityEngine;
using Domain;
using System;

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
            var unitRepo = factory.Create(_characterAssets);

            // CompositionRoot に注入して共有ユースケースを初期化。
            CompositionRoot.Initialize(randomProvider, damageService, unitRepo);

            // シーン切り替えで破棄されないようにする。
            DontDestroyOnLoad(gameObject);
        }
    }
}
