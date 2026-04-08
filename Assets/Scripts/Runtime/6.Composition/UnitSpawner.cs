using System;
using Domain;
using InfraStructure;
using UnityEngine;

namespace Composition
{
    /// <summary>
    ///     シーン上でユニットをスポーンするためのシンプルなスポナーコンポーネント。
    ///     Inspector でプレハブとテンプレートIDを設定してテストプレイに使用してください。
    /// </summary>
    [DefaultExecutionOrder(-900)]
    public sealed class UnitSpawner : MonoBehaviour
    {
        /// <summary> 味方の生成数。 </summary>
        [SerializeField]
        [Tooltip("スポーンする味方ユニットの数。")]
        private int _friendCount = 1;

        [SerializeField]
        [Tooltip("生成する味方の CharacterAsset。asset.name が templateId、asset.Character が Prefab になります。")]
        private CharacterAsset _friendAsset = null;

        /// <summary> 敵の生成数。 </summary>
        [SerializeField]
        [Tooltip("スポーンする敵ユニットの数。")]
        private int _enemyCount = 1;

        [SerializeField]
        [Tooltip("生成する敵の CharacterAsset。asset.name が templateId、asset.Character が Prefab になります。")]
        private CharacterAsset _enemyAsset = null;

        [SerializeField, Tooltip("スポーンする味方ユニットの位置。")]
        private Transform _friendPosition = null;

        [SerializeField, Tooltip("スポーンする敵ユニットの位置。")]
        private Transform _enemyPosition = null;

        private readonly UnitFactory _factory = new UnitFactory();

        /// <summary> Awake で自動的にスポーンします（Play 中の確認用）。 </summary>
        private void Awake()
        {
            // UnitTemplateRepository の存在を確認
            var repo = Composition.CompositionRoot.UnitTemplateRepository;
            if (repo == null)
            {
                Debug.LogError("UnitSpawner: CompositionRoot.UnitTemplateRepository が null です。CompositionInitializer の設定を確認してください。");
                return;
            }

            // 味方を生成
            SpawnGroup(_friendAsset, _friendCount, _friendPosition.position);

            // 敵を生成
            SpawnGroup(_enemyAsset, _enemyCount, _enemyPosition.position);
        }

        /// <summary>
        /// 指定したテンプレート ID に基づいて、複数のユニットを基準位置から横方向に並べて生成します。
        /// </summary>
        /// <param name="templateId">生成するユニットのテンプレートを識別する ID。null または空文字列の場合は処理をスキップします。</param>
        /// <param name="count">生成するユニットの数。0 以下の場合はユニットは生成されません。</param>
        /// <param name="basePosition">最初のユニットを配置する基準位置。以降のユニットはこの位置から横方向にオフセットされて配置されます。</param>
        private void SpawnGroup(CharacterAsset asset, int count, Vector3 basePosition)
        {
            if (asset == null)
            {
                Debug.LogWarning("UnitSpawner: CharacterAsset が割り当てられていません。スキップします。");
                return;
            }

            var templateId = asset.name;
            if (!CompositionRoot.UnitTemplateRepository.TryGet(templateId, out var template))
            {
                Debug.LogError($"UnitSpawner: テンプレートが見つかりません。 id={templateId}");
                return;
            }

            var prefab = asset.Character;
            if (prefab == null)
            {
                Debug.LogError($"UnitSpawner: CharacterAsset の Character が null です。 asset={asset.name}");
                return;
            }

            for (int i = 0; i < Math.Max(0, count); i++)
            {
                var offset = new Vector3(i * 1.5f, 0f, 0f);
                var position = basePosition + offset;
                var go = _factory.Spawn(template, prefab, position);
                // 必要ならここで派生する Controller や AI を追加し、UnitPresenter を取得して操作する。
                var presenter = go.GetComponent<Adaptor.UnitPresenter>();
            }
        }
    }
}
