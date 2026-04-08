using System;
using Domain;
using UnityEngine;

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
        /// </summary>
        public IUnitTemplateRepository Create(ScriptableObject[] characterAssets)
        {
            var assets = characterAssets ?? System.Array.Empty<ScriptableObject>();

            // InfraStructure アセンブリへのコンパイル時参照を避けるため、リフレクションで生成を試みる。
            var typeName = "InfraStructure.UnitTemplateRepository, InfraStructure";
            var type = Type.GetType(typeName);
            if (type == null)
            {
                // InfraStructure 側の型が見つからない場合。
                Debug.LogError($"UnitTemplateRepositoryFactory: 型が見つかりません。 type={typeName}。InfraStructure アセンブリが存在しコンパイル済みか確認してください。");
                throw new InvalidOperationException($"UnitTemplateRepositoryFactory: 型が見つかりません。 type={typeName}");
            }

            try
            {
                var instance = Activator.CreateInstance(type, new object[] { assets });
                if (instance is IUnitTemplateRepository repo)
                {
                    return repo;
                }

                // 生成したオブジェクトが期待するインターフェースを実装していない場合。
                Debug.LogError($"UnitTemplateRepositoryFactory: 生成したインスタンスが IUnitTemplateRepository を実装していません。 type={type.FullName}");
                throw new InvalidOperationException($"UnitTemplateRepositoryFactory: 生成したインスタンスが IUnitTemplateRepository を実装していません。 type={type.FullName}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"UnitTemplateRepositoryFactory: リフレクションによる生成に失敗しました。 type={typeName} エラー: {ex}");
                throw;
            }
        }   
    }
}
