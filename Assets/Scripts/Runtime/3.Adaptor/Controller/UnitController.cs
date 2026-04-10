using Application;
using Domain;
using System;

namespace Adaptor
{
    /// <summary>
    /// 自律行動用コントローラ (Adaptor層)。
    /// MonoBehaviour を継承しない純粋なクラスです。UnitSpawner で生成して View 側から毎フレーム Update を呼んでください。
    /// </summary>
    public sealed class UnitController
    {
        private readonly Application.FindNearestAndChaseUseCase _chaseUseCase;

        public UnitController(Application.FindNearestAndChaseUseCase chaseUseCase)
        {
            _chaseUseCase = chaseUseCase;
        }

        /// <summary>
        /// 毎フレーム呼び出されるコマンド。targetId と stoppingRange に基づき行動を指示します。
        /// </summary>
        public void Update(Guid selfId, Guid targetId, float stoppingRange)
        {
            if (_chaseUseCase == null) return;
            try
            {
                _chaseUseCase.Execute(selfId, stoppingRange);
            }
            catch (Exception ex)
            {
                // アダプタ層なのでログ出力に留める
                UnityEngine.Debug.LogWarning($"UnitController.Update: ChaseUseCase.Execute failed: {ex.Message}");
            }
        }
    }
}
