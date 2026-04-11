using System;
using System.Linq;
using UnityEngine;
// コンパイル順序の問題を避けるため、Application 型は完全修飾で参照しています。
namespace Application
{
    /// <summary>
    /// 最寄りの敵を探して追跡するユースケース。
    /// 毎フレームまたは周期的に Execute を呼び出して使用します。
    /// </summary>
    public sealed class FindNearestAndChaseUseCase
    {
        private readonly IMoveService _moveService;
        private readonly IUnitQueryService _queryService;

        public FindNearestAndChaseUseCase(IMoveService moveService, IUnitQueryService queryService)
        {
            _moveService = moveService ?? throw new ArgumentNullException(nameof(moveService));
            _queryService = queryService ?? throw new ArgumentNullException(nameof(queryService));
        }

        /// <summary>
        /// selfId のユニットに対して最寄りの敵を探し追跡/停止を指示します。
        /// </summary>
        public void Execute(Guid selfId, float stopRange)
        {
            var selfPos = _moveService.GetPosition(selfId);

            var nearest = _queryService.GetAllUnits()
                .Where(id => id != selfId)
                .OrderBy(id => (_moveService.GetPosition(id) - selfPos).sqrMagnitude)
                .FirstOrDefault();

            if (nearest == Guid.Empty) return;
            var targetPos = _moveService.GetPosition(nearest);
            var distance = Vector3.Distance(selfPos, targetPos);

            if (distance <= stopRange)
            {
                _moveService.Stop(selfId);
                Debug.Log($"FindNearestAndChaseUseCase: Stop {selfId} (target {nearest}) dist={distance} stopRange={stopRange}");
            }
            else
            {
                _moveService.Move(selfId, targetPos);
                Debug.Log($"FindNearestAndChaseUseCase: Move {selfId} -> {nearest} pos={targetPos} dist={distance}");
            }
        }
    }
}
