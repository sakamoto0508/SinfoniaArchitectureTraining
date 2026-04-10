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

        public void Register(Guid id)
        {
            _units.Add(id);
        }

        public IEnumerable<Guid> GetAllUnits()
        {
            return _units;
        }
    }
}
