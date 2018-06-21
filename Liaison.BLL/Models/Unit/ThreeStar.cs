using System;

namespace Liaison.BLL.Models.Unit
{
    public abstract class ThreeStar : AUnit, IUnit
    {
        public abstract string GetName();

        public string GetAdminCorps()
        {
            return "";
        }

        public string PrintTree()
        {
            throw new NotImplementedException();
        }

        public abstract int GetRankLevel();

        public string GetRankStar()
        {
            return RankStar;
        }
        public string GetIndexes()
        {
            return this.Indices == null ? string.Empty : string.Join(",", this.Indices);
        }

        public string GetEquipment()
        {
            return null;
        }

        public bool IsTaskForce => false;
    }
}