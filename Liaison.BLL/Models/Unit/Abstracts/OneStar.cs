using Liaison.BLL.Models.Unit.Interfaces;

namespace Liaison.BLL.Models.Unit.Abstracts
{
    public abstract class OneStar : AUnit, IUnit
    {
        public abstract string GetAdminCorps();
        public bool UseOrdinal { get; set; }
        public abstract string GetName();

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