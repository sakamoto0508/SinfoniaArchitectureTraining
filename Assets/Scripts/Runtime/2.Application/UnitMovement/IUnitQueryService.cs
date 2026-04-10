using Domain;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Application
{
    /// <summary>
    /// Unit一覧を取得する
    /// </summary>
    public interface IUnitQueryService
    {
        IEnumerable<Guid> GetAllUnits();
    }
}
