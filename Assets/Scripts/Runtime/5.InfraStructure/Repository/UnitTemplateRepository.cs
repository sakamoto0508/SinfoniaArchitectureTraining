using System;
using System.Linq;
using Domain;
using UnityEngine;
using InfraStructure;

namespace InfraStructure
{
    /// <summary>
    ///     ScriptableObject 配列から UnitTemplate を提供するリポジトリ実装（非 MonoBehaviour）。
    ///     Composition の Factory から生成して使用してください。
    /// </summary>
    public sealed class UnitTemplateRepository : IUnitTemplateRepository
    {
        /// <summary> コンストラクタ。ScriptableObject 配列を受け取りリポジトリを初期化します。 </summary>
        /// <param name="characterAssets">CharacterAsset 相当の ScriptableObject 配列。</param>
        public UnitTemplateRepository(CharacterAsset[] characterAssets)
        {
            _characterAssets = characterAssets;
        }

        /// <summary>
        ///     templateId に対応する UnitTemplate を取得します。見つからない場合は KeyNotFoundException を投げます。
        /// </summary>
        public UnitTemplate Get(string templateId)
        {
            if (string.IsNullOrEmpty(templateId))
            {
                throw new ArgumentException($"UnitTemplateRepository.Get: templateId を指定してください。", nameof(templateId));
            }

            if (TryGet(templateId, out var template))
            {
                return template;
            }

            throw new System.Collections.Generic.KeyNotFoundException($"UnitTemplateRepository.Get: テンプレートが見つかりません。 id={templateId}");
        }

        /// <summary>
        ///     見つかれば true を返し out に template を設定します。
        /// </summary>
        public bool TryGet(string templateId, out UnitTemplate template)
        {
            template = default;

            if (string.IsNullOrEmpty(templateId))
            {
                return false;
            }

            if (_characterAssets == null || _characterAssets.Length == 0)
            {
                return false;
            }

            var asset = _characterAssets.FirstOrDefault(a => string.Equals(a.name, templateId, StringComparison.Ordinal));
            if (asset == null)
            {
                return false;
            }

            template = Map(asset);
            return true;
        }

        /// <summary> 内部で保持する ScriptableObject 配列。 </summary>
        private CharacterAsset[] _characterAssets = Array.Empty<CharacterAsset>();

        private static UnitTemplate Map(ScriptableObject asset)
        {
            if (asset is CharacterAsset ca)
            {
                return new UnitTemplate(
                    templateId: asset.name,
                    unitType: ca.CharacterName,
                    maxHealth: ca.Health,
                    moveSpeed: ca.MoveSpeed,
                    attackPower: ca.AttackPower,
                    attackRange: ca.AttackRange,
                    defense: ca.Defence,
                    criticalRate: ca.CriticalChance,
                    criticalMultiplier: ca.CriticalDamage
                );
            }

            // フォールバック: 非期待型の場合は既存のリフレクションベースの読み取りを使う
            var type = asset.GetType();
            float GetFloat(string propName)
            {
                var prop = type.GetProperty(propName);
                if (prop != null && prop.GetMethod != null)
                {
                    var val = prop.GetValue(asset);
                    if (val is float f) return f;
                }
                return 0f;
            }

            Domain.UnitType ResolveUnitType()
            {
                var prop = type.GetProperty("CharacterName");
                if (prop != null && prop.GetMethod != null)
                {
                    var val = prop.GetValue(asset);
                    if (val is Domain.UnitType ut) return ut;
                    if (val is int i) return (Domain.UnitType)i;
                }
                return Domain.UnitType.Unknown;
            }

            return new UnitTemplate(
                templateId: asset.name,
                unitType: ResolveUnitType(),
                maxHealth: GetFloat("Health"),
                moveSpeed: GetFloat("MoveSpeed"),
                attackPower: GetFloat("AttackPower"),
                attackRange: GetFloat("AttackRange"),
                defense: GetFloat("Defence"),
                criticalRate: GetFloat("CriticalChance"),
                criticalMultiplier: GetFloat("CriticalDamage")
            );
        }
    }
}
