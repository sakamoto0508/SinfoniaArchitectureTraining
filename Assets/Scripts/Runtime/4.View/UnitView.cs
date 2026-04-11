using Adaptor;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace View
{
    public class UnitView : MonoBehaviour
    {
        private UnitController _controller;
        private NavMeshAgent _agent;
        private Guid _id;
        private Guid _targetId;
        private float _attackRange;

        public void Init(Guid id, UnitController unitController)
        {
            _controller = unitController;
            _id = id;
        }

        void Update()
        {
            _controller.Update(_id, _targetId, _attackRange);
        }
    }
}
