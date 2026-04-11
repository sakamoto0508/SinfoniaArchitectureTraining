using Adaptor;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace View
{
    using Adaptor;
    public class UnitView : MonoBehaviour
    {
        /// <summary> この View を制御するコントローラ。 </summary>
        private UnitController _controller;
        private NavMeshAgent _agent;
        private Guid _id;
        private Guid _targetId;
        private float _attackRange;
        private View.UnitPresenterBehaviour _presenterBehaviour;

        /// <summary>
        ///     初期化。ユニットIDとコントローラを設定する。
        /// </summary>
        public void Init(Guid id, UnitController unitController)
        {
            _controller = unitController;
            _id = id;
        }

        /// <summary>
        ///     MonoBehaviour ラッパーを受け取ってバインドします。
        /// </summary>
        public void SetPresenterBehaviour(View.UnitPresenterBehaviour behaviour)
        {
            _presenterBehaviour = behaviour;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            try
            {
                if (_presenterBehaviour != null)
                {
                    gameObject.name = _presenterBehaviour.GetDisplayName();
                }
            }
            catch { }
        }

        /// <summary>
        ///     毎フレーム呼び出される更新処理で、コントローラの Update を呼び出す。
        /// </summary>
        void Update()
        {
            _controller?.Update(_id, _targetId, _attackRange);
            UpdateDisplay();
        }
    }
}
