using Adaptor;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace View
{
    public class UnitView : MonoBehaviour
    {
        /// <summary> この View を制御するコントローラ。 </summary>
        private UnitController _controller;
        private NavMeshAgent _agent;
        private Guid _id;
        private Guid _targetId;
        private float _attackRange;

        /// <summary>
        ///     初期化。ユニットIDとコントローラを設定する。
        /// </summary>
        public void Init(Guid id, UnitController unitController)
        {
            _controller = unitController;
            _id = id;
        }

        /// <summary>
        ///     毎フレーム呼び出される更新処理で、コントローラの Update を呼び出す。
        /// </summary>
        void Update()
        {
            _controller.Update(_id, _targetId, _attackRange);
        }
    }
}
