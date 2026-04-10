using System;
using UnityEngine;

namespace Application
{
    public interface IMoveService
    {
        void Move(Guid id, Vector3 position);
        void Stop(Guid id);
        Vector3 GetPosition(Guid id);
    }
}
