using System;
using Liaison.BLL.Models.Unit.Interfaces;

namespace Liaison.BLL.Models.Unit.Abstracts
{
    public abstract class OneBar : AUnit, IUnit
    {
        public abstract string GetAdminCorps();
        public bool IsDecommissioned()
        {
            return Decommissioned;
        }


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