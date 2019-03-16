using System.Linq;
using Liaison.BLL.Models.Unit.Abstracts;
using Liaison.BLL.Models.Unit.Interfaces;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{
    public class Directorate : AUnit, IUnit
    {
        
        public Directorate(Data.Sql.Edmx.Unit sqlUnit, bool cont)
        {
            this.UnitId = sqlUnit.UnitId;        
            this.MissionName = sqlUnit.MissionName;
            this.Service = (ServicesBll)sqlUnit.ServiceIdx;
            this.ServiceType = (ServiceTypeBLL)sqlUnit.ServiceTypeIdx;
            this.RankSymbol = sqlUnit.RankSymbol.ToCharArray()[0];
            this.RankLevel = sqlUnit.Rank.RankLevel;
            this.RankStar = sqlUnit.Rank.Rank1;
            this.Mission = new BllMissions(sqlUnit.MissionUnits);
            this.Base = new BLLBase(sqlUnit.Bases.FirstOrDefault());

            this.Indices = sqlUnit.UnitIndexes.OrderBy(x => x.DisplayOrder).Where(x => x.IsDisplayIndex)
                .Select(x => x.IndexCode).ToList();
            this.SortIndex = GetSortIndex(sqlUnit.UnitIndexes);
        
            var relMain = sqlUnit.RelationshipsFrom.ToList();
            var relt = sqlUnit.RelationshipsTo.ToList();
            relMain.AddRange(relt);
            this.Relationships = new BLLRelationships(sqlUnit.UnitId, relt);
            //string a = "b";
        }

        public string GetName()
        {
            return this.MissionName;
        }

        public string PrintTree()
        {
            return AUnit.PrintAnyTree(this);
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

        public string GetEquipment()
        {
            return null;
        }

        public bool IsTaskForce { get; }
        public bool IsDecommissioned()
        {
            return Decommissioned;
        }

        public string GetAdminCorps()
        {
            return this.AdminCorps?.DisplayName;
        }
    }
}