using Application;
using System;
using System.Collections.Generic;

namespace InfraStructure
{
    /// <summary>
    /// Unit一覧管理
    /// </summary>
    public class UnitQueryService : Application.IUnitQueryService
    {
        private readonly List<Guid> _units = new();
        /// <summary> ユニット ID を登録する。 </summary>
        public void Register(Guid id)
        {
            _units.Add(id);
        }

        /// <summary> 登録されている全ユニットの ID を返す。 </summary>
        public IEnumerable<Guid> GetAllUnits()
        {
            return _units;
        }
    }
}
