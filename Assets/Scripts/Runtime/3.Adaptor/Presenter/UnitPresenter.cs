using Application;
using Domain;
using System;
using UnityEngine;

namespace Adaptor
{
    /// <summary>
    /// ユニットの表示・ビューを担当する Presenter (MonoBehaviour)。
    /// CharacterEntity を受け取り表示を更新します。
    /// </summary>
    public sealed class UnitPresenter : MonoBehaviour
    {
        private Application.IMoveService _moveService;

        /// <summary> バインドされた CharacterEntity。 </summary>
        public CharacterEntity Entity { get; private set; }

        /// <summary>
        /// 外部から MoveService を注入します。Composition 層（Factory）で呼んでください。
        /// </summary>
        public void Init(Application.IMoveService moveService)
        {
            _moveService = moveService;
        }

        /// <summary>
        /// CharacterEntity を受け取り表示を初期化します。
        /// </summary>
        public void Bind(CharacterEntity entity)
        {
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
            UpdateView();
        }

        /// <summary>
        /// 表示を更新します。必要に応じて Healthバーやエフェクトを更新してください。
        /// </summary>
        public void UpdateView()
        {
            if (Entity == null) return;
            var hp = (int)Entity.Health.CurrentHealth.Value;
            try
            {
                gameObject.name = $"{Entity.TemplateId} HP:{hp}";
            }
            catch { }
        }

        public UnitPositionDTO Get(Guid id)
        {
            if (_moveService == null) return new UnitPositionDTO(Vector3.zero);
            return new UnitPositionDTO(_moveService.GetPosition(id));
        }

        public void Update()
        {
            UpdateView();
        }
    }
}
