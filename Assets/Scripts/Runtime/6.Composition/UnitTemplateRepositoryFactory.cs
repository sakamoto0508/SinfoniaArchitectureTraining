using System;
using Domain;
using UnityEngine;
using InfraStructure;

namespace Composition
{
    /// <summary>
    ///     UnitTemplateRepository を生成するファクトリ。
    ///     CompositionInitializer から呼び出して使用してください。
    /// </summary>
    public sealed class UnitTemplateRepositoryFactory
    {
        /// <summary>
        ///     ScriptableObject 配列から IUnitTemplateRepository を生成して返す。
        ///     Inspector から渡される ScriptableObject[] を受け取り、CharacterAsset に変換して Repo を生成します。
        /// </summary>
        public IUnitTemplateRepository Create(ScriptableObject[] characterAssets)
        {
            var assets = characterAssets ?? Array.Empty<ScriptableObject>();
            // filter and cast to CharacterAsset where possible
            var caList = new System.Collections.Generic.List<CharacterAsset>();
            foreach (var a in assets)
            {
                if (a is CharacterAsset ca) caList.Add(ca);
            }
            return new UnitTemplateRepository(caList.ToArray());
        }
    }
}
