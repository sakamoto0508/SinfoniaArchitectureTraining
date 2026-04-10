using Adaptor;
using UnityEngine;

namespace View
{
    public class UnitViewModel
    {
        public Vector3 Position { get; private set; }

        public void UpdatePosition(in UnitPositionDTO dto)
        {
            Position = dto.Position;
        }
    }
}
