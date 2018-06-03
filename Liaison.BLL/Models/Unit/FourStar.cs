namespace Liaison.BLL.Models.Unit
{
    public abstract class FourStar : AUnit, IUnit
    {
        public abstract string GetName();
       

    
        public string PrintTree()
        {
            return "printtree";
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