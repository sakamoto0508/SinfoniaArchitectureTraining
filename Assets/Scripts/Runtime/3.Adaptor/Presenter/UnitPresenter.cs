using Application;
using Domain;
using System;
using UnityEngine;

namespace Adaptor
{
    /// <summary>
    /// ユニットの表示ロジックを担うプレゼンター（Adaptor 層、純粋クラス）。
    /// View はこのクラスのメソッドを呼んで表示を更新してください。
    /// </summary>
    public sealed class UnitPresenter : MonoBehaviour
    {
        private Application.IMoveService _moveService;

        /// <summary> バインドされた CharacterEntity（読み取り専用） </summary>
        public CharacterEntity Entity { get; private set; }

        /// <summary>
        /// MonoBehaviour 化したため、初期化は Init で行います。
        /// </summary>
        public void Init(Application.IMoveService moveService)
        {
            _moveService = moveService;
        }

        /// <summary>
        /// CharacterEntity をバインドします。
        /// </summary>
        public void Bind(CharacterEntity entity)
        {
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        /// <summary>
        /// 表示用の文字列を取得します（例: 名前や HP を含む）。
        /// </summary>
        public string GetDisplayName()
        {
            if (Entity == null) return string.Empty;
            var hp = (int)Entity.Health.CurrentHealth.Value;
            return $"{Entity.TemplateId} HP:{hp}";
        }

        /// <summary>
        /// 指定したユニットの位置を取得します。
        /// </summary>
        public UnitPositionDTO GetPosition(Guid id)
        {
            if (_moveService == null) return new UnitPositionDTO(Vector3.zero);
            return new UnitPositionDTO(_moveService.GetPosition(id));
        }
    }
}
