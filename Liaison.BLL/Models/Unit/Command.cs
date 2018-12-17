using System.Collections.Generic;
using Liaison.Helper.Enumerators;
using System.Linq;
using Liaison.Data.Sql.Edmx;
using System.Threading.Tasks;
using Liaison.BLL.Models.Unit.Abstracts;
using Liaison.BLL.Models.Unit.Interfaces;

namespace Liaison.BLL.Models.Unit
{
	public class Command : AUnit, IUnit
    {
        public string GetAdminCorps()
        {
            return this.AdminCorps?.DisplayName;
        }
        private string CommandName { get; set; }
        private string UniqueName { get; set; }

        public Command(Data.Sql.Edmx.Unit sqlUnit, bool doRelTos)
        {
            this.UnitId = sqlUnit.UnitId;
            this.UnitGuid = sqlUnit.UnitGuid;
            this.MissionName = sqlUnit.MissionName;
            this.UniqueName = sqlUnit.UniqueName;
            this.CommandName = sqlUnit.CommandName;
            this.Service = (ServicesBll) sqlUnit.ServiceIdx;
            this.ServiceType = (ServiceTypeBLL) sqlUnit.ServiceTypeIdx;
            this.RankSymbol = sqlUnit.RankSymbol.ToCharArray()[0];
            this.RankLevel = sqlUnit.Rank.RankLevel;
            this.RankStar = sqlUnit.Rank.Rank1;
            this.Decommissioned = sqlUnit.Decommissioned ?? false;

            this.Mission = new BllMissions(sqlUnit.MissionUnits);
            this.Base = new BLLBase(sqlUnit.Bases.FirstOrDefault());
            this.Indices = sqlUnit.UnitIndexes.OrderBy(x => x.DisplayOrder).Where(x => x.IsDisplayIndex)
                .Select(x => x.IndexCode).ToList();
            this.SortIndex = GetSortIndex(sqlUnit.UnitIndexes);

            var relMain = sqlUnit.RelationshipsFrom.ToList();
            //var relt = doRelTos ? sqlUnit.RelationshipsTo.ToList() : new List<Relationship>();             
            var relt = sqlUnit.RelationshipsTo.ToList();

            relMain.AddRange(relt);
            this.Relationships = new BLLRelationships(sqlUnit.UnitId, relt);
        }

        public string GetName()
        {
            if (!string.IsNullOrWhiteSpace(this.UniqueName))
            {
                return this.UniqueName;
            }
            if (!string.IsNullOrWhiteSpace(this.MissionName))
            {
                return this.MissionName + " " + this.CommandName;
            }
            return this.CommandName;
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

        public bool IsTaskForce => false;
        public bool IsDecommissioned()
        {
            return Decommissioned;
        }
    }
}