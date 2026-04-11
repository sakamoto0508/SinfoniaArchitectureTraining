using System;
using UnityEngine;

namespace Adaptor
{
    /// <summary>
    ///     Presenter が View に渡す軽量 DTO。
    ///     View はこの DTO を使って表示を更新します。
    /// </summary>
    public sealed class PresenterDto
    {
        public PresenterDto(Guid unitId, string templateId, string displayName, Vector3 position)
        {
            UnitId = unitId;
            TemplateId = templateId ?? string.Empty;
            DisplayName = displayName ?? string.Empty;
            Position = position;
        }

        /// <summary> ユニットの一意識別子。 </summary>
        public Guid UnitId { get; }

        /// <summary> テンプレートID。 </summary>
        public string TemplateId { get; }

        /// <summary> 表示名、または画面更新用文字列。 </summary>
        public string DisplayName { get; }

        /// <summary> 表示用の座標。 </summary>
        public Vector3 Position { get; }
    }
}
