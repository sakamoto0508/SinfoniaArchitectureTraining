using System;
using UnityEngine;

namespace Application
{
    /// <summary>
    ///     ユニット移動に関するユースケースのインターフェース。
    ///     Application 層に置くことで移動の高レベルな振る舞いを定義します。
    /// </summary>
    public interface IUnitMovementUseCase
    {
        /// <summary>
        ///     指定ユニットを指定ターゲットの位置まで追従さ
        ///     停止距離 stopRange に達したら移動を止める想定。
        /// </summary>
        void StartPursue(Guid moverId, Guid targetId, Vector3 targetPosition, float stopRange);

        /// <summary>
        ///     指定ユニットの移動を停止する。
        /// </summary>
        void Stop(Guid moverId);
    }
}
