using System;
using System.Linq;
using UnityEngine;

namespace Application
{
    public class UnitMovementUseCase
    {
        /// <summary>
        /// コンストラクタ。
        /// IMoveService を注入して移動処理を外部に委譲します。
        /// </summary>
        /// <param name="moveService">移動処理を実装したサービス。</param>
        /// <param name="unitQueryService">ユニット情報を取得するサービス。</param>
        public UnitMovementUseCase(IMoveService moveService, IUnitQueryService unitQueryService)
        {
            _moveService = moveService;
            _unitQueryService = unitQueryService;
        }

        public void Update(Guid selfId, Guid target, float stoppingRange)
        {
            StartPursue(selfId, target, stoppingRange);
        }

        /// <summary>
        /// 指定した mover を target に向かって追従させます。
        /// stoppingRange は到達判定（この距離以内で停止）に使用します。
        /// </summary>
        /// <param name="mover">追従を行うユニットの ID。</param>
        /// <param name="target">追従対象のユニットの ID。</param>
        /// <param name="stoppingRange">停止距離（この範囲以内で移動を停止）。</param>
        public void StartPursue(Guid mover, Guid target, float stoppingRange)
        {
            var moverPos = _moveService.GetPosition(mover);
            var targetPos = _moveService.GetPosition(target);

            float distance = Vector3.Distance(moverPos, targetPos);

            if (distance > stoppingRange)
            {
                _moveService.Move(mover, targetPos);
            }
            else
            {
                _moveService.Stop(mover);
            }
        }

        public void Stop(Guid mover)
        {
            _moveService.Stop(mover);
        }

        // 移動を実行するためのサービス（Adaptor 層で実装される）
        private readonly IMoveService _moveService;
        private readonly IUnitQueryService _unitQueryService;
    }
}
