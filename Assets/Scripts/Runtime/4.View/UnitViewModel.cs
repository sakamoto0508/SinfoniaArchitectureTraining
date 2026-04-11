using Adaptor;
using UnityEngine;

namespace View
{
    public class UnitViewModel
    {
        /// <summary> 表示モデル上の位置。 </summary>
        public Vector3 Position { get; private set; }

        /// <summary>
        ///     DTO から位置情報を受け取り、モデルの位置を更新する。
        /// </summary>
        public void UpdatePosition(in UnitPositionDTO dto)
        {
            Position = dto.Position;
        }
    }
}
