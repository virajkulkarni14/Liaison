using System.Linq;
using System.Text;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Models.Unit
{
    public class Facility : AUnit, IUnit
    {
        public Facility(Data.Sql.Edmx.Unit sqlUnit)
        {
            this.UnitId = sqlUnit.UnitId;
            this.UnitGuid = sqlUnit.UnitGuid;
            this.MissionName = sqlUnit.MissionName;
            this.CommandName = sqlUnit.CommandName;
            if (sqlUnit.Ships.Any())
                this.Vessel = new Vessel(sqlUnit.Ships.First());
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
            //this.AdminCorps = new BLLAdminCorps(sqlUnit.AdminCorp);

            var relMain = sqlUnit.RelationshipsFrom.ToList();
            var relt = sqlUnit.RelationshipsTo.ToList();

            relMain.AddRange(relt);
            this.Relationships = new BLLRelationships(sqlUnit.UnitId, relt);
        }

        public Vessel Vessel { get; set; }

        public string GetAdminCorps()
        {
            return "";
        }
        public string CommandName { get; set; }

        public string GetName()
        {
            StringBuilder sb = new StringBuilder();
            if (this.Vessel != null)
            {
                sb.Append(this.Vessel.Prefix + " " + this.Vessel.ShipName + " (");
            }

            sb.Append(this.MissionName + " " + this.CommandName);
            if (this.Vessel != null)
            {
                sb.Append(")");
            }

            return sb.ToString();
        }

        public string PrintTree()
        {
            throw new System.NotImplementedException();
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
            return "";
        }

        public bool IsTaskForce => false;
        public bool IsDecommissioned()
        {
            return Decommissioned;
        }
    }
}