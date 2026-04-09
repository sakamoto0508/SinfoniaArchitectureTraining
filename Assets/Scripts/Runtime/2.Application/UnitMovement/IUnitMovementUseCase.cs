using System;
using UnityEngine;

namespace Application
{
    /// <summary>
    ///     ユニット移動に関するユースケースのインターフェース（Application 層）。
    ///     Adaptor 側がこのインターフェースを実装し、Controller はインターフェース経由で移動を依頼します。
    /// </summary>
    public interface IUnitMovementUseCase
    {
        /// <summary>
        ///     指定ユニットを targetPosition に向かわせ、stopRange を到達判定に使います。
        ///     moverId は CharacterEntity.Id を指定してください。
        /// </summary>
        void StartPursue(Guid moverId, Guid targetId, Vector3 targetPosition, float stopRange);

        /// <summary>
        ///     指定ユニットの追従を停止します。
        /// </summary>
        void Stop(Guid moverId);
    }
}
