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
        [SerializeField, Tooltip("スポーンを遅延させる秒数。0 にすると遅延なし。")]
        private float _spawnDelaySeconds = 0.5f;

        /// <summary> Start で自動的にスポーンします（若干遅延して初期化が安定するように調整）。 </summary>
        private void Start()
        {
            StartCoroutine(DelayedSpawn());
        }

        private System.Collections.IEnumerator DelayedSpawn()
        {
            // UnitTemplateRepository の存在を確認
            var repo = Composition.CompositionRoot.UnitTemplateRepository;
            if (repo == null)
            {
                Debug.LogError("UnitSpawner: CompositionRoot.UnitTemplateRepository が null です。CompositionInitializer の設定を確認してください。");
                yield break;
            }

            // 少し待つか、NavMesh が利用可能になるまで待機（どちらか早い方）。
            var timer = 0f;
            var timeout = Math.Max(0f, _spawnDelaySeconds);
            var checkInterval = 0.05f;
            while (timer < timeout)
            {
                yield return new WaitForSeconds(checkInterval);
                timer += checkInterval;
            }

            // 友軍と敵の生成
            SpawnGroup(_friendAsset, _friendCount, _friendPosition.position);
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
                var presenter = go.GetComponent<Adaptor.UnitPresenter>();
                Domain.CharacterEntity entity = null;
                if (presenter != null)
                {
                    entity = presenter.Entity;
                }
                // 所属情報をセット（味方/敵）。UnitController は Team コンポーネントを参照して行動します。
                // ここではリフレクションでコンポーネントを追加して、アセンブリ間の型衝突を避ける。
                Type FindType(string name)
                {
                    foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        var t = asm.GetType(name);
                        if (t != null) return t;
                    }
                    return null;
                }

                var teamType = FindType("Adaptor.Team");
                Component teamComp = null;
                if (teamType != null)
                {
                    teamComp = go.AddComponent(teamType);
                    var f = teamType.GetField("IsEnemy");
                    if (f != null)
                    {
                        f.SetValue(teamComp, asset == _enemyAsset);
                    }
                }

                // Adaptor.UnitController（純粋クラス）を生成し View.UnitView と紐付ける。
                Adaptor.UnitController controller = null;
                try
                {
                    controller = new Adaptor.UnitController(Composition.CompositionRoot.ChaseUseCase);
                }
                catch { }

                // Prefab 上に UnitView があればコントローラで初期化する。
                try
                {
                    var view = go.GetComponent<View.UnitView>();
                    if (view != null && controller != null && entity != null)
                    {
                        view.Init(entity.UnitId, controller);
                    }
                }
                catch { }

                // コントローラは MonoBehaviour ではないため、UnitRegistry にマッピングを登録する。
                try
                {
                    if (entity != null) Adaptor.UnitRegistry.Register(entity.UnitId, go);
                }
                catch { }

                // --- Spawn 時の単体パラメータをログ出力する ---
                try
                {
                    // Asset 側のパラメータ
                    var assetInfo = $"Asset(name={asset.name}, MoveSpeed={asset.MoveSpeed}, Health={asset.Health}, Defence={asset.Defence}, AttackPower={asset.AttackPower}, AttackRange={asset.AttackRange}, AttackSpeed={asset.AttackSpeed}, CriticalChance={asset.CriticalChance}, CriticalDamage={asset.CriticalDamage})";

                    // Entity 側のパラメータ
                    var entityInfo = entity != null
                        ? $"Entity(id={entity.UnitId}, template={entity.TemplateId}, currentHP={entity.Health.CurrentHealth.Value})"
                        : "Entity=null";

                    // NavMeshAgent 情報があれば出す
                    string agentInfo = "";
                    try
                    {
                        var agent = go.GetComponent<UnityEngine.AI.NavMeshAgent>();
                        if (agent != null)
                        {
                            agentInfo = $"NavMeshAgent(speed={agent.speed}, stoppingDistance={agent.stoppingDistance}, isOnNavMesh={agent.isOnNavMesh})";
                        }
                    }
                    catch { }

                    Debug.Log($"UnitSpawner: Spawned unit go={go.name} prefab={prefab.name} {entityInfo} {assetInfo} {agentInfo}");
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"UnitSpawner: Spawn ログ出力で例外: {ex.Message}");
                }
            }
        }
    }
}
