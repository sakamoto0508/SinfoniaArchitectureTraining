using UnityEngine;

namespace Adaptor
{
    /// <summary>
    ///     ユニットの所属を示す簡易コンポーネント。
    ///     UnitSpawner が生成時に設定します。
    /// </summary>
    public sealed class Team : MonoBehaviour
    {
        // 味方かどうか。true = 敵側
        public bool IsEnemy;
    }
}
