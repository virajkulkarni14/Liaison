using System;

namespace Liaison.BLL.Models.Unit
{
    public abstract class TwoBar : AUnit,IUnit
    {
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