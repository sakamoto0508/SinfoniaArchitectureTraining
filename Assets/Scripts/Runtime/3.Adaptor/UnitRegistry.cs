using System;
using System.Collections.Generic;
using UnityEngine;

namespace Adaptor
{
    /// <summary>
    ///     Entity.Id と GameObject を紐付ける簡易レジストリ。
    ///     Spawn 時に登録し、破棄時に解除してください。
    /// </summary>
    public static class UnitRegistry
    {
        private static readonly Dictionary<Guid, GameObject> _map = new();

        public static void Register(Guid id, GameObject go)
        {
            if (id == Guid.Empty || go == null) return;
            _map[id] = go;
        }

        public static void Unregister(Guid id)
        {
            if (id == Guid.Empty) return;
            _map.Remove(id);
        }

        public static bool TryGet(Guid id, out GameObject go)
        {
            if (id == Guid.Empty)
            {
                go = null;
                return false;
            }
            return _map.TryGetValue(id, out go);
        }
    }
}
