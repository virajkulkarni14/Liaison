using Liaison.BLL.Models.Unit.Interfaces;

namespace Liaison.BLL.Models.Unit.Abstracts
{
    public abstract class TwoStar : AUnit, IUnit
    {
        public bool UseOrdinal { get; set; }
        public abstract string GetName();

        public abstract string GetAdminCorps();

        public string PrintTree()
        {
            return AUnit.PrintAnyTree(this);
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
        public bool IsDecommissioned()
        {
            return Decommissioned;
        }
    }
}