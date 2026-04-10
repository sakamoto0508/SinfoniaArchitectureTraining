using UnityEngine;

namespace Adaptor
{
    public readonly ref struct UnitPositionDTO
    {
        public UnitPositionDTO(UnityEngine.Vector3 position)
        {
            Position = position;
        }

        public readonly UnityEngine.Vector3 Position;
    }
}
