using System;

namespace Domain
{
    /// <summary>
    ///     ユニットテンプレートを取得するリポジトリのインターフェース。
    ///     Application/Infrastructure 層で具象実装を提供してください。
    /// </summary>
    public interface IUnitTemplateRepository
    {
        /// <summary>
        ///     templateId に対応する UnitTemplate を取得します。見つからない場合は例外を投げる実装でも、
        ///     TryGet を使う実装でも構いません。
        /// </summary>
        UnitTemplate Get(string templateId);

        /// <summary>
        ///     見つかれば true を返し out に template を設定します。
        /// </summary>
        bool TryGet(string templateId, out UnitTemplate template);
    }
}
