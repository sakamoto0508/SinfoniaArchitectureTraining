using System;
using Domain;
using UnityEngine;
using UnityEngine.AI;

namespace Composition
{
    /// <summary>
    ///     UnitTemplate から Domain.CharacterEntity と View (GameObject) を生成するファクトリ。
    ///     Composition 層で生成責務を持ち、UnitSpawner 等から利用します。
    /// </summary>
    public sealed class UnitFactory
    {
        /// <summary>
        ///     UnitTemplate から CharacterEntity を生成して返します。
        /// </summary>
        /// <param name="template">生成元の UnitTemplate。</param>
        /// <returns>生成された CharacterEntity。</returns>
        public CharacterEntity CreateEntity(UnitTemplate template)
        {
            if (template == null) throw new ArgumentNullException(nameof(template));

            return new CharacterEntity(Guid.NewGuid(), template.TemplateId,
                template.MaxHealth, template.AttackPower, template.Defense,
                template.CriticalRate, template.CriticalMultiplier, template.AttackRange);
        }

        /// <summary>
        ///     指定した prefab を Instantiate して UnitPresenter を結び付け、CharacterEntity をバインドした GameObject を返します。
        /// </summary>
        /// <param name="template">生成元の UnitTemplate。</param>
        /// <param name="prefab">ユニットの表示に使うプレハブ。</param>
        /// <param name="position">生成位置。</param>
        /// <returns>生成された GameObject。</returns>
        public GameObject Spawn(UnitTemplate template, GameObject prefab, Vector3 position)
        {
            if (template == null) throw new ArgumentNullException(nameof(template));
            if (prefab == null) throw new ArgumentNullException(nameof(prefab));

            var entity = new CharacterEntity(Guid.NewGuid(), template.TemplateId,
                template.MaxHealth, template.AttackPower, template.Defense,
                template.CriticalRate, template.CriticalMultiplier, template.AttackRange);
            // 互換性維持のため CreateEntity を残す。
            var go = UnityEngine.Object.Instantiate(prefab, position, Quaternion.identity);

            try
            {
                var agentAtSpawn = go.GetComponent<NavMeshAgent>();
                if (agentAtSpawn != null)
                {
                    try { agentAtSpawn.speed = template.MoveSpeed; } catch { }
                    try { agentAtSpawn.stoppingDistance = template.AttackRange; } catch { }
                }
            }
            catch { }

            var presenterInstance = new Adaptor.UnitPresenter(CompositionRoot.MoveService);
            presenterInstance.Bind(entity);

            // NavMeshAgent があれば CompositionRoot の MoveService に登録
            try
            {
                var agent = go.GetComponent<UnityEngine.AI.NavMeshAgent>();
                if (agent != null && Composition.CompositionRoot.MoveService != null)
                {
                    // MoveService に Register メソッドがあれば呼び出し、なければ Move を使う。
                    try
                    {
                        var moveSvc = Composition.CompositionRoot.MoveService;
                        var registerMethod = moveSvc.GetType().GetMethod("Register");
                        if (registerMethod != null)
                        {
                            // Register(Guid, NavMeshAgent) メソッドがあればそちらを呼び出す。
                            registerMethod.Invoke(moveSvc, new object[] { entity.UnitId, agent });
                        }
                        else
                        {
                            moveSvc.Move(entity.UnitId, agent.transform.position);
                        }
                    }
                    catch { }
                }
            }
            catch { }

            // UnitQueryService へ登録。
            try
            {
                var q = Composition.CompositionRoot.UnitQueryService;
                var reg = q?.GetType().GetMethod("Register");
                // UnitQueryService に Register(Guid) メソッドがあれば呼び出し、なければ無視する。
                reg?.Invoke(q, new object[] { entity.UnitId });
            }
            catch { }

            return go;
        }
    }
}
