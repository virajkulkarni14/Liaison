using System;
using System.Collections.Generic;
using Liaison.BLL.Models.Equipment;
using Liaison.BLL.Models.Unit;

namespace Liaison.BLL.Models.Unit
{

    public abstract class TwoBar :  AUnit, IUnit
    {
        public List<IEquipment> Equipment { get; set; }
        public bool UseOrdinal { get; set; }

        public abstract string GetAdminCorps();
        public abstract string GetName();
        public string PrintTree()
        {
            throw new NotImplementedException();
        }
        public int GetRankLevel()
        {
            return RankLevel ?? 0;
        }

        public string GetRankStar()
        {
            return RankStar;
        }

        public string GetIndexes()
        {
            return this.Indices == null ? string.Empty : string.Join(",", this.Indices);
        }

        public abstract string GetEquipment();
        public bool IsTaskForce => false;
    }
}