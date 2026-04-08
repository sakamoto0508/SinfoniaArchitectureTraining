using System;
using System.Linq;
using Domain;
using UnityEngine;

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
        public UnitTemplateRepository(ScriptableObject[] characterAssets)
        {
            _characterAssets = characterAssets ?? Array.Empty<ScriptableObject>();
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
        private ScriptableObject[] _characterAssets = Array.Empty<ScriptableObject>();

        /// <summary>
        ///     初期化メソッド。必要に応じてアセット配列を差し替えることができます（互換用）。
        /// </summary>
        public void Initialize(ScriptableObject[] characterAssets)
        {
            _characterAssets = characterAssets ?? Array.Empty<ScriptableObject>();
        }

        private static UnitTemplate Map(ScriptableObject asset)
        {
            // ScriptableObject のリフレクションでプロパティを読み取る（CharacterAsset に依存しない）
            var type = asset.GetType();
            float GetFloat(string propName)
            {
                var prop = type.GetProperty(propName);
                if (prop != null && prop.GetMethod != null)
                {
                    var val = prop.GetValue(asset);
                    if (val is float f) return f;
                }
                // プロパティが見つからなかった場合は 0 を返す
                return 0f;
            }

            // UnitType を読み取る。CharacterAsset の CharacterName プロパティを想定しているが、存在しない場合は Unknown を返す。
            Domain.UnitType ResolveUnitType()
            {
                var prop = type.GetProperty("CharacterName");
                if (prop != null && prop.GetMethod != null)
                {
                    var val = prop.GetValue(asset);
                    if (val is Domain.UnitType ut) return ut;
                    // enum might be serialized as int
                    if (val is int i) return (Domain.UnitType)i;
                }
                return Domain.UnitType.Unknown;
            }

            return new UnitTemplate(
                templateId: asset.name,
                unitType: ResolveUnitType(),
                maxHealth: GetFloat("Health"),
                attackPower: GetFloat("AttackPower"),
                defense: GetFloat("Defence"),
                criticalRate: GetFloat("CriticalChance"),
                criticalMultiplier: GetFloat("CriticalDamage")
            );
        }
    }
}
