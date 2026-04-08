using System;
using Domain;
using UnityEngine;

namespace Adaptor
{
    /// <summary>
    ///     ユニットの表示・ビューを担当する Presenter。
    ///     CharacterEntity を受け取り表示を更新します。
    /// </summary>
    public sealed class UnitPresenter : MonoBehaviour
    {
        /// <summary> バインドされた CharacterEntity。 </summary>
        public CharacterEntity Entity { get; private set; }

        /// <summary>
        ///     CharacterEntity を受け取り表示を初期化します。
        /// </summary>
        public void Bind(CharacterEntity entity)
        {
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
            // 表示名にテンプレート id をセットする
            gameObject.name = $"Unit_{Entity.TemplateId}";
            // 初回の表示更新
            UpdateView();
        }

        /// <summary>
        ///     表示を更新します。必要に応じて Healthバーやエフェクトを更新してください。
        /// </summary>
        public void UpdateView()
        {
            if (Entity == null) return;
            // 現状は GameObject 名に現在HP を反映する簡易実装
            var hp = (int)Entity.Health.CurrentHealth.Value;
            gameObject.name = $"Unit_{Entity.TemplateId}_HP{hp}";
        }

        //private void Update()
        //{
        //    // 毎フレーム表示を更新する（例示）。負荷が気になる場合はイベント駆動に変更してください。
        //    UpdateView();
        //}
    }
}
