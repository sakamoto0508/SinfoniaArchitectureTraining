using System;
using Domain;
using UnityEngine;

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
                template.CriticalRate, template.CriticalMultiplier);
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

            var entity = CreateEntity(template);
            var go = UnityEngine.Object.Instantiate(prefab, position, Quaternion.identity);
            var presenter = go.GetComponent<Adaptor.UnitPresenter>();
            if (presenter == null)
            {
                // 自動で追加してバインドする
                presenter = go.AddComponent<Adaptor.UnitPresenter>();
            }

            presenter.Bind(entity);
            return go;
        }
    }
}
